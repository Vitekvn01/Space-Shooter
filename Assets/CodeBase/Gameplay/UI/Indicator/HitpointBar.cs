using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class HitpointBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private float _lastHitPoints;

        private void Update()
        {
            float hitPoints = ((float)Player.Instance.ActiveShip.CurrentHitPoints / (float)Player.Instance.ActiveShip.MaxHitPoints);

            if (hitPoints != _lastHitPoints)
            {
                _image.fillAmount = hitPoints;
                _lastHitPoints = hitPoints;
            }

        }
    }
}

