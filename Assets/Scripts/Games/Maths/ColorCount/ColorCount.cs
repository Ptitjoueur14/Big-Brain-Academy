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

        [Header("Balls Count")] 
        public int totalBallsCount;
        public int currentBallCount;
        public int redBallCount;
        public int blueBallCount;

        [Header("Balls Count Interval")] 
        public int minBallsCount;
        public int maxBallsCount;
        
        [Header("Ball Spawn Speed")]
        public float ballSpawnSpeed; // The time in seconds between each ball spawn
        public float currentBallSpawnSpeed;
        public bool areBallsSpawning;
        public bool isSpedUp; // If the player speeds up the ball spawning phase by holding left click
        
        [Header("Color Buttons")]
        public GameObject colorButtonsParent;
        
        private Random Random = new (DateTime.Now.Millisecond);

        private void Start()
        {
            colorButtonsParent.SetActive(false);
            ballSpawnSpeed = 0.5f;
            switch (Difficulty.DifficultyLevel)
            {
                case DifficultyLevel.Easy: // 3-5 balls
                    minBallsCount = 3;
                    maxBallsCount = 5;
                    break;
                case DifficultyLevel.Medium: // 5-7 balls
                    minBallsCount = 5;
                    maxBallsCount = 7;
                    break;
                case DifficultyLevel.Hard: // 8-12 balls
                    minBallsCount = 8;
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
            
            yield return new WaitForSeconds(currentBallSpawnSpeed);
            colorButtonsParent.SetActive(true);
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

            if (Random.Next(0, 2) == 0)
            {
                ball.ballSpawnSide = BallSpawnSide.LeftSide;
            }
            else
            {
                ball.ballSpawnSide = BallSpawnSide.RightSide;
            }

            ball.name = ball.ToString();
            currentBallCount++;
            colorBalls.Add(ball);
        }

        public void RestartLevel()
        {
            colorButtonsParent.SetActive(false);
            foreach (ColorBall ball in colorBalls)
            {
                Destroy(ball.gameObject);
            }
            colorBalls.Clear();
            
            currentBallCount = 0;
            blueBallCount = 0;
            redBallCount = 0;
            totalBallsCount = Random.Next(minBallsCount, maxBallsCount + 1);
            StartCoroutine(SpawnAllBalls());
            areBallsSpawning = true;
        }

        public void OnBlueButtonClicked()
        {
            ShowBallCounts();
            if (blueBallCount > redBallCount)
            {
                Win();
            }
            else
            {
                Lose();
            }
        }

        public void OnRedButtonClicked()
        {
            ShowBallCounts();
            if (redBallCount > blueBallCount)
            {
                Win();
            }
            else
            {
                Lose();
            }
        }

        public void OnEqualButtonClicked()
        {
            ShowBallCounts();
            if (redBallCount == blueBallCount)
            {
                Win();
            }
            else
            {
                Lose();
            }
        }

        public void Win()
        {
            RestartLevel();
            GameManager.Win();
        }

        public void Lose()
        {
            RestartLevel();
            GameManager.Lose();
        }

        public void ShowBallCounts()
        {
            
        }
    }
}