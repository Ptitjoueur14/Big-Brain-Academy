using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus.LevelSelectionMenu
{
    public class LevelSelectionMenu : MonoBehaviour
    {
        [Header("Level Category Menu")]
        public List<GameObject> levelCategoryMenus; // A separate menu for each category 
        public GameObject identificationCategoryMenu;
        public GameObject memoryCategoryMenu;
        public GameObject analysisCategoryMenu;
        public GameObject mathsCategoryMenu;
        public GameObject perceptionCategoryMenu;
        
        [Header("Difficulty Selection Menu")]
        public GameObject difficultySelectionMenu;
        public TMP_Text gameLevelNameText; // The name of the game level selected shown at the top
        public List<DifficultyButton> difficultyButtons;
        
        [Header("Medal Sprites")]
        public Sprite noMedalSprite;
        public Sprite bronzeMedalSprite;
        public Sprite silverMedalSprite;
        public Sprite goldMedalSprite;
        public Sprite platinumMedalSprite;
        
        private void Start()
        {
            DisableAllCategoryMenus();
            switch (GameManager.Instance.gameCategory)
            {
                case GameCategory.Identification:
                    identificationCategoryMenu.SetActive(true);
                    break;
                case GameCategory.Memory:
                    memoryCategoryMenu.SetActive(true);
                    break;
                case GameCategory.Analysis:
                    analysisCategoryMenu.SetActive(true);
                    break;
                case GameCategory.Maths:
                    mathsCategoryMenu.SetActive(true);
                    break;
                case GameCategory.Perception:
                    perceptionCategoryMenu.SetActive(true);
                    break;
            }
        }

        public void DisableAllCategoryMenus()
        {
            foreach (GameObject levelCategoryMenu in levelCategoryMenus)
            {
                levelCategoryMenu.SetActive(false);
            }
        }

        public void OnBackArrowButtonClickedMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnBackArrowButtonClickedLevelSelectionMenu()
        {
            difficultySelectionMenu.SetActive(false);
        }

        public void UpdateBrainMassAndText()
        {
            foreach (DifficultyButton difficultyButton in difficultyButtons)
            {
                difficultyButton.UpdateBrainMassTextAndMedal();
            }
        }

        public void OnStartButtonClicked()
        {
            SceneManager.LoadScene("GameLevel");
        }
    }
}