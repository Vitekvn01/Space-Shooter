using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanell : MonoBehaviour
    {
        private const string PassedText = "Passed";
        private const string LoseText = "Lose";
        private const string RestartText = "Restart";
        private const string NextText = "Next";
        private const string MainMenuText = "Main menu";
        private const string KillsTextPrefix = "Kills : ";
        private const string ScoreTextPrefix = "Scores : ";
        private const string TimeTextPrefix = "Time : ";

        [SerializeField] private TextMeshProUGUI _kills;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private TextMeshProUGUI _result;
        [SerializeField] private TextMeshProUGUI _buttonNextLevel;

        private bool m_LevelPassed = false;

        private void Start()
        {
            gameObject.SetActive(false);
            LevelController.Instance.LevelLost += OnLevelLost;
            LevelController.Instance.LevelPassed += OnLevelPassed;
        }

        private void OnDestroy()
        {
            LevelController.Instance.LevelLost -= OnLevelLost;
            LevelController.Instance.LevelPassed -= OnLevelPassed;
        }


        private void OnLevelPassed()
        {
            gameObject.SetActive(true);

            m_LevelPassed = true;

            FillLevelStaistics();

            _result.text = PassedText;

            if (LevelController.Instance.HasNextLevel == true)
            {
                _buttonNextLevel.text = NextText;
            }
            else
            {
                _buttonNextLevel.text = MainMenuText;
            }
        }

        private void OnLevelLost()
        {
            gameObject.SetActive(true);

            FillLevelStaistics();

            _result.text = LoseText;
            _buttonNextLevel.text = RestartText;
        }

        private void FillLevelStaistics()
        {
            _kills.text = KillsTextPrefix + Player.Instance.NumKills.ToString();
            _score.text = ScoreTextPrefix + Player.Instance.Score.ToString();
            _time.text = TimeTextPrefix + LevelController.Instance.LevelTime.ToString("F0");
        }
        
        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            if (m_LevelPassed == true)
            {
                    LevelController.Instance.LoadNextLevel();
            }
            else
            {
                LevelController.Instance.RestartLevel();
            }
        }
    }
}

