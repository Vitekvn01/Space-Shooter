using Common;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : ProjectileBase
    {
        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Instantiate(_impactEffectPrefab, pos, Quaternion.identity);
            base.OnProjectileLifeEnd(col, pos);
        }

        protected override void OnHit(Destructible destructible)
        {
            if (_parent == Player.Instance.ActiveShip)
            {
                Player.Instance.AddScore(destructible.ScoreValue);
                if (destructible is SpaceShip)
                {
                    if (destructible.CurrentHitPoints <= 0)
                    {
                        Player.Instance.AddKill();
                    }
                    
                }
            }
        }
    }
}

