using System.Collections.Generic;
using UnityEngine;

namespace Modes.Stretching
{
    [System.Serializable]
    public class GameScoreEntry
    {
        public GameLevel gameLevel;
        public List<DifficultyScoreEntry> difficultyScores = new();
    }
    
    [System.Serializable]
    public class DifficultyScoreEntry
    {
        public DifficultyLevel difficultyLevel;
        public int bestBrainMass;
        public MedalType bestMedal;
    }
    
    [CreateAssetMenu(fileName = "BrainScoreDatabase", menuName = "Game/Brain Score Database")]
    public class BrainScoreDatabase : ScriptableObject
    {
        public List<GameScoreEntry> gameScores = new();

        public void RegisterScore(GameLevel gameLevel, DifficultyLevel difficultyLevel, int brainMass, MedalType medal)
        {
            GameScoreEntry gameScoreEntry = gameScores.Find(entry => entry.gameLevel == gameLevel);

            if (gameScoreEntry == null)
            {
                DifficultyScoreEntry newDifficultyScoreEntry = new DifficultyScoreEntry
                {
                    difficultyLevel = difficultyLevel,
                    bestBrainMass = brainMass,
                    bestMedal = medal,
                };
                GameScoreEntry newGameScoreEntry = new GameScoreEntry
                {
                    gameLevel = gameLevel,
                };
                newGameScoreEntry.difficultyScores.Add(newDifficultyScoreEntry);
                Debug.Log($"New game entry added for game {gameLevel} in difficulty {difficultyLevel} : new brain mass of {brainMass} g with new medal of {medal}");
            }

            else
            {
                DifficultyScoreEntry difficultyScoreEntry = gameScoreEntry.difficultyScores.Find(entry => entry.difficultyLevel == difficultyLevel);
                if (difficultyScoreEntry == null)
                {
                    DifficultyScoreEntry newDifficultyScoreEntry = new DifficultyScoreEntry
                    {
                        difficultyLevel = difficultyLevel,
                        bestBrainMass = brainMass,
                        bestMedal = medal,
                    };
                    gameScoreEntry.difficultyScores.Add(newDifficultyScoreEntry);
                    Debug.Log($"New difficulty entry added for game {gameLevel} in difficulty {difficultyLevel} : new brain mass of {brainMass} g with new medal of {medal}");
                }
                else
                {
                    if (brainMass > difficultyScoreEntry.bestBrainMass)
                    {
                        Debug.Log($"New best brain mass for game {gameLevel} in difficulty {difficultyLevel} : {difficultyScoreEntry.bestBrainMass} g -> {brainMass} g");
                        difficultyScoreEntry.bestBrainMass = brainMass;
                    }

                    if (medal != MedalType.None && medal > difficultyScoreEntry.bestMedal)
                    {
                        Debug.Log($"New best medal for game {gameLevel} in difficulty {difficultyLevel} : {difficultyScoreEntry.bestMedal} -> {medal}");
                    }
                }
            }
        }
        
        
        
    }
}