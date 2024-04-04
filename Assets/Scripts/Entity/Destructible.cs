using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Destructible : Entity
    {
        #region Properties

        [SerializeField] private bool _indestructible;
        public bool IsIndestructible => _indestructible;


        [SerializeField] private int _hitPoints;


        private int _currentHitPoints;
        public int CurrentHitPoints => _currentHitPoints;

        private float _timer = 0;
        private float _timerIndestructible;
        private bool _isTimerOffIndestructible = false;

        #endregion

        #region Unity Events 

        protected virtual void Start()
        {
            _currentHitPoints = _hitPoints;
        }

        private void Update()
        {
            if (_isTimerOffIndestructible == true)
            {
                _timer += Time.deltaTime;
                if (_timer >= _timerIndestructible)
                {
                    _indestructible = false;
                    _timer = 0;
                    _isTimerOffIndestructible = false;
                }

            }
        }

        #endregion

        #region Public API

        public void ApplyDamge(int damage)
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
            if(_indestructible) return;
            _indestructible = true;
            _timerIndestructible = time;
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
    }
}
