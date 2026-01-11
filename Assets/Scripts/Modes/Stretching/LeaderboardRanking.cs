using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Modes.Stretching
{
    [System.Serializable]
    public class GameLeaderboardRanking
    {
        public GameLevel gameLevel;
        public List<DifficultyLeaderboardRanking> difficultyLeaderboardRankings;

        public GameLeaderboardRanking(GameLevel gameLevel)
        {
            this.gameLevel = gameLevel;
            this.difficultyLeaderboardRankings = new List<DifficultyLeaderboardRanking>();
        }
    }
    
    [System.Serializable]
    public class DifficultyLeaderboardRanking
    {
        public DifficultyLevel difficultyLevel;
        public List<LeaderboardRank> leaderboardRankings;

        public DifficultyLeaderboardRanking(DifficultyLevel difficultyLevel)
        {
            this.difficultyLevel = difficultyLevel;
            this.leaderboardRankings = new List<LeaderboardRank>();
        }

        public void InsertLeaderboardRank(LeaderboardRank leaderboardRank)
        {
            // TODO: Only keep best 5 or 10 rankings for each difficulty ??
            leaderboardRankings.Add(leaderboardRank);
            leaderboardRankings.Sort((rank1, rank2) =>
            {
                if (rank1.brainMass == rank2.brainMass) // If 2 equal brain mass, sort by errors instead
                {
                    return rank1.errors - rank2.errors;
                }
                return rank2.brainMass - rank1.brainMass; // Sort by brain mass
            });

            // Update rank fields for the difficulty leaderboard
            for (int i = 0; i < leaderboardRankings.Count; i++)
            {
                leaderboardRankings[i].rank = i + 1;
            }
        }
    }

    [System.Serializable]
    public class LeaderboardRank
    {
        public int rank;
        public int brainMass;
        public int errors;

        public LeaderboardRank(int rank, int brainMass, int errors)
        {
            this.rank = rank;
            this.brainMass = brainMass;
            this.errors = errors;
        }
    }
    
    [CreateAssetMenu(fileName = "LeaderboardRanking", menuName = "Game/Stretching/Leaderboard Ranking")]
    public class LeaderboardRanking : ScriptableObject
    {
        public List<GameLeaderboardRanking> gameLeaderboardRankings = new();

        public void RegisterRank(GameLevel gameLevel, DifficultyLevel difficultyLevel, int brainMass, int errors)
        {
            GameLeaderboardRanking gameLeaderboardRanking = gameLeaderboardRankings.Find(entry => entry.gameLevel == gameLevel);

            if (gameLeaderboardRanking == null)
            {
                LeaderboardRank newLeaderboardRank = new LeaderboardRank(1, brainMass, errors);
                
                DifficultyLeaderboardRanking newDifficultyLeaderboardRanking = new DifficultyLeaderboardRanking(difficultyLevel);
                newDifficultyLeaderboardRanking.leaderboardRankings.Add(newLeaderboardRank);

                GameLeaderboardRanking newGameLeaderboardRanking = new GameLeaderboardRanking(gameLevel);
                newGameLeaderboardRanking.difficultyLeaderboardRankings.Add(newDifficultyLeaderboardRanking);
                gameLeaderboardRankings.Add(newGameLeaderboardRanking);
                Debug.Log($"New game leaderbaoard added for game {gameLevel} in difficulty {difficultyLevel} : rank 1 with new brain mass of {brainMass} g and {errors} errors");
            }

            else
            {
                DifficultyLeaderboardRanking difficultyLeaderboardRanking = gameLeaderboardRanking.difficultyLeaderboardRankings.Find(entry => entry.difficultyLevel == difficultyLevel);
                if (difficultyLeaderboardRanking == null)
                {
                    LeaderboardRank newLeaderboardRank = new LeaderboardRank(1, brainMass, errors);
                    
                    DifficultyLeaderboardRanking newDifficultyLeaderboardRanking = new DifficultyLeaderboardRanking(difficultyLevel);
                    newDifficultyLeaderboardRanking.leaderboardRankings.Add(newLeaderboardRank);
                    
                    gameLeaderboardRanking.difficultyLeaderboardRankings.Add(newDifficultyLeaderboardRanking);
                    Debug.Log($"New difficulty leaderboard added for game {gameLevel} in difficulty {difficultyLevel} : rank 1 with new brain mass of {brainMass} g and {errors} errors");
                }
                else
                {
                    LeaderboardRank newLeaderboardRank = new LeaderboardRank(1, brainMass, errors);
                    difficultyLeaderboardRanking.InsertLeaderboardRank(newLeaderboardRank);
                    Debug.Log($"New leaderboard rank added for game {gameLevel} in difficulty {difficultyLevel} : rank {newLeaderboardRank.rank} with new brain mass of {brainMass} g and {errors} errors");
                }
            }
        }
        
        [CanBeNull]
        public GameLeaderboardRanking FindGameLeaderboardRanking(GameLevel gameLevel)
        {
            return gameLeaderboardRankings.Find(entry => entry.gameLevel == gameLevel);
        }

        [CanBeNull]
        public DifficultyLeaderboardRanking FindDifficultyLeaderboardRanking(GameLevel gameLevel,
            DifficultyLevel difficultyLevel)
        {
            GameLeaderboardRanking gameLeaderboardRanking = FindGameLeaderboardRanking(gameLevel);
            if (gameLeaderboardRanking != null)
            {
                return gameLeaderboardRanking.difficultyLeaderboardRankings.Find(entry => entry.difficultyLevel == difficultyLevel);
            }
            return null;
        }
    }
}