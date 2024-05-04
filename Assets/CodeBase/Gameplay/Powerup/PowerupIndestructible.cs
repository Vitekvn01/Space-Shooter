using UnityEngine;

namespace SpaceShooter
{
    public class PowerupIndestructible : Powerup
    {
        [Header("Таймер бонуса :")]
        [SerializeField] private float _time;
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.ApplyIndestructible(_time);
        }
    }
}

