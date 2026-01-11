using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modes.Stretching
{
    public enum MedalType
    {
        None, // No medal
        Bronze, // >= 100 g
        Silver, // >= 200 g
        Gold, // >= 300 g
        Platinum, // >= 400 g
    }
    
    public class Stretching : MonoBehaviour
    {
        [Header("Brain Mass")]
        public List<int> brainMassList; // The list containing the 10 brain masses of the Stretching game
        public int brainMass; // The average brain mass of the brain mass list
        public int maxBrainMassListCount = 10; // 10 levels per Stretching game

        [Header("Game Stats")] 
        public TMP_Text remainingLevelsText;
        public int remainingLevels = 10;
        public int errors;

        [Header("Medals")]
        public MedalType obtainedMedal; // The medal obtained in the Stretching Game
        public GameObject platinumMedalPrefab; // Medal awarded on >= 400 g average (only awarded if gold is already obtained)
        public GameObject goldMedalPrefab; // Medal awarded on >= 300 g average
        public GameObject silverMedalPrefab; // Medal awarded on >= 200 g average
        public GameObject bronzeMedalPrefab; // Medal awarded on >= 100 g average
        public Animator medalAnimator;

        // Calculate the average brain mass from the list
        public int CalculateBrainMassAverage()
        {
            int totalBrainMass = 0;
            foreach (int mass in brainMassList)
            {
                totalBrainMass += mass;
            }
            totalBrainMass /= brainMassList.Count;
            return totalBrainMass;
        }

        // Determine the type of medal to be awarded based on the average brain mass of the game
        public MedalType DetermineMedal()
        {
            if (brainMass >= 400)
            {
                // TODO: Check if gold medial is unlocked for the selected game level
                return MedalType.Platinum;
            }
            if (brainMass >= 300)
            {
                return MedalType.Gold;
            }
            if (brainMass >= 200)
            {
                return MedalType.Silver;
            }
            if (brainMass >= 100)
            {
                return MedalType.Bronze;
            }
            return MedalType.None; // No medal
        }

        public void AddBrainMass(int mass)
        {
            if (brainMassList.Count < maxBrainMassListCount)
            {
                brainMassList.Add(mass);
            }

            if (brainMassList.Count == maxBrainMassListCount)
            {
                Debug.Log("Got 10 brain masses. Finishing stretching game...");
                FinishStretchingGame();
            }
        }

        public void FinishStretchingGame()
        {
            brainMass = CalculateBrainMassAverage();
            obtainedMedal = DetermineMedal();
            Debug.Log($"Got medal {obtainedMedal.ToString()} and mass {brainMass}.");
            switch (obtainedMedal)
            {
                // TODO : Show medal and add to Medal database for each game
                case MedalType.Platinum:
                    break;
                case MedalType.Gold:
                    break;
                case MedalType.Silver:
                    break;
                case MedalType.Bronze:
                    break;
                case MedalType.None: // No medals ( < 100 g)
                    break;
            }
            
            GameManager.Instance.brainScoreDatabase.RegisterScore(GameManager.Instance.gameLevel, GameManager.Instance.difficultyLevel, brainMass, obtainedMedal);
            GameManager.Instance.SaveDatabase();
            
            // Go to end scene
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("GameEndMenu");
        }

        public void DecreaseRemainingLevels()
        {
            remainingLevels--;
            remainingLevelsText.text = remainingLevels.ToString();
        }
    }
}