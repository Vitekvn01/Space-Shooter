using Common;
using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public abstract class ProjectileBase : Entity
    {
        [SerializeField] protected float _velocity;

        [SerializeField] protected float _lifetime;

        [SerializeField] protected int _damage;

        [SerializeField] protected ImpactEffect _impactEffectPrefab;

        protected float _timer;

        protected virtual void OnHit(Destructible destructible) { }
        protected virtual void OnHit(Collider2D collider2D) { }
        protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }

        protected virtual void Movement(float stepLength)
        {
            Vector2 step = transform.up * stepLength;
            transform.position += new Vector3(step.x, step.y, 0);
        }

        #region Unity event 
        protected virtual void Update()
        {
            
            float stepLength = Time.deltaTime * _velocity;
            

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                OnHit(hit.collider);

                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != _parent)
                {

                    dest.ApplyDamage(_damage);

                    OnHit(dest);
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            _timer += Time.deltaTime;

            if (_timer > _lifetime)
                OnProjectileLifeEnd(hit.collider, hit.point);


            Movement(stepLength);


        }
        #endregion*/

        protected Destructible _parent;
        public void SetParentShooter(Destructible parent)
        {
            _parent = parent;
        }
    }
}

