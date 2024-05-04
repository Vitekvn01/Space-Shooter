using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;


        // Start is called before the first frame update
        void Start()
        {
            _panel.SetActive(false);
            Time.timeScale = 1;
        }

        public void ShowPause()
        {
            _panel.SetActive(true);
            Time.timeScale = 0;
        }

        public void HidePause()
        {
            _panel.SetActive(false);
            Time.timeScale = 1;
        }


        public void LoadMainMenu()
        {
            _panel.SetActive(true);
            Time.timeScale = 1;

            SceneManager.LoadScene(0);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

