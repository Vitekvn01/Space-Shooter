using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ShipSelectionButton : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private SpaceShip _prefab;

        [SerializeField] private Text _shipName;
        [SerializeField] private Text _hitPoints;
        [SerializeField] private Text _speed;
        [SerializeField] private Text _agility;
        [SerializeField] private Image _preview;

        private void Start()
        {
            if (_prefab == null) return;

            _shipName.text = _prefab.Nickname;
            _hitPoints.text = "HP : " + _prefab.MaxHitPoints.ToString();
            _speed.text = "Speed : " + _prefab.MaxLinearVelocity.ToString();
            _agility.text = "Agility : " + _prefab.MaxAngularVelocity.ToString();
            _preview.sprite = _prefab.PreviewImage;
        }

        public void SelectShip()
        {
            Player.SelectedSpaceShip = _prefab;
            _mainMenu.ShowMainPanel();
        }
    }
}

