using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Settings")]
    public DifficultyLevel difficultyLevel;
    public GameCategory gameCategory;
    
    [Header("Levels Solved")]
    public int levelsSolved;
    public TMP_Text levelsSolvedText;

    [Header("Game Statistics")] 
    public int wrongClicks;
    public TMP_Text wrongClicksText;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        if (Application.isPlaying)
        {
            DontDestroyOnLoad(gameObject); 
        }
        Difficulty.DifficultyLevel = difficultyLevel;
    }

    public void IncreaseLevelsSolved()
    {
        levelsSolved++;
        levelsSolvedText.text = levelsSolved.ToString();
    }
    
    public void IncreaseWrongClicks()
    {
        wrongClicks++;
        wrongClicksText.text = wrongClicks.ToString();
    }
}