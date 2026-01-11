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
        
        [Header("Leaderboard Ranking")]
        public DifficultyLeaderboardRanking difficultyLeaderboardRanking;
        public LeaderboardRank leaderboardRank;
        public TMP_Text rankText;
        public TMP_Text brainMassText;
        public TMP_Text errorsText;
        public GameObject leftArrowButton;
        public GameObject rightArrowButton;
        
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
            
            LeaderboardRanking leaderboardRanking = GameManager.Instance.leaderboardRanking;
            difficultyLeaderboardRanking =
                leaderboardRanking.FindDifficultyLeaderboardRanking(GameManager.Instance.gameLevel,
                    GameManager.Instance.difficultyLevel);
            leaderboardRank = difficultyLeaderboardRanking.leaderboardRankings.Find(rank => rank.brainMass == stretching.brainMass && rank.errors == stretching.errors);
            UpdateLeaderboardRank(leaderboardRank);
            UpdateArrowButtons();
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

        public void UpdateLeaderboardRank(LeaderboardRank rank)
        {
            rankText.text = rank.rank.ToString();
            brainMassText.text = rank.brainMass + " g";
            UpdateBrainMass(rank.brainMass);
            errorsText.text = rank.errors.ToString();
        }

        public void UpdateBrainMass(int brainMass)
        {
            switch (brainMass)
            {
                case >= 400:
                    brainMassText.color = Color.cyan;
                    break;
                case >= 300:
                    brainMassText.color = Color.yellow;
                    break;
                case >= 200:
                    brainMassText.color = Color.gray;
                    break;
                case >= 100:
                    brainMassText.color = new Color(1f, 0.5f, 0f);
                    break;
                case < 100:
                    // TODO : Text for case None
                    brainMassText.color = Color.black;
                    break;
            }
        }

        public void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene("MainMenu");
            // TODO: Destroy Stretching from DontDestroyOnLoad
            Destroy(stretching.gameObject);
        }

        public void OnLeftArrowButtonClicked()
        {
            int previousRank = leaderboardRank.rank;
            leaderboardRank = difficultyLeaderboardRanking.leaderboardRankings[previousRank - 2];
            UpdateLeaderboardRank(leaderboardRank);
            UpdateArrowButtons();
        }
        
        public void OnRightArrowButtonClicked()
        {
            int previousRank = leaderboardRank.rank;
            leaderboardRank = difficultyLeaderboardRanking.leaderboardRankings[previousRank];
            UpdateLeaderboardRank(leaderboardRank);
            UpdateArrowButtons();
        }

        public void UpdateArrowButtons()
        {
            if (leaderboardRank.rank == 1)
            {
                leftArrowButton.SetActive(false);
            }

            if (leaderboardRank.rank < difficultyLeaderboardRanking.leaderboardRankings.Count)
            {
                rightArrowButton.SetActive(true);
            }
            
            if (leaderboardRank.rank > 1)
            {
                leftArrowButton.SetActive(true);
            }
            
            if (leaderboardRank.rank == difficultyLeaderboardRanking.leaderboardRankings.Count)
            {
                rightArrowButton.SetActive(false);
            }
        }
    }
}