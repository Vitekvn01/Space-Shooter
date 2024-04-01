using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// ����� ��� Rigidbody
        /// </summary>
        [Header("SpaceShip")]
        [SerializeField] private float _mass;

        /// <summary>
        /// ��������� ����� ����.
        /// </summary>
        [SerializeField] private float _thrust;

        /// <summary>
        /// ��������� ����.
        /// </summary>
        [SerializeField] private float _mobility;


        /// <summary>
        /// ������������� �������� ����.
        /// </summary>
        [SerializeField] private float _maxLinearVelocity;

        /// <summary>
        /// ������������� ������������ ����. � ��������/���
        /// </summary>
        [SerializeField] private float _maxAngularVelocity;

        private Rigidbody2D _rigid;

        #region Public API
        /// <summary>
        /// ���������� �������� ����� -1.0 �� +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// ���������� ������������ ����� -1.0 �� +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        [Header("explosionDestroy")]
        [SerializeField] private GameObject _explosionPrefabs;
        #endregion

        #region Unity Event
        protected override void Start()
        {
            base.Start();

            _rigid = GetComponent<Rigidbody2D>();
            _rigid.mass = _mass;

            _rigid.inertia = 1;
        }

        protected override void OnDeath()
        {
            Instantiate(_explosionPrefabs, transform.position, transform.rotation);
            base.OnDeath();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
        }

        #endregion

        /// <summary>
        /// ����� ���������� ��� �������
        /// </summary>
        private void UpdateRigidBody()
        {
            _rigid.AddForce(ThrustControl * _thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            _rigid.AddForce(-_rigid.velocity * (_thrust / _maxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
            _rigid.AddTorque(TorqueControl * _mobility * Time.fixedDeltaTime, ForceMode2D.Force);
            _rigid.AddTorque(-_rigid.angularVelocity * (_mobility / _maxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] _turrets;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < _turrets.Length; i++)
            {
                if (_turrets[i].Mode == mode)
                {
                    _turrets[i].Fire();
                }
            }
        }
    }
}

