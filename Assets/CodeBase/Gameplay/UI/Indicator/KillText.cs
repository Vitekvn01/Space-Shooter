using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class KillText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private float _lastNumKills;

        private void Update()
        {
            int numKills = Player.Instance.NumKills;

            if (numKills != _lastNumKills)
            {
                _text.text = "Kill: " + numKills.ToString();
                _lastNumKills = numKills;
            }



        }
    }
}

