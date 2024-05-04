using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupSpeedup : Powerup
    {
        [Header("Таймер бонуса :")]
        [SerializeField] private float _time;
        [Header("Ускорить в :")]
        [SerializeField] private float _speedUp;
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.ApplySpeedUp(_time, _speedUp);
        }
    }
}

