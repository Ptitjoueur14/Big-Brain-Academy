using JetBrains.Annotations;
using Modes.Stretching;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menus.LevelSelectionMenu
{
    public class LevelButton : MonoBehaviour
    {
        public GameLevel gameLevel;
        public GameObject difficultySelectionMenu;
        
        public LevelSelectionMenu levelSelectionMenu;

        public void OnLevelButtonClicked()
        {
            GameManager.Instance.gameLevel = gameLevel;
            difficultySelectionMenu.SetActive(true);
            levelSelectionMenu.UpdateBrainMassAndText();
            string gameLevelName = Level.TranslateGameLevelToFrench(gameLevel);
            levelSelectionMenu.gameLevelNameText.text = gameLevelName;
        }
    }
}