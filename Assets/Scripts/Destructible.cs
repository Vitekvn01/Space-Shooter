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

        #endregion

        #region Unity Events 

        protected virtual void Start()
        {
            _currentHitPoints = _hitPoints;
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
