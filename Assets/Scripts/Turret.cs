using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretMode _mode;
    public TurretMode Mode => _mode;

    [SerializeField] private TurretProperties _turretProperties;

    private float _refireTimer;

    public bool CanFiire => _refireTimer <= 0;

    private SpaceShip _ship;

    private void Start()
    {
        _ship = transform.root.GetComponent<SpaceShip>();
    }

    private void Update()
    {
        if (_refireTimer > 0)
            _refireTimer -= Time.deltaTime;
    }

    //
    public void Fire()
    {
        if (_turretProperties == null) return;

        if (_refireTimer > 0) return;

        ProjectileBase projectile = Instantiate(_turretProperties.ProjectilePrefab).GetComponent<ProjectileBase>();
        projectile.transform.position = transform.position;
        projectile.transform.up = transform.up;

        projectile.SetPatentShooter(_ship);

        _refireTimer = _turretProperties.RateOfFire;

        {
            // SFX
        }
    }

    public void AssignLoadout(TurretProperties props)
    {
        if (_mode != props.Mode) return;

        _refireTimer = 0;
        _turretProperties = props;
    }
}
