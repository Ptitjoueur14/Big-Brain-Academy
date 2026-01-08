using System;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    [Header("Timer In Milliseconds")]
    public int gameTimer; // The timer of the current game
    public int currentLevelTimer; // The timer of the current level
    public int previousLevelTimer; // The timer of the previously beaten level

    [Header("Timer Texts")]
    public TMP_Text gameTimerText;
    public TMP_Text currentLevelTimerText;
    public TMP_Text previousLevelTimerText;

    public void Awake()
    {
        gameTimer = 0;
        currentLevelTimer = 0;
        previousLevelTimer = 0;
    }

    private void Start()
    {
        if (gameTimerText != null)
        {
            gameTimerText.text = "0.000 s";
        }
        if (currentLevelTimerText != null)
        {
            currentLevelTimerText.text = "0.000 s";
        }
        if (previousLevelTimerText != null)
        {
            previousLevelTimerText.text = "0.000 s";
        }
    }

    private void Update()
    {
        int deltaMs = (int)(Time.deltaTime * 1000f); 
        gameTimer += deltaMs;
        currentLevelTimer += deltaMs;
        
        UpdateTimerTexts();
    }
    
    public int GetSeconds(int timer)
    {
        return timer / 1000;
    }

    public int GetMilliseconds(int timer)
    {
        return timer % 1000;
    } 

    public string DisplayTimer(int timer)
    {
        int seconds = GetSeconds(timer);
        int milliseconds = GetMilliseconds(timer);
        return $"{seconds}.{milliseconds:D3} s";
    }

    public void UpdateTimerTexts()
    {
        gameTimerText.text = DisplayTimer(gameTimer);
        currentLevelTimerText.text = DisplayTimer(currentLevelTimer);
    }

    public void UpdateLevelTimers()
    {
        previousLevelTimer = currentLevelTimer;
        previousLevelTimerText.text = DisplayTimer(previousLevelTimer);
        currentLevelTimer = 0;
        UpdateTimerTexts();
    }
}