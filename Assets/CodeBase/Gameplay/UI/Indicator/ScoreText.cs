using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private float _lastScore;

        private void Update()
        {
            int Score = Player.Instance.Score;

            if (Score != _lastScore)
            {
                _text.text = "Score: " + Score.ToString();
                _lastScore = Score;
            }



        }
    }
}

