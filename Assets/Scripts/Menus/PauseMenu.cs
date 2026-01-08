using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class PauseMenu : MonoBehaviour
    {
        public bool isPauseMenuActive;
        public bool canPauseGame; // If the game is paused when the pause menu is active
        public GameObject pauseMenu;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPauseMenuActive = !isPauseMenuActive;
                pauseMenu.SetActive(isPauseMenuActive);
                if (canPauseGame)
                {
                    if (isPauseMenuActive)
                    {
                        Time.timeScale = 0;
                    }
                    else
                    {
                        Time.timeScale = 1;
                    }
                }
            }
        }

        public void OnMainMenuButtonClicked()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}