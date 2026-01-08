using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        public LevelSelectionMenu levelSelectionMenu;

        private void Start()
        {
            levelSelectionMenu.gameObject.SetActive(false);
        }

        public void OnIdentificationCategoryButtonClicked()
        {
            GameManager.Instance.gameCategory = GameCategory.Identification;
            OpenLevelSelectionMenu();
        }
        
        public void OnMemoryCategoryButtonClicked()
        {
            GameManager.Instance.gameCategory = GameCategory.Memory;
            OpenLevelSelectionMenu();
        }
        
        public void OnAnalysisCategoryButtonClicked()
        {
            GameManager.Instance.gameCategory = GameCategory.Analysis;
            OpenLevelSelectionMenu();
        }
        
        public void OnMathsCategoryButtonClicked()
        {
            GameManager.Instance.gameCategory = GameCategory.Maths;
            OpenLevelSelectionMenu();
        }
        
        public void OnPerceptionCategoryButtonClicked()
        {
            GameManager.Instance.gameCategory = GameCategory.Perception;
            OpenLevelSelectionMenu();
        }

        // Opens a menu to select the desired category level and the difficulty
        public void OpenLevelSelectionMenu()
        {
            levelSelectionMenu.gameObject.SetActive(true);
        }
    }
}