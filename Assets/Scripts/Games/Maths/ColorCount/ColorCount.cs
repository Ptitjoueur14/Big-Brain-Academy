using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

namespace Games.Maths.ColorCount
{
    public class ColorCount : MonoBehaviour
    {
        [Header("Color Balls")] 
        public GameObject ballsParent;
        public List<ColorBall> colorBalls;
        public ColorBall ballPrefab;
        public int redBallCount;
        public int blueBallCount;

        [Header("Balls Count")] 
        public int totalBallsCount;
        public int currentBallCount;

        [Header("Balls Count Interval")] 
        public int minBallsCount;
        public int maxBallsCount;
        
        [Header("Ball Spawn Speed")]
        public float ballSpawnSpeed; // The time in seconds between each ball spawn
        public float currentBallSpawnSpeed;
        public bool areBallsSpawning;
        public bool isSpedUp; // If the player speeds up the ball spawning phase by holding left click
        
        private Random Random = new (DateTime.Now.Millisecond);

        private void Start()
        {
            switch (Difficulty.DifficultyLevel)
            {
                case DifficultyLevel.Easy: // 4-5 balls
                    minBallsCount = 4;
                    maxBallsCount = 5;
                    break;
                case DifficultyLevel.Medium: // 6-8 balls
                    minBallsCount = 6;
                    maxBallsCount = 8;
                    break;
                case DifficultyLevel.Hard: // 9-12 balls
                    minBallsCount = 9;
                    maxBallsCount = 12;
                    break;
                case DifficultyLevel.Expert: // 10-14 balls
                    minBallsCount = 10;
                    maxBallsCount = 14;
                    break;
            }
            totalBallsCount = Random.Next(minBallsCount, maxBallsCount + 1);
            StartCoroutine(SpawnAllBalls());
            areBallsSpawning = true;
            currentBallSpawnSpeed = ballSpawnSpeed;
        }

        private void Update()
        {
            if (areBallsSpawning && Input.GetMouseButtonDown((int) MouseButton.LeftMouse))
            {
                isSpedUp = true;
                currentBallSpawnSpeed = ballSpawnSpeed / 2;
            }
            else if (areBallsSpawning && !Input.GetMouseButtonDown((int) MouseButton.LeftMouse))
            {
                isSpedUp = false;
                currentBallSpawnSpeed = ballSpawnSpeed;
            }
        }

        public IEnumerator SpawnAllBalls()
        {
            for (int i = 0; i < totalBallsCount; i++)
            {
                SpawnBall();
                yield return new WaitForSeconds(currentBallSpawnSpeed);
            }
            areBallsSpawning = false;
            isSpedUp = false;
            ShowColorChoiceButtons();
        }

        public void SpawnBall()
        {
            ColorBall ball = Instantiate(ballPrefab, transform.position, Quaternion.identity, ballsParent.transform);
            ball.ballIndex = currentBallCount + 1;
            if (Random.Next(0, 2) == 0)
            {
                ball.ballColor = Color.red;
                redBallCount++;
            }
            else
            {
                ball.ballColor = Color.blue;
                blueBallCount++;
            }
            currentBallCount++;
            colorBalls.Add(ball);
        }

        public void ShowColorChoiceButtons()
        {
            
        }

        public bool IsCorrectBlue()
        {
            return blueBallCount > redBallCount;
        }

        public bool IsCorrectRed()
        {
            return redBallCount > blueBallCount;
        }

        public bool IsCorrectEqual()
        {
            return blueBallCount == redBallCount;
        }
    }
}