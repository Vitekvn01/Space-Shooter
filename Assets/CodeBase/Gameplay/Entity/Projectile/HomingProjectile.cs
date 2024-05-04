using SpaceShooter;
using UnityEditor;
using UnityEngine;
using Common;
using Unity.Burst.CompilerServices;

public class HomingProjectile : ProjectileBase
{
    [Header("Настройки зоны поиска цели")]
    [SerializeField] private float _centerZoneFind;
    [SerializeField] private float _heightZoneFind;
    [SerializeField] private float _widthZoneFind;
    [SerializeField] private float _timerFindTarget;

    [Header("Настройки взрыва")]
    [SerializeField] private GameObject _explosionPrefabs;
    [SerializeField] private float _radiusExplosion;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _coefficientFinalDamage; // 0.5f

    private Transform _target;

    protected override void OnHit(Destructible destructible)
    {
        Explosion(destructible.transform.position, _radiusExplosion);
    }

    protected override void Movement(float stepLength)
    {
        if (_target != null)
        {
            // направление на цель
            Vector3 direction = _target.position - transform.position;
            // передаём в локальный "вверх" - Y
            transform.up = direction.normalized * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, _target.position, stepLength);
        }
        else
        {
            base.Movement(stepLength);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (_timer >= _timerFindTarget)
        {
            FindTarget();
        }
    }

    private void FindTarget()
    {
        Transform minDistanceTarget = null;

        Vector2 boxPos = transform.position + (transform.up * _centerZoneFind);
        Vector2 boxSize = new Vector2(_widthZoneFind, _heightZoneFind);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxPos, boxSize, transform.rotation.eulerAngles.z);

        float minDistance = float.MaxValue;
        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider.name);
            if (collider.transform.root.GetComponent<Entity>() != null && collider.transform.root.GetComponent<Destructible>() != _parent)
            {

                Transform transformTarget = collider.transform.root;
                float distance = Vector2.Distance(transformTarget.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minDistanceTarget = transformTarget;
                }
                

            }
        }
        if (minDistanceTarget != null)
        {
            SetTarget(minDistanceTarget);
        }

    }

    private void SetTarget(Transform target)
    {
        if (_target == null)
        {
            _target = target;
            Debug.Log(target.name);
        }
        else return;
    }

    private void Explosion(Vector2 pos, float radiusExplosion)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radiusExplosion);

        foreach (Collider2D collider in colliders)
        {
            if (collider.transform.root.TryGetComponent(out Destructible dest))
            {
                Vector2 dir = (Vector2)collider.transform.position - pos;
                float finalDamage = _damage / (dir.magnitude / _coefficientFinalDamage);
                Debug.Log(" По объекту: " + collider.transform.root.name + " Нанесён урон: " + (int)finalDamage + " коэф (магнитуда): " + dir.magnitude);
                if (collider.transform.root.TryGetComponent(out Rigidbody2D rb))
                {
                    rb.AddForce(dir.normalized * _explosionForce, ForceMode2D.Impulse);
                }
                if (dest != _parent)
                {

                    dest.ApplyDamage((int)finalDamage);

                    if (dest.CurrentHitPoints <= 0)
                    {
                        if (_parent == Player.Instance.ActiveShip)
                        {
                            Player.Instance.AddScore(dest.ScoreValue);

                            if (dest is SpaceShip)
                            {
                                Player.Instance.AddKill();
                            }
                        }
                    }
                }
            }
        }
        Instantiate(_explosionPrefabs, transform.position, transform.rotation);
    }

    protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
    {
        base.OnProjectileLifeEnd(col, pos);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 boxPos = transform.position + (transform.up * _centerZoneFind);
        Vector2 boxSize = new Vector2(_widthZoneFind, _heightZoneFind);

        Gizmos.color = Color.red;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxPos, boxSize, transform.rotation.eulerAngles.z);

        foreach (Collider2D collider in colliders)
        {
            Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
        }

        Handles.color = new Color(0, 1, 0, 0.3f);
        Handles.DrawSolidDisc(transform.position, transform.forward, _radiusExplosion);
    }
#endif
}
