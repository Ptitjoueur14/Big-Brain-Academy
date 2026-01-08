using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menus
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

        public void OnBackArrowButtonClicked()
        {
            gameObject.SetActive(false);
        }

        public void OnEasyDifficultyButtonClicked()
        {
            GameManager.Instance.difficultyLevel = DifficultyLevel.Easy;
        }
        
        public void OnMediumDifficultyButtonClicked()
        {
            GameManager.Instance.difficultyLevel = DifficultyLevel.Medium;
        }
        
        public void OnHardDifficultyButtonClicked()
        {
            GameManager.Instance.difficultyLevel = DifficultyLevel.Hard;
        }
        
        public void OnExpertDifficultyButtonClicked()
        {
            GameManager.Instance.difficultyLevel = DifficultyLevel.Expert;
        }

        public void OnStartButtonClicked()
        {
            SceneManager.LoadScene("GameLevel");
        }

        public void OnBalloonBurstButtonClicked()
        {
            GameManager.Instance.gameLevel = GameLevel.BalloonBurst;
        }
        
        public void OnMalletMathButtonClicked()
        {
            GameManager.Instance.gameLevel = GameLevel.MalletMath;
        }
        
        public void OnColorCountButtonClicked()
        {
            GameManager.Instance.gameLevel = GameLevel.ColorCount;
        }
        
        public void OnTickTockTurnButtonClicked()
        {
            GameManager.Instance.gameLevel = GameLevel.TickTockTurn;
        }
    }
}