using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LivesIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _icon;

        private float _lastLives;

        private void Start()
        {
            _icon.sprite = Player.Instance.ActiveShip.PreviewImage;
        }

        private void Update()
        {
            int Lives = Player.Instance.NumLives;

            if (Lives != _lastLives)
            {
                _text.text = Lives.ToString();
                _lastLives = Lives;
            }



        }
    }
}

