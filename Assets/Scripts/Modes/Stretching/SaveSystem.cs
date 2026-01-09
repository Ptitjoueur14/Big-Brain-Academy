using System.Net.Mime;
using UnityEngine;
using UnityEngine.Windows;
using File = System.IO.File;

namespace Modes.Stretching
{
    public static class SaveSystem
    {
        private static string Path => Application.persistentDataPath + "/brainScores.json";

        public static void Save(BrainScoreDatabase db)
        {
            string json = JsonUtility.ToJson(db, true);
            File.WriteAllText(Path, json);
        }

        public static void Load(BrainScoreDatabase db)
        {
            if (!File.Exists(Path))
            {
                Debug.LogWarning("No save file found");
                return;
            }
            
            string json = File.ReadAllText(Path);
            JsonUtility.FromJsonOverwrite(json, db);
        }
    }
}