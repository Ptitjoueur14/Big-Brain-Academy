using Modes.Stretching;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Databases")]
    public BrainScoreDatabase brainScoreDatabase;
    public LeaderboardRanking leaderboardRanking;
    
    [Header("Game Settings")]
    public DifficultyLevel difficultyLevel;
    public GameMode gameMode;
    public GameCategory gameCategory;
    public GameLevel gameLevel;
    
    [Header("Levels Solved")]
    public int levelsSolved;
    public TMP_Text levelsSolvedText;

    [Header("Game Statistics")] 
    public int wrongClicks;
    public TMP_Text wrongClicksText;

    [Header("Level Finish Icon")] 
    public Canvas canvasParent;
    public GameObject solvedIconPrefab;
    public GameObject errorIconPrefab;
    
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
        
        //SaveSystem.Load(brainScoreDatabase);
    }

    public void IncreaseLevelsSolved()
    {
        if (!levelsSolvedText || !wrongClicksText)
        {
            levelsSolvedText = GameObject.Find("LevelsSolvedText").GetComponent<TMP_Text>();
            wrongClicksText = GameObject.Find("WrongClicksText").GetComponent<TMP_Text>();
        }
        
        levelsSolved++;
        levelsSolvedText.text = levelsSolved.ToString();
        Timer timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        Debug.Log($"Time taken to solve level : {timer.DisplayTimer(timer.currentLevelTimer)}. Total time : {timer.DisplayTimer(timer.gameTimer)}. Levels solved : {GameManager.Instance.levelsSolved}");
    }
    
    public void IncreaseWrongClicks()
    {
        if (!levelsSolvedText || !wrongClicksText)
        {
            levelsSolvedText = GameObject.Find("LevelsSolvedText").GetComponent<TMP_Text>();
            wrongClicksText = GameObject.Find("WrongClicksText").GetComponent<TMP_Text>();
        }
        
        wrongClicks++;
        wrongClicksText.text = wrongClicks.ToString();
    }

    public static void Win()
    {
        Instance.IncreaseLevelsSolved(); //Increment levels solved counter
        
        Timer timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        timer.UpdateLevelTimers();
        
        BrainMass brainMass = GameObject.FindGameObjectWithTag("BrainMass").GetComponent<BrainMass>();
        if (Instance.gameMode == GameMode.Stretching)
        {
            brainMass.stretching.AddBrainMass(brainMass.brainMassNumber);
            brainMass.stretching.DecreaseRemainingLevels();
        }
        brainMass.SpawnBrainCloud();

        Instance.canvasParent = FindFirstObjectByType<Canvas>();
        Instantiate(Instance.solvedIconPrefab, Instance.transform.position, Quaternion.identity, Instance.canvasParent.transform);
    }

    public static void Lose()
    {
        Instance.IncreaseWrongClicks();
        
        BrainMass brainMass = GameObject.FindGameObjectWithTag("BrainMass").GetComponent<BrainMass>();
        if (Instance.gameMode == GameMode.Stretching)
        {
            brainMass.stretching.errors++;
            brainMass.stretching.AddBrainMass(0);
            brainMass.stretching.DecreaseRemainingLevels();
        }
        brainMass.RestartBrainMassDecrease();

        Instance.canvasParent = FindFirstObjectByType<Canvas>();
        Instantiate(Instance.errorIconPrefab, Instance.transform.position, Quaternion.identity, Instance.canvasParent.transform);
    }

    public void SaveDatabases()
    {
        SaveSystem.SaveBrainScoreDatabase(brainScoreDatabase);
        SaveSystem.SaveLeaderboardRanking(leaderboardRanking);
    }
    
    
}