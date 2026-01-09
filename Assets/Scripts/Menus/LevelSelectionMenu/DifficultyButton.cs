using UnityEngine;

namespace Menus.LevelSelectionMenu
{
    public class DifficultyButton : MonoBehaviour
    {
        public DifficultyLevel difficultyLevel;

        public void OnDifficultyButtonClicked()
        {
            GameManager.Instance.difficultyLevel = difficultyLevel;
        }
    }
}