using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Games.Maths.TickTockTurn
{
    public enum ClockDirection
    {
        Forward,
        Backward,
    }

    public class Time
    {
        public int Hours; // The hour component of the time
        public int Minutes; // The minutes component of the time
    }
    
    public class TickTockTurn : MonoBehaviour
    {
        [Header("Time")] 
        public int timeToTurn; // The time in minutes to add/remove on the clock
        public int turnedTime; // The time in minutes the player turned the clock in this level
        
        [Header("Clock Time")]
        public float currentHourClockSector; // The sector of the clock the Hour hand is on (can be between two sectors)
        public int currentMinuteClockSector; // The sector of the clock the Minute segment is on
        public ClockDirection clockDirection; // The direction to go in

        [Header("Time Interval")] // The possible time to turn in minutes for each difficulty
        public List<int> easyTimesList = new (){ 5, 10, 15, 20, 30, 40, 45, 50, 60, 65, 70, 75 };
        public List<int> mediumTimesList = new() { 25, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 180 };
        public List<int> hardTimesList = new() { 35, 55, 65, 85, 95, 105, 115, 125, 135, 145, 155, 165, 175, 185, 195, 205, 210, 240 };
        public List<int> expertTimesList = new() { 55, 85, 105, 125, 145, 165, 175, 180, 185, 190, 195, 200, 205, 210, 215, 220, 235, 240, 245, 250, 255, 260, 265, 270, 285, 295, 300, 305, 325, 360, 390, 420 };
        
        [Header("UI Elements")]
        public TMP_Text timeToTurnText;
        public GameObject validateButton;
        
        private Random Random = new (DateTime.Now.Millisecond); 
        
        private void Start()
        {
            timeToTurnText.gameObject.SetActive(true);
            if (Random.Next(0, 2) == 0)
            {
                clockDirection = ClockDirection.Forward;
            }
            else
            {
                clockDirection = ClockDirection.Backward;
            }
            GetRandomTime();

            timeToTurnText.text = DisplayTimeToTurn();
        }

        public void GetRandomTime()
        {
            switch (GameManager.Instance.difficultyLevel) // Pick a random time from the list of that difficulty
            {
                case DifficultyLevel.Easy: // Can go up to 75 minutes (1h 15m)
                    timeToTurn = easyTimesList[Random.Next(0, easyTimesList.Count)];
                    break;
                case DifficultyLevel.Medium: // Can go up to 180 minutes (3h)
                    timeToTurn = mediumTimesList[Random.Next(0, mediumTimesList.Count)];
                    break;
                case DifficultyLevel.Hard: // Can go up to 240 minutes (4h)
                    timeToTurn = hardTimesList[Random.Next(0, hardTimesList.Count)];
                    break;
                case DifficultyLevel.Expert: // Can go up to 420 minutes (7h)
                    timeToTurn = expertTimesList[Random.Next(0, expertTimesList.Count)];
                    break;
            }

            if (clockDirection == ClockDirection.Backward)
            {
                timeToTurn = -timeToTurn;
            }
        }

        // Convert the time in minutes to a time in hours and minutes
        public Time GetTime(int timeInMinutes)
        {
            Time time = new Time();
            if (timeInMinutes < 0)
            {
                timeInMinutes = -timeInMinutes;
            }
            time.Hours = timeInMinutes / 60;
            time.Minutes = timeInMinutes % 60;
            return time;
        }

        public string DisplayTimeToTurn()
        {
            string result = "";
            Time time = GetTime(timeToTurn);
            if (clockDirection == ClockDirection.Forward)
            {
                result += "Avancer ";
            }
            else
            {
                result += "Reculer ";
            }

            if (time.Hours == 0) // Don't display Hours
            {
                return result += $"de {time.Minutes}m !";
            }
            return result += $"de {time.Hours}h {time.Minutes}m !";
        }

        public void OnValidateButtonClicked()
        {
            if (turnedTime == timeToTurn)
            {
                GameManager.Win();
            }
            else
            {
                GameManager.Lose();
            }
            ResetLevel();
        }

        public void ResetLevel()
        {
            turnedTime = 0;
            if (Random.Next(0, 2) == 0)
            {
                clockDirection = ClockDirection.Forward;
            }
            else
            {
                clockDirection = ClockDirection.Backward;
            }

            GetRandomTime();

            timeToTurnText.text = DisplayTimeToTurn();
        }
    }
}