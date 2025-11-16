using System;
using UnityEngine;

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard,
    Expert
}
public class Difficulty : MonoBehaviour
{
    [Header("Set Difficulty in Inspector")]
    public DifficultyLevel currentDifficulty;
    
    public static DifficultyLevel DifficultyLevel { get; set; }

    private void Awake()
    {
        DifficultyLevel = currentDifficulty; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
