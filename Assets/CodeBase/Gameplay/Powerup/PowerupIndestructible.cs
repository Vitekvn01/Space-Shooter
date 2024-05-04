using UnityEngine;

namespace SpaceShooter
{
    public class PowerupIndestructible : Powerup
    {
        [Header("������ ������ :")]
        [SerializeField] private float _time;
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.ApplyIndestructible(_time);
        }
    }
}

