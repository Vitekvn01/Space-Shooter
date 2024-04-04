using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Meteorite : Destructible
{
    [SerializeField] private bool _debriseOnDestroy;
    [SerializeField] private GameObject _debriseMeteorits;
    [SerializeField] private int _debriseCount;
    [SerializeField] private float _radius;

    protected override void OnDeath()
    {
        if (_debriseOnDestroy == true)
        {
            for (int i = 0; i < _debriseCount; i++)
            {
                float rotation = Random.Range(0, 360);
                Vector2 pos = (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * _radius;
                GameObject debrisMeteorite = Instantiate(_debriseMeteorits, pos, Quaternion.Euler(new Vector3(0, 0, rotation)));
            }
        }

        base.OnDeath();
    }

#if UNITY_EDITOR

    private Color GizmoColor = new Color(0, 1, 0, 0.3f);
    private void OnDrawGizmosSelected()
    {
        Handles.color = GizmoColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
    }

#endif      
}
