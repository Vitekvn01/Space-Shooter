using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _levelSelectionPanel;
        [SerializeField] private GameObject _shipSelectionPanel;

        private void Start()
        {
            ShowMainPanel();
        }

        public void ShowMainPanel()
        {
            _mainPanel.SetActive(true);
            _shipSelectionPanel.SetActive(false);
            _levelSelectionPanel.SetActive(false);
        }

        public void ShowShipSelectionPanel()
        {
            _shipSelectionPanel.SetActive(true);
            _mainPanel.SetActive(false);
        }

        public void ShowLevelSelection()
        {
            _levelSelectionPanel.SetActive(true);
            _mainPanel.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

