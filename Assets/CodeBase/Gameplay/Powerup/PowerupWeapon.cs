using UnityEngine;

namespace SpaceShooter
{
    public class PowerupWeapon : Powerup
    {
        [SerializeField] private TurretProperties _properites;

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssignWeapon(_properites);
        }
    }   
}
