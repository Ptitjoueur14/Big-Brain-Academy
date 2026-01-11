using System;
using System.Linq;
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
        public Sprite medalSprite;
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
                medalSprite = levelSelectionMenu.noMedalSprite;
                brainMassText.text = "0 g";
                brainMassText.color = new Color(0.3f, 0.3f, 0.3f);
                Debug.Log($"Found no difficulty entry for game {GameManager.Instance.gameLevel} in difficulty {difficultyLevel}");
                return;
            }

            // Disable expert difficulty if Hard difficulty doesn't have Gold medal
            if (difficultyLevel == DifficultyLevel.Hard)
            {
                if (difficultyScoreEntry.bestMedal < MedalType.Gold)
                {
                    levelSelectionMenu.difficultyButtons.Last().gameObject.SetActive(false);
                }
                else
                {
                    levelSelectionMenu.difficultyButtons.Last().gameObject.SetActive(true);
                }
            }

            brainMassText.text = difficultyScoreEntry.bestBrainMass + " g";
            switch (difficultyScoreEntry.bestMedal)
            {
                case MedalType.Platinum:
                    medalSprite = levelSelectionMenu.platinumMedalSprite;
                    brainMassText.color = Color.cyan;
                    break;
                case MedalType.Gold:
                    medalSprite = levelSelectionMenu.goldMedalSprite;
                    brainMassText.color = Color.yellow;
                    break;
                case MedalType.Silver:
                    medalSprite = levelSelectionMenu.silverMedalSprite;
                    brainMassText.color = Color.gray;
                    break;
                case MedalType.Bronze:
                    medalSprite = levelSelectionMenu.bronzeMedalSprite;
                    brainMassText.color = new Color(0.8f, 0.5f, 0f);
                    break;
                case MedalType.None:
                    medalSprite = levelSelectionMenu.noMedalSprite;
                    brainMassText.color = Color.black;
                    break;
            }

            medalImage.sprite = medalSprite;
            Debug.Log($"Found difficulty entry for game {GameManager.Instance.gameLevel} in difficulty {difficultyLevel} : Found {difficultyScoreEntry.bestMedal} medal with {difficultyScoreEntry.bestBrainMass} g");
        }
    }
}