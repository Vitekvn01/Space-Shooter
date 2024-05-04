using Common;
using Unity.Mathematics;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [SerializeField] private Sprite _previewImage;

        public Sprite PreviewImage => _previewImage;
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

        [Header("explosionDestroy")]
        [SerializeField] private GameObject _explosionPrefabs;

        private float _timerSpeedUp;
        private float _timeOffSpeedUp;
        private bool _isSpeedUp = false;

        private float _oldThrust;

        #region Public API
        /// <summary>
        /// ���������� �������� ����� -1.0 �� +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// ���������� ������������ ����� -1.0 �� +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        public float MaxAngularVelocity => _maxAngularVelocity;
        public float MaxLinearVelocity => _maxLinearVelocity;
        

        #endregion

        #region Unity Event
        protected override void Start()
        {
            base.Start();

            _rigid = GetComponent<Rigidbody2D>();
            _rigid.mass = _mass;

            _rigid.inertia = 1;

            InitOffensive();

            _oldThrust = _thrust;
        }

        protected override void OnDeath()
        {
            Instantiate(_explosionPrefabs, transform.position, transform.rotation);
            base.OnDeath();
        }

        protected override void Update()
        {
            base.Update();
            if (_isSpeedUp == true)
            {
                _timerSpeedUp += Time.deltaTime;
                if (_timerSpeedUp >= _timeOffSpeedUp)
                {
                    _timerSpeedUp = 0;
                    _isSpeedUp = false; ;
                }

            }
            else
            {
                _thrust = _oldThrust;
            }
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
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

        [SerializeField] private int _maxEnergy;
        [SerializeField] private int _maxAmmo;
        [SerializeField] private int _energyRegenPerSecond;

        private float _primaryEnergy;
        private int _secondaryAmmo;

        public void AddEnergy(int e)
        {
            _primaryEnergy = math.clamp(_primaryEnergy + e, 0, _maxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            _secondaryAmmo = math.clamp(_secondaryAmmo + ammo, 0, _maxAmmo);
        }


        public bool DrawAmmo(int count)
        {
            if (count == 0)
            {
                return true;
            }

            if (_secondaryAmmo >= count)
            {
                _secondaryAmmo -= count;
                return true;
            }

            return false;
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
            {
                return true;
            }

            if (_primaryEnergy >= count)
            {
                _primaryEnergy -= count;
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties prop)
        {
            for (int i = 0; i < _turrets.Length; i++)
            {
                _turrets[i].AssignLoadout(prop);
            }
        }

        public void ApplySpeedUp(float time, float speed)
        {
            _isSpeedUp = true;
            _timeOffSpeedUp = time;
            SpeedUp(speed);

        }

        private void InitOffensive()
        {
            _primaryEnergy = _maxEnergy;
            _secondaryAmmo = _maxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            _primaryEnergy += (float)_energyRegenPerSecond * Time.fixedDeltaTime;
            _primaryEnergy = math.clamp(_primaryEnergy, 0, _maxEnergy);
        }

        private void SpeedUp(float speed)
        {
            _thrust = _thrust * speed;
        }
    }
}

