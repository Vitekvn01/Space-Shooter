using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace SpaceShooter
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _numLives;

        [SerializeField] private SpaceShip _ship;
        [SerializeField] private GameObject _playerShipPrefab;

        [SerializeField] private CameraController _cameraController;
        [SerializeField] private MovementController _movementController;

        private void Start()
        {
            _ship.EventOnDeath.AddListener(OnShopDeath);
        }

        private void OnShopDeath()
        {
            _numLives--;

            if (_numLives > 0)
                Respawn();
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(_playerShipPrefab);

            _ship = newPlayerShip.GetComponent<SpaceShip>();

            _ship.EventOnDeath.AddListener(OnShopDeath);

            _cameraController.SetTarget(_ship.transform);
            _movementController.SetTargetShip(_ship);

        }
    }
}

