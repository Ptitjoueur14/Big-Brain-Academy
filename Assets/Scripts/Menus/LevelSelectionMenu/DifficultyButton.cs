using System;
using JetBrains.Annotations;
using Modes.Stretching;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menus.LevelSelectionMenu
{
    public class DifficultyButton : MonoBehaviour
    {
        [Header("Difficulty")]
        public DifficultyLevel difficultyLevel;
        
        [Header("UI Elements")] 
        public Image medalImage;
        public TMP_Text brainMassText;
        
        [Header("Level Selection Menu")]
        public LevelSelectionMenu levelSelectionMenu;
        
        public void OnDifficultyButtonClicked()
        {
            GameManager.Instance.difficultyLevel = difficultyLevel;
        }

        public DifficultyScoreEntry GetDifficultyScoreEntry()
        {
            BrainScoreDatabase brainScoreDatabase = GameManager.Instance.brainScoreDatabase;
            return brainScoreDatabase.FindDifficultyScoreEntry(GameManager.Instance.gameLevel, difficultyLevel);
        }

        public void UpdateBrainMassTextAndMedal()
        {
            DifficultyScoreEntry difficultyScoreEntry = GetDifficultyScoreEntry();
            if (difficultyScoreEntry == null)
            {
                medalImage.sprite = levelSelectionMenu.noMedalSprite;
                brainMassText.text = "0 g";
                brainMassText.color = new Color(0.3f, 0.3f, 0.3f);
                Debug.Log($"Found no difficulty entry for game {GameManager.Instance.gameLevel} in difficulty {difficultyLevel}");
                return;
            }

            brainMassText.text = difficultyScoreEntry.bestBrainMass + " g";
            switch (difficultyScoreEntry.bestMedal)
            {
                case MedalType.Platinum:
                    medalImage.sprite = levelSelectionMenu.platinumMedalSprite;
                    brainMassText.color = Color.cyan;
                    break;
                case MedalType.Gold:
                    medalImage.sprite = levelSelectionMenu.goldMedalSprite;
                    brainMassText.color = Color.yellow;
                    break;
                case MedalType.Silver:
                    medalImage.sprite = levelSelectionMenu.silverMedalSprite;
                    brainMassText.color = Color.gray;
                    break;
                case MedalType.Bronze:
                    medalImage.sprite = levelSelectionMenu.bronzeMedalSprite;
                    brainMassText.color = new Color(1f, 0.5f, 0f);
                    break;
                case MedalType.None:
                    medalImage.sprite = levelSelectionMenu.noMedalSprite;
                    brainMassText.color = Color.black;
                    break;
            }
            Debug.Log($"Found difficulty entry for game {GameManager.Instance.gameLevel} in difficulty {difficultyLevel} : Found {difficultyScoreEntry.bestMedal} medal with {difficultyScoreEntry.bestBrainMass} g");
        }
    }
}