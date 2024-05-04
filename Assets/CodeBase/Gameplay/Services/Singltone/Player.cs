using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        public static SpaceShip SelectedSpaceShip;

        [SerializeField] private int _numLives;

        public int NumLives => _numLives;

        private SpaceShip _ship;
        [SerializeField] private SpaceShip _playerShipPrefab;

        public SpaceShip ActiveShip => _ship;

        private FollowCamera _cameraController;
        private ShipInputController _shipInputController;
        private Transform _spawnPoint;

        public void Construct(FollowCamera followCamera, ShipInputController shipInputController, Transform spawnPoint)
        {
            _cameraController = followCamera;
            _shipInputController = shipInputController;
            _spawnPoint = spawnPoint;
        }

        private int _score;
        private int _numKills;

        public int Score => _score;
        public int NumKills => _numKills;
        public SpaceShip ShipPrefab
        {
            get
            {
                if (SelectedSpaceShip == null)
                {
                    return _playerShipPrefab;
                }
                else
                {
                    return SelectedSpaceShip;
                }
            }
        }

        private void Start()
        {
            Respawn();
        }

        private void OnShipDeath()
        {
            _numLives--;

            if (_numLives > 0)
                Respawn();
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(ShipPrefab);

            _ship = newPlayerShip.GetComponent<SpaceShip>();

            _ship.EventOnDeath.AddListener(OnShipDeath);

            _cameraController.SetTarget(_ship.transform);
            _shipInputController.SetTargetShip(_ship);

        }

        public void AddKill()
        {
            _numKills += 1;
        }

        public void AddScore(int num)
        {
            _score += num;
        }
    }
}

