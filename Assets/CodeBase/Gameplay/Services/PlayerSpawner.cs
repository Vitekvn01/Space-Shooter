using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private FollowCamera followCameraPrefab;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private ShipInputController _shipInputControllerPrefab;
        [SerializeField] private VirtualGamePad _virtualGamePadPrefab;

        [SerializeField] private Transform _spawnPoint;

        public Player Spawn()
        {
            FollowCamera followCamera = Instantiate(followCameraPrefab);
            VirtualGamePad virtualGamePad = Instantiate(_virtualGamePadPrefab);

            ShipInputController shipInputController = Instantiate(_shipInputControllerPrefab);
            shipInputController.Construct(virtualGamePad);

            Player player = Instantiate(_playerPrefab);
            player.Construct(followCamera, shipInputController, _spawnPoint);

            return player;
        }
    }
}

