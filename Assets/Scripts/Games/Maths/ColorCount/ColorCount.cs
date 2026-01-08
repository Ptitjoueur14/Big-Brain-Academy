using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
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
        
        [Header("UI Elements")] 
        public GameObject UIParent;
        [Header("Color Buttons")]
        public GameObject colorButtonsParent;
        public List<Button> colorButtons;

        [Header("Color Count Texts")] 
        public GameObject colorCountsParent;
        public TMP_Text blueCountText;
        public TMP_Text redCountText;
        public float colorCountShowDuration; // The amount of seconds color counts show before winning or losing
        
        private Random Random = new (DateTime.Now.Millisecond);

        private void Start()
        {
            UIParent.SetActive(true);
            colorButtonsParent.SetActive(false);
            colorCountsParent.SetActive(false);
            foreach (Button button in colorButtons)
            {
                button.interactable = true;
            }
            
            ballSpawnSpeed = 0.5f;
            switch (GameManager.Instance.difficultyLevel)
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
            if (areBallsSpawning && Input.GetKey(KeyCode.Space))
            {
                isSpedUp = true;
                currentBallSpawnSpeed = ballSpawnSpeed / 2;
            }
            else if (areBallsSpawning && Input.GetKeyUp(KeyCode.Space))
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
            colorCountsParent.SetActive(false);
            foreach (ColorBall ball in colorBalls)
            {
                Destroy(ball.gameObject);
            }
            colorBalls.Clear();
            
            foreach (Button button in colorButtons)
            {
                button.interactable = true;
            }
            
            currentBallCount = 0;
            blueBallCount = 0;
            redBallCount = 0;
            totalBallsCount = Random.Next(minBallsCount, maxBallsCount + 1);
            StartCoroutine(SpawnAllBalls());
            areBallsSpawning = true;
            if (!isSpedUp)
            {
                currentBallSpawnSpeed = ballSpawnSpeed;
            }
        }

        // Runs this function when the Blue button is clicked
        public void OnBlueButtonClicked()
        {
            foreach (Button button in colorButtons)
            {
                button.interactable = false;
            }
            
            ShowBallCounts();
            if (blueBallCount > redBallCount)
            {
                StartCoroutine(Win());
            }
            else
            {
                StartCoroutine(Lose());
            }
        }

        // Runs this function when the Red button is clicked
        public void OnRedButtonClicked()
        {
            foreach (Button button in colorButtons)
            {
                button.interactable = false;
            }
            
            ShowBallCounts();
            if (redBallCount > blueBallCount)
            {
                StartCoroutine(Win());
            }
            else
            {
                StartCoroutine(Lose());
            }
        }

        // Runs this function when the Equal button is clicked
        public void OnEqualButtonClicked()
        {
            foreach (Button button in colorButtons)
            {
                button.interactable = false;
            }
            
            ShowBallCounts();
            if (redBallCount == blueBallCount)
            {
                StartCoroutine(Win());
            }
            else
            {
                StartCoroutine(Lose());
            }
        }

        public IEnumerator Win()
        {
            GameManager.Win();
            yield return new WaitForSeconds(colorCountShowDuration);
            RestartLevel();
        }

        public IEnumerator Lose()
        {
            GameManager.Lose();
            yield return new WaitForSeconds(colorCountShowDuration);
            RestartLevel();
        }

        public void ShowBallCounts()
        {
            colorCountsParent.SetActive(true);
            blueCountText.text = blueBallCount.ToString();
            redCountText.text = redBallCount.ToString();
        }
    }
}