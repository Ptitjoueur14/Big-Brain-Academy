using UnityEngine;

public enum GameLevel // The enum listing all possible levels from each category
{
    // Identification
    Whack,
    
    // Memory
    
    // Analysis
    
    // Maths
    BalloonBurst,
    MalletMath,
    ColorCount,
    TickTockTurn, // Special
    
    // Perception
    
}

public class Level : MonoBehaviour
{
    // Translate the enum name to French for UI
    public static string TranslateGameLevelToFrench(GameLevel gameLevel)
    {
        switch (gameLevel)
        {
            case GameLevel.BalloonBurst:
                return "Compte éclate";
            case GameLevel.MalletMath:
                return "Maillet mathique";
            case GameLevel.ColorCount:
                return "Basket-boules";
            case GameLevel.TickTockTurn:
                return "À la bonne heure";
        }
        
        // TODO : Translate other enums
        return "";
    }
}
