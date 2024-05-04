using Common;
using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBuilder : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _playerHUDPrefab;
        [SerializeField] private GameObject _levelGUIPrefab;
        [SerializeField] private GameObject _backgroundPrefab;

        [Header("Dependencies")]
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private LevelBoundary _levelBoundary;
        [SerializeField] private LevelController _levelController;

        private void Awake()
        {
            _levelBoundary.Init();
            _levelController.Init();

            Player player = _playerSpawner.Spawn();

            player.Init();

            Instantiate(_playerHUDPrefab);
            Instantiate(_levelGUIPrefab);

            GameObject background = Instantiate(_backgroundPrefab);
            background.GetComponent<SyncTransform>().SetTarget(player.FollowCamera.transform);

        }
    }
}

