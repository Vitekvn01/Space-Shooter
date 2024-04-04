using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : Entity
{
    [SerializeField] protected float _velocity;

    [SerializeField] protected float _lifetime;

    [SerializeField] protected int _damage;

    [SerializeField] protected ImpactEffect _impactEffectPrefab;

    protected float _timer;

    protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
    {
        Destroy(gameObject);
    }

    #region Unity event 
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
    #endregion*/

    protected Destructible _parent;
    public void SetParentShooter(Destructible parent)
    {
        _parent = parent;
    }
}
