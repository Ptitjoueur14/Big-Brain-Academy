using UnityEngine;

public enum DifficultyLevel
{
    Easy = 1,
    Medium = 2,
    Hard = 3,
    Expert = 4,
}

public class Difficulty : MonoBehaviour
{
    public static string TranslateDifficultyLevelToFrench(DifficultyLevel difficultyLevel)
    {
        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                return "Facile";
            case DifficultyLevel.Medium:
                return "Moyen";
            case DifficultyLevel.Hard:
                return "Difficile";
            case DifficultyLevel.Expert:
                return "Expert";
        }
        return "";
    }
}
