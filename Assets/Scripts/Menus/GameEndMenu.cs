using Modes.Stretching;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menus
{
    public class GameEndMenu : MonoBehaviour
    {
        [Header("Stretching")]
        public Stretching stretching;
        
        public TMP_Text categoryText;
        public TMP_Text levelText;
        public TMP_Text difficultyText;
        
        public TMP_Text errorsText;
        
        public Sprite medalSprite;
        public TMP_Text brainMassText;
        
        [Header("Medal Sprites")]
        public Sprite noMedalSprite;
        public Sprite bronzeMedalSprite;
        public Sprite silverMedalSprite;
        public Sprite goldMedalSprite;
        public Sprite platinumMedalSprite;
        
        public void Start()
        {
            stretching = FindFirstObjectByType<Stretching>();
            UpdateTitleTexts();
            UpdateMedalAndBrainMass();
            errorsText.text = stretching.errors.ToString();
        }

        public void UpdateTitleTexts()
        {
            GameCategory gameCategory = GameManager.Instance.gameCategory;
            categoryText.text = Category.TranslateGameCategoryToFrench(gameCategory);
            switch (gameCategory)
            {
                case GameCategory.Identification:
                    categoryText.color = new Color(0f, 0.5f, 0f);
                    break;
                case GameCategory.Memory:
                    categoryText.color = new Color(0.7f, 0f, 1f);
                    break;
                case GameCategory.Maths:
                    categoryText.color = Color.cyan;
                    break;
                case GameCategory.Analysis:
                    categoryText.color = new Color(1f, 0.2f, 0f);
                    break;
                case GameCategory.Perception:
                    categoryText.color = new Color(1f, 0.7f, 0f);
                    break;
            }
            levelText.text = Level.TranslateGameLevelToFrench(GameManager.Instance.gameLevel);
            DifficultyLevel difficultyLevel = GameManager.Instance.difficultyLevel;
            difficultyText.text = Difficulty.TranslateDifficultyLevelToFrench(difficultyLevel);
            switch (difficultyLevel)
            {
                case DifficultyLevel.Easy:
                    difficultyText.color = Color.green;
                    break;
                case DifficultyLevel.Medium:
                    difficultyText.color = Color.yellow;
                    break;
                case DifficultyLevel.Hard:
                    difficultyText.color = Color.red;
                    break;
                case DifficultyLevel.Expert:
                    difficultyText.color = Color.black;
                    break;
            }
        }

        public void UpdateMedalAndBrainMass()
        {
            switch (stretching.obtainedMedal)
            {
                case MedalType.Platinum:
                    medalSprite = platinumMedalSprite;
                    brainMassText.color = Color.cyan;
                    break;
                case MedalType.Gold:
                    medalSprite = goldMedalSprite;
                    brainMassText.color = Color.yellow;
                    break;
                case MedalType.Silver:
                    medalSprite = silverMedalSprite;
                    brainMassText.color = Color.gray;
                    break;
                case MedalType.Bronze:
                    medalSprite = bronzeMedalSprite;
                    brainMassText.color = new Color(1f, 0.5f, 0f);
                    break;
                case MedalType.None:
                    // TODO : Text for case None
                    medalSprite = noMedalSprite;
                    brainMassText.color = Color.black;
                    break;
            }
            
            brainMassText.text = stretching.brainMass + " g";
        }

        public void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene("MainMenu");
            // TODO: Destroy Stretching from DontDestroyOnLoad
            Destroy(stretching.gameObject);
        }
    }
}