using System.Net.Mime;
using UnityEngine;
using UnityEngine.Windows;
using File = System.IO.File;

namespace Modes.Stretching
{
    public static class SaveSystem
    {
        private static string BrainScoresPath => Application.persistentDataPath + "/brainScores.json";
        private static string LeaderboardRankingPath => Application.persistentDataPath + "/leaderboardRanking.json";

        public static void SaveBrainScoreDatabase(BrainScoreDatabase db)
        {
            string json = JsonUtility.ToJson(db, true);
            File.WriteAllText(BrainScoresPath, json);
        }

        public static void LoadBrainScoreDatabase(BrainScoreDatabase db)
        {
            if (!File.Exists(BrainScoresPath))
            {
                Debug.LogWarning("No save file found for brain score database");
                return;
            }
            
            string json = File.ReadAllText(BrainScoresPath);
            JsonUtility.FromJsonOverwrite(json, db);
        }
        
        public static void SaveLeaderboardRanking(LeaderboardRanking ranking)
        {
            string json = JsonUtility.ToJson(ranking, true);
            File.WriteAllText(LeaderboardRankingPath, json);
        }

        public static void LoadLeaderboardRanking(LeaderboardRanking ranking)
        {
            if (!File.Exists(LeaderboardRankingPath))
            {
                Debug.LogWarning("No save file found for leaderboard ranking");
                return;
            }
            
            string json = File.ReadAllText(LeaderboardRankingPath);
            JsonUtility.FromJsonOverwrite(json, ranking);
        }
    }
}