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
    public static DifficultyLevel DifficultyLevel;
    private void Awake()
    {
        //DifficultyLevel = currentDifficulty; 
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
