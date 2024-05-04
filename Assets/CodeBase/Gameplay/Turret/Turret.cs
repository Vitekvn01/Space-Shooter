using SpaceShooter;
using Common;
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

        if(_ship.DrawEnergy(_turretProperties.EnergyUsage) == false) return;

        if (_ship.DrawAmmo(_turretProperties.AmmoUsage) == false) return;

        ProjectileBase projectileBase = Instantiate(_turretProperties.ProjectilePrefab).GetComponent<ProjectileBase>();
        projectileBase.transform.position = transform.position;
        projectileBase.transform.up = transform.up;

        projectileBase.SetParentShooter(_ship);

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
