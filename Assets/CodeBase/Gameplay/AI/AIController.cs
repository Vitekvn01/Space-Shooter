using System;
using UnityEditor;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        [SerializeField] private AIBehaviour _AIBehaviour;

        [SerializeField] private AIPointPatrol _patrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _navigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _navigationAngular;

        [SerializeField] private float _randomSelectMovePointTime;

        [SerializeField] private float _findNewTargtetTime;

        [SerializeField] private float _evadeRayLength;

        [SerializeField] private float _evadeDistance;

        [Header("Стрельба")]
        [SerializeField] private float _shootDelay;
        [SerializeField] private float _leadFactor;
        [SerializeField] private bool _isZoneFire;
        [SerializeField] private float _distanceFire;


        [Header("Зона обнаружения противника")]
        [SerializeField] private bool _isFindTargetInZoneDetection;
        [SerializeField] private float _distanceDetectionEnemy;

        [Header("Движение по вейпоинтам")]
        [SerializeField] private bool _isMoveWaypoints;
        [SerializeField] private Transform[] Waypoints;
        [SerializeField] private float _minDistanceToWaypoint;
        [SerializeField] private float _stopBetweenWaypointsTime;



        private bool IsEvade = false;

        private SpaceShip _spaceShip;

        private Vector3 _movePosition;

        private Destructible _selectedTarget;

        private Rigidbody2D _rbTarget;

        private int _indexWaypoints;


        private Timer _randomizeDirectionTimer;
        private Timer _fireTimer;
        private Timer _findNewTargetTimer;
        private Timer _nextPointTimer;

        private void Start()
        {
            _spaceShip = GetComponent<SpaceShip>();

            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()

        {
            if (_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionEvadeCollision();
            if (IsEvade == false)
            {
                ActionFindNewMovePosition();
                ActionFire();
            }
        }

        private void ActionFindNewMovePosition()
        {
            if (_selectedTarget != null)
            {
                Debug.Log("дистанция огня на упреждение " + MakeLead());
                _movePosition = _selectedTarget.transform.position + _selectedTarget.transform.up * MakeLead();
            }
            else
            {
                if (!_isMoveWaypoints)
                {
                    if (_patrolPoint != null)
                    {
                        bool isInsidePatrolZone = (_patrolPoint.transform.position - transform.position).sqrMagnitude < _patrolPoint.Radius * _patrolPoint.Radius;

                        if (isInsidePatrolZone == true)
                        {
                            if (_randomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * _patrolPoint.Radius + _patrolPoint.transform.position;

                                _movePosition = newPoint;

                                _randomizeDirectionTimer.Start(_randomSelectMovePointTime);

                            }
                        }
                        else
                        {
                            _movePosition = _patrolPoint.transform.position;
                        }
                    }

                }
                else
                {
                    ActionMovePositionNextWaypoint();
                }
            }

        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, _evadeRayLength) == true)
            {
                IsEvade = true;
                _movePosition = transform.position + transform.right * _evadeDistance;

            }
            if (IsEvade == true)
            {
                float _minDistancetoMovePosition = 2.5f;
                float currentDistancetoMovePosition = Vector3.Distance(transform.position, _movePosition);
                if (_minDistancetoMovePosition >= currentDistancetoMovePosition)
                {
                    IsEvade = false;
                }
            }

        }

        private void ActionControlShip()
        {
            _spaceShip.ThrustControl = _navigationLinear;

            _spaceShip.TorqueControl = ComputeAliginTorqueNormalized(_movePosition, _spaceShip.transform) * _navigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;

        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (_findNewTargetTimer.IsFinished == true)
            {
                _selectedTarget = FindNearestDestructableTarget();

                if (_selectedTarget != null)
                {
                    _rbTarget = _selectedTarget.GetComponent<Rigidbody2D>();
                }

                _findNewTargetTimer.Start(_shootDelay);
            }
        }

        private void ActionFire()
        {
            if (_selectedTarget != null)
            {
                if (_isZoneFire)
                {
                    if (_distanceFire >= Vector3.Distance(transform.position, _selectedTarget.transform.position))
                    {
                        if (_fireTimer.IsFinished == true)
                        {
                            _spaceShip.Fire(TurretMode.Primary);

                            _spaceShip.Fire(TurretMode.Secondary);

                            _fireTimer.Start(_shootDelay);
                        }
                    }
                }
                else
                {
                    if (_fireTimer.IsFinished == true)
                    {
                        _spaceShip.Fire(TurretMode.Primary);

                        _spaceShip.Fire(TurretMode.Secondary);

                        _fireTimer.Start(_shootDelay);
                    }
                }


            }
        }

        private Destructible FindNearestDestructableTarget()
        {

            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == _spaceShip) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == _spaceShip.TeamId) continue;

                float dist = Vector2.Distance(_spaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }

            if (_isFindTargetInZoneDetection)
            {
                if (potentialTarget != null)
                {
                    if (_distanceDetectionEnemy >= Vector3.Distance(transform.position, potentialTarget.transform.position))
                    {
                        return potentialTarget;
                    }
                    else return null;
                }
                else return null;
            }
            else
            {
                return potentialTarget;
            }

        }

        #region Timers

        private void InitTimers()
        {
            _randomizeDirectionTimer = new Timer(_randomSelectMovePointTime);
            _fireTimer = new Timer(_shootDelay);
            _findNewTargetTimer = new Timer(_findNewTargtetTime);
            _nextPointTimer = new Timer(0);
        }

        private void UpdateTimers()
        {
            _randomizeDirectionTimer.RemoveTime(Time.deltaTime);
            _fireTimer.RemoveTime(Time.deltaTime);
            _findNewTargetTimer.RemoveTime(Time.deltaTime);
            _nextPointTimer.RemoveTime(Time.deltaTime);
        }


        #endregion

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            _AIBehaviour = AIBehaviour.Patrol;
            _patrolPoint = point;
        }

        public void ActionMovePositionNextWaypoint()
        {
            if (Waypoints.Length != 0)
            {
                if (_nextPointTimer.IsFinished)
                {
                    _movePosition = Waypoints[_indexWaypoints].position;
                    float currentDistanceToWaypoint = Vector3.Distance(transform.position, _movePosition);
                    if (_minDistanceToWaypoint >= currentDistanceToWaypoint)
                    {
                        _indexWaypoints++;
                        _nextPointTimer.Start(_stopBetweenWaypointsTime);
                        if (_indexWaypoints >= Waypoints.Length)
                        {
                            _indexWaypoints = 0;
                        }
                    }
                }

            }
            else
            {
                _isMoveWaypoints = false;
            }
        }
        private float MakeLead()
        {
            return _rbTarget.velocity.magnitude * _leadFactor;
        }



#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_isFindTargetInZoneDetection)
            {
                Handles.color = new Color(0, 1, 0, 0.3f);
                Handles.DrawSolidDisc(transform.position, transform.forward, _distanceDetectionEnemy);
            }

            if (_isZoneFire)
            {
                Handles.color = new Color(100, 100, 0, 0.3f);
                Handles.DrawSolidDisc(transform.position, transform.forward, _distanceFire);
            }
        }
#endif
    }



}