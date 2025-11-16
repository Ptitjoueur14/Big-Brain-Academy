using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Games.Maths
{
    public class BalloonBurst : MonoBehaviour
    {
        public GameObject balloonsParent;
        public List<Balloon> balloons;
        //public int? LastPoppedBalloonNumber = -100; // The number of the last correct balloon popped by the player
        public int lastPoppedBalloonNumber = -100;
        public Balloon balloonPrefab; // Balloon prefab to instantiate
    
        [Header("Total Balloons Count")]
        public int totalBalloons;
        public int balloonCount; // The current balloon count of the game
        
        public System.Random Random;
        
        [Header("Balloon Count Interval")]
        public int minBalloonCount;
        public int maxBalloonCount; // Max number of balloons depending on difficulty
        
        [Header("Balloon Number Interval")]
        public int minNumber;
        public int maxNumber; // Max number value depending on difficulty

        [Header("Balloon Rotation Interval")]
        public int minRotation;
        public int maxRotation; // Max rotation angle on spawn in degrees
        public float minRotationSpeed;
        public float maxRotationSpeed; // Max rotation speed of the balloon
        
        [Header("Balloon Movement Speed Interval")]
        public float minMoveSpeed;
        public float maxMoveSpeed; // Max move speed of the balloon
        
        [Header("Balloon Spawn Settings")]
        // Spawn bounds of ballons
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
        public float minDistanceBetweenBalloons;
        
        [Header("Balloon Colors")]
        public List<Sprite> balloonColors;
        
        void Awake()
        {
            Random = new System.Random(DateTime.Now.Millisecond); // Random to assign random values to balloon fields
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            switch (Difficulty.DifficultyLevel) 
            {
                case DifficultyLevel.Easy: // 3-4 balloons with values [0;7]
                    minBalloonCount = 3;
                    maxBalloonCount = 4;
                    minNumber = 0;
                    maxNumber = 7;
                    minRotation = 0;
                    maxRotation = 5;
                    minRotationSpeed = 0f;
                    maxRotationSpeed = 1f;
                    minMoveSpeed = 0f;
                    maxMoveSpeed = 0.1f;
                    break;
                case DifficultyLevel.Medium: // 4-5 balloons with numbers [0;30]
                    minBalloonCount = 4;
                    maxBalloonCount = 5;
                    minNumber = 0;
                    maxNumber = 30;
                    minRotation = 5;
                    maxRotation = 20;
                    minRotationSpeed = 0.5f;
                    maxRotationSpeed = 2f;
                    minMoveSpeed = 0.05f;
                    maxMoveSpeed = 0.2f;
                    break;
                case DifficultyLevel.Hard: // 5-7 balloons with numbers [0;50]
                    minBalloonCount = 5;
                    maxBalloonCount = 7;
                    minNumber = 0;
                    maxNumber = 50;
                    minRotation = 7;
                    maxRotation = 50;
                    minRotationSpeed = 1f;
                    maxRotationSpeed = 4f; 
                    minMoveSpeed = 0.1f;
                    maxMoveSpeed = 0.4f;
                    break;
                case DifficultyLevel.Expert: // 7-8 balloons with numbers [-99;99]
                    minBalloonCount = 7;
                    maxBalloonCount = 8;
                    minNumber = -99;
                    maxNumber = 99;
                    minRotation = 20;
                    maxRotation = 60;
                    minRotationSpeed = 2f;
                    maxRotationSpeed = 5f;
                    minMoveSpeed = 0.3f;
                    maxMoveSpeed = 0.8f;
                    break;
            }
            
            SpawnAllBalloons();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
            {
                ClickBalloon();
            }
        }
        
        // Pick a unique random number for the new balloon not already in the balloon list
        public int GetRandomNumber(int numberMin, int numberMax)
        {
            List<int> availableNumbers = new List<int>();
            for (int number = numberMin; number <= numberMax; number++)
            {
                if (balloons.All(balloon => balloon.number != number)) // number is not in the balloon list -> available
                {
                    availableNumbers.Add(number);
                }
            }

            if (availableNumbers.Count == 0) // No available numbers
            {
                // TODO : Return of number with no available number
                return -1;
            }
            
            int randomIndex = Random.Next(availableNumbers.Count);
            return availableNumbers[randomIndex];
        }
        
        // Pick a random float number for the balloon speed in [speedMin;speedMax]
        public float GetRandomSpeed(float speedMin, float speedMax)
        {
            return (float) Random.NextDouble() * (speedMax - speedMin) + speedMin;
        }

        public Vector2 AvoidCorners(float x, float y)
        {
            if (x - minX < minDistanceBetweenBalloons)
            {
                x += minDistanceBetweenBalloons;
            }
            else if (maxX - x < minDistanceBetweenBalloons)
            {
                x -= minDistanceBetweenBalloons;
            }
            if (y - minY < minDistanceBetweenBalloons)
            {
                y += minDistanceBetweenBalloons;
            }
            else if (maxY - y < minDistanceBetweenBalloons)
            {
                y -= minDistanceBetweenBalloons;
            }

            return new Vector2(x, y);
        }
        
        // Pick a random available spawn position for the new balloon
        public Vector2 GetRandomSpawnPosition()
        {
            int maxAttempts = 50;
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                float x = (float) Random.NextDouble() * (maxX - minX) + minX;
                float y = (float) Random.NextDouble() * (maxY - minY) + minY;
                Vector2 spawnPosition = AvoidCorners(x, y);

                if (balloons.All(balloon => Vector2.Distance(balloon.transform.position, spawnPosition) > minDistanceBetweenBalloons))
                {
                    return spawnPosition;
                }
            }
            
            // No valid position found, return a random position anyway
            float randomX = (float) Random.NextDouble() * (maxX - minX) + minX;
            float randomY = (float) Random.NextDouble() * (maxY - minY) + minY;
            return new Vector2(randomX, randomY);
        }
        
        // Spawns a new balloon in a random position
        public Balloon SpawnBalloon()
        {
            if (balloonCount < totalBalloons)
            {
                // Spawn balloon in random position after checking if it doesn't overlap with a corner or other balloon
                Vector2 randomSpawnPosition = GetRandomSpawnPosition();
                float randomRotationAngle = (float) Random.NextDouble() * (maxRotation - minRotation) + minRotation;
                bool isRightRotation = Random.Next(0, 2) == 0;
                if (isRightRotation) // Rotate to the right 50% of the time
                {
                    randomRotationAngle *= -1; // Can rotate left or right
                }
                Balloon balloon = Instantiate(
                    balloonPrefab, 
                    randomSpawnPosition, 
                    Quaternion.Euler(0, 0, randomRotationAngle), 
                    balloonsParent.transform
                    );
                balloon.number = GetRandomNumber(minNumber, maxNumber);
                balloon.balloonIndex = balloonCount + 1;
                balloon.name = balloon.ToString();
                
                // Balloon has a random color in the list of colors
                int randomColorIndex = Random.Next(balloonColors.Count);
                balloon.graphics.sprite = balloonColors[randomColorIndex];
                
                balloon.rotationSpeed = GetRandomSpeed(minRotationSpeed, maxRotationSpeed);
                balloon.moveSpeed = GetRandomSpeed(minMoveSpeed, maxMoveSpeed);

                if (isRightRotation)
                {
                    balloon.rotationSpeed *= -1; // Make right-tilted ballons rotate to the right
                }
                
                // Add balloon to list
                balloons.Add(balloon);
                balloonCount++;
                return balloon;
            }
            return null;
        }

        public void SpawnAllBalloons()
        {
            totalBalloons = Random.Next(minBalloonCount, maxBalloonCount + 1);
            for (int i = 0; i < totalBalloons; i++)
            {
                SpawnBalloon();
            }
            
            PrintAllBalloons(); // Print all balloons with their numbers
        }

        public bool AreAllBalloonsPopped()
        {
            // TODO : Go to next level
            
            // Spawn new ballons (next level)
            return balloons.Count == 0;
        }

        public bool ClickBalloon()
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (!hit)
            {
                return false;
            }
            Balloon hitBalloon = hit.collider.GetComponent<Balloon>();
            if (!hitBalloon)
            {
                return false;
            }

            if (PopBalloon(hitBalloon) && AreAllBalloonsPopped()) // All balloons popped : Restart game
            {
                lastPoppedBalloonNumber = -100;
                SpawnAllBalloons(); //FIX
                
                Timer timer = GameManager.Instance.GetComponent<Timer>();
                GameManager.Instance.IncreaseLevelsSolved(); //Increment levels solved counter
                Debug.Log($"Time taken to solve level : {timer.DisplayTimer(timer.currentLevelTimer)}. Total time : {timer.DisplayTimer(timer.gameTimer)}. Levels solved : {GameManager.Instance.levelsSolved}");
                timer.UpdateLevelTimers();
            }
            return true;
        }
        
        // Try to pop the balloon and return true if it is correct (balloons popped in ascending order)
        public bool PopBalloon(Balloon balloon)
        {
            if (balloonCount == 0)
            {
                return false;
            }
            
            Balloon minBalloon = balloons.OrderBy(b => b.number).First(); 
            if (balloon != minBalloon) 
            { 
                //Debug.Log($"Failed to pop {balloon} because {minBalloon} is lower than it"); 
                return false;
            }

            // Balloon number is greater than the previous one : Destroy balloon
            //Debug.Log($"Popped {balloon}");
            lastPoppedBalloonNumber= balloon.number;
            balloons.Remove(balloon);
            Destroy(balloon.gameObject);
            balloonCount--;
            
            return true;
        }
        
        public void PrintAllBalloons()
        {
            string result = "";
            foreach (Balloon balloon in balloons)
            {
                result += balloon;
                if (balloon.balloonIndex != balloonCount)
                {
                    result += "\n";
                }
            }
            //Debug.Log(result);
        }
    }
}
