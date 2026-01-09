using System.Collections.Generic;
using UnityEngine;

namespace Modes.Stretching
{
    [System.Serializable]
    public class GameScoreEntry
    {
        public GameLevel gameLevel;
        public int bestBrainMass;
        public MedalType bestMedal;
    }
    
    [CreateAssetMenu(fileName = "BrainScoreDatabase", menuName = "Game/Brain Score Database")]
    public class BrainScoreDatabase : ScriptableObject
    {
        public List<GameScoreEntry> gameScores = new();

        public void RegisterScore(GameLevel gameLevel, int brainMass, MedalType medal)
        {
            GameScoreEntry entry = gameScores.Find(entry => entry.gameLevel == gameLevel);

            if (entry == null)
            {
                GameScoreEntry gameScoreEntry = new GameScoreEntry
                {
                    gameLevel = gameLevel,
                    bestBrainMass = brainMass,
                    bestMedal = medal,
                };
                Debug.Log($"New entry added for game {gameLevel} : new brain mass of {brainMass} g with new medal of {medal}");
                gameScores.Add(gameScoreEntry);
            }

            else
            {
                if (brainMass > entry.bestBrainMass)
                {
                    Debug.Log($"New best brain mass for game {gameLevel} : {entry.bestBrainMass} g -> {brainMass} g");
                    entry.bestBrainMass = brainMass;
                }

                if (medal != MedalType.None && medal > entry.bestMedal)
                {
                    Debug.Log($"New best medal for game {gameLevel} : {entry.bestMedal} -> {medal}");
                }
            }
        }
        
        
        
    }
}