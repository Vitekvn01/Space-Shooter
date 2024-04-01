using Common;
using UnityEngine;


namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float _velocity;

        [SerializeField] private float _lifetime;

        [SerializeField] private int _damage;

        [SerializeField] private ImpactEffect _impactEffectPrefab;

        protected void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }


        private float _timer;
        protected Destructible _parent;

        private void Update()
        {
            float stepLength = Time.deltaTime * _velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {

                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != _parent)
                {
                    dest.ApplyDamge(_damage);
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            _timer += Time.deltaTime;

            if (_timer > _lifetime)
                OnProjectileLifeEnd(hit.collider, hit.point);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        public void SetPatentShooter(Destructible parent)
        {
            _parent = parent;
        }
    }
}
