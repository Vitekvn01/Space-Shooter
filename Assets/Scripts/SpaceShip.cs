using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// масса для Rigidbody
        /// </summary>
        [Header("SpaceShip")]
        [SerializeField] private float _mass;

        /// <summary>
        /// Толкающая вперёд сила.
        /// </summary>
        [SerializeField] private float _thrust;

        /// <summary>
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float _mobility;


        /// <summary>
        /// Макксимальная линейная сила.
        /// </summary>
        [SerializeField] private float _maxLinearVelocity;

        /// <summary>
        /// Макксимальная вращательная сила. В градусах/сек
        /// </summary>
        [SerializeField] private float _maxAngularVelocity;

        private Rigidbody2D _rigid;

        #region Public API
        /// <summary>
        /// Управление линейной тягой -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// Управление вращательной тягой -1.0 до +1.0
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

            InitOffensive();
        }

        protected override void OnDeath()
        {
            Instantiate(_explosionPrefabs, transform.position, transform.rotation);
            base.OnDeath();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
        }

        #endregion

        /// <summary>
        /// Метод добавления сил кораблю
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
    }
}

