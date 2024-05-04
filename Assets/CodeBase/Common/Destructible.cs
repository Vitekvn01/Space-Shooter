using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class Destructible : Entity
    {
        #region Properties

        [SerializeField] private bool _indestructible;
        public bool IsIndestructible => _indestructible;


        [SerializeField] private int _hitPoints;

        public int MaxHitPoints => _hitPoints;

        private int _currentHitPoints;
        public int CurrentHitPoints => _currentHitPoints;

        private float _timerIndestructible = 0;
        private float _timeOffIndestructible;
        private bool _isTimerOffIndestructible = false;

        #endregion

        #region Unity Events 

        protected virtual void Start()
        {
            _currentHitPoints = _hitPoints;
            transform.SetParent(null);
        }

        protected virtual void Update()
        {
            if (_isTimerOffIndestructible == true)
            {
                _timerIndestructible += Time.deltaTime;
                if (_timerIndestructible >= _timeOffIndestructible)
                {
                    _indestructible = false;
                    _timerIndestructible = 0;
                    _isTimerOffIndestructible = false;
                }

            }
        }

        #endregion

        #region Public API

        public void ApplyDamage(int damage)
        {
            if (_indestructible) return;

            _currentHitPoints -= damage;

            if (_currentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        public void ApplyIndestructible(float time)
        {
            if (_indestructible) return;
            _indestructible = true;
            _timeOffIndestructible = time;
            _isTimerOffIndestructible = true;

        }

        #endregion

        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            _eventOnDeath?.Invoke();
        }

        [SerializeField] private UnityEvent _eventOnDeath;
        public UnityEvent EventOnDeath => _eventOnDeath;

        private static HashSet<Destructible> _allDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => _allDestructibles;

        protected virtual void OnEnable()
        {
            if (_allDestructibles == null)
                _allDestructibles = new HashSet<Destructible>();

            _allDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            _allDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int _teamId;
        public int TeamId => _teamId;

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;


    }
}
