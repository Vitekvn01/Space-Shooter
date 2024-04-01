using UnityEngine;


namespace SpaceShooter
{
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        [SerializeField] private TurretMode _mode;
        public TurretMode Mode => _mode;

        [SerializeField] private ProjectileBase _projectilePrefab;
        public ProjectileBase ProjectilePrefab => _projectilePrefab;

        [SerializeField] private float _rateOfFire;
        public float RateOfFire => _rateOfFire;

        [SerializeField] private int _energyUsage;
        public int EnergyUsage => _energyUsage;

        [SerializeField] private int _ammoUsage;
        public int AmmoUsage => _ammoUsage;

        [SerializeField] private AudioClip _launchSFX;
        public AudioClip LaunchSFX => _launchSFX;
    }
}

