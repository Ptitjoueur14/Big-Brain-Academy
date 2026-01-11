using UnityEngine;

public enum GameCategory
{
    Identification,
    Memory,
    Maths,
    Analysis,
    Perception
}

public class Category : MonoBehaviour
{
    public static string TranslateGameCategoryToFrench(GameCategory gameCategory)
    {
        switch (gameCategory)
        {
            case GameCategory.Identification:
                return "Identification";
            case GameCategory.Memory:
                return "Mémoire";
            case GameCategory.Maths:
                return "Maths";
            case GameCategory.Analysis:
                return "Analyse";
            case GameCategory.Perception:
                return "Perception";
        }
        return "";
    }
}
