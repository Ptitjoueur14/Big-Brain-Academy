using UnityEngine;

namespace Menus.LevelSelectionMenu
{
    public class LevelButton : MonoBehaviour
    {
        public GameLevel gameLevel;

        public void OnLevelButtonClicked()
        {
            GameManager.Instance.gameLevel = gameLevel;
        }
    }
}