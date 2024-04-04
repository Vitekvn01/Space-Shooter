using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupIndestructible : Powerup
    {
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.ApplyIndestructible(10);
        }
    }

}
