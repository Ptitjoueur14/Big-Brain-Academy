using System.Collections.Generic;
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
            SceneManager.LoadScene("MainMenu");
        }

        public void OnStartButtonClicked()
        {
            SceneManager.LoadScene("GameLevel");
        }
    }
}