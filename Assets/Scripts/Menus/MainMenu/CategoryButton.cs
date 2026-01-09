using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus.MainMenu
{
    public class CategoryButton : MonoBehaviour
    {
        public GameCategory gameCategory;

        public void OnCategoryButtonClicked()
        {
            GameManager.Instance.gameCategory = gameCategory;
            SceneManager.LoadScene("LevelSelectionMenu");
        }
    }
}