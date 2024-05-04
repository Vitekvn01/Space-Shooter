using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelController : SingletonBase<LevelController>
    {
        private const string MainMenuSceneName = "main_menu";

        public event UnityAction LevelPassed;
        public event UnityAction LevelLost;

        [SerializeField] private LevelProperties _levelProperties;
       [SerializeField] private LevelCondition[] _conditions;

        private bool _IsLevelCompleted;
        private float _levelTime;
        public float LevelTime => _levelTime;

       public bool HasNextLevel => _levelProperties.NextLevel != null;

        private void Start()
        {
            Time.timeScale = 1;
            _levelTime = 0;
        }

        private void Update()
        {
            if (_IsLevelCompleted == false)
            {
                _levelTime += Time.deltaTime;
                CheckLevelConditions();
            }

            if (Player.Instance.NumLives == 0)
            {
                Lose();
            }
        }

        private void CheckLevelConditions()
        {
            int numCompleted = 0;

            for (int i = 0; i < _conditions.Length; i++)
            {
                if (_conditions[i].IsCompleted == true)
                {
                    numCompleted++;
                }
            }

            if (numCompleted == _conditions.Length)
            {
                _IsLevelCompleted = true;

                Pass();
            }
        }

        private void Lose()
        {
            LevelLost?.Invoke();
            Time.timeScale = 0;
        }

        private void Pass()
        {
            LevelPassed?.Invoke();
            Time.timeScale = 0;
        }

        public void LoadNextLevel()
        {
            if (HasNextLevel == true)
                SceneManager.LoadScene(_levelProperties.NextLevel.SceneName);
            else
                SceneManager.LoadScene(MainMenuSceneName);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(_levelProperties.SceneName);
        }
    }
}

