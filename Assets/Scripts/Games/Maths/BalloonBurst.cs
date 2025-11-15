using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Games.Maths
{
    public class BalloonBurst : MonoBehaviour
    {
        public GameObject balloonsParent;
        public List<Balloon> balloons;
        public Balloon lastPoppedBalloon; // Last correct balloon popped by the player
        public Balloon balloonPrefab; // Balloon prefab to instantiate
    
        public int totalBalloons;
        public int balloonCount; // The current balloon count of the game
        
        public System.Random Random;
        
        public int minBalloonCount;
        public int maxBalloonCount;
        
        public int minNumber;
        public int maxNumber;
        
        public float minRotationSpeed;
        public float maxRotationSpeed;
        
        public float minMoveSpeed;
        public float maxMoveSpeed;
        
        void Awake()
        {
            Random = new System.Random((int)Difficulty.DifficultyLevel); // Random to assign random values to balloon fields
            switch (Difficulty.DifficultyLevel)
            {
                case DifficultyLevel.Easy: // 3-4 balloons with values [0;7]
                    minBalloonCount = 3;
                    maxBalloonCount = 4;
                    minNumber = 0;
                    maxNumber = 7;
                    minRotationSpeed = 0f;
                    maxRotationSpeed = 0.05f;
                    minMoveSpeed = 0f;
                    maxMoveSpeed = 0.1f;
                    break;
                case DifficultyLevel.Medium: // 4-5 balloons with numbers [0;30]
                    minBalloonCount = 4;
                    maxBalloonCount = 5;
                    minNumber = 0;
                    maxNumber = 30;
                    minRotationSpeed = 0.05f;
                    maxRotationSpeed = 0.2f;
                    minMoveSpeed = 0.05f;
                    maxMoveSpeed = 0.2f;
                    break;
                case DifficultyLevel.Hard: // 5-7 balloons with numbers [0;50]
                    minBalloonCount = 5;
                    maxBalloonCount = 7;
                    minNumber = 0;
                    maxNumber = 50;
                    minRotationSpeed = 0.1f;
                    maxRotationSpeed = 0.4f;
                    minMoveSpeed = 0.1f;
                    maxMoveSpeed = 0.4f;
                    break;
                case DifficultyLevel.Expert: // 7-8 balloons with numbers [-99;99]
                    minBalloonCount = 7;
                    maxBalloonCount = 8;
                    minNumber = -99;
                    maxNumber = 99;
                    minRotationSpeed = 0.3f;
                    maxRotationSpeed = 0.5f;
                    minMoveSpeed = 0.3f;
                    maxMoveSpeed = 0.5f;
                    break;
            }
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        //Pick a unique random number for the new balloon not already in the balloon list
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
                return -1;
            }
            
            int randomIndex = Random.Next(availableNumbers.Count);
            return availableNumbers[randomIndex];
        }
        
        // Picks a random float number for the balloon speed in [speedMin;speedMax]
        public float GetRandomSpeed(float speedMin, float speedMax)
        {
            return (float) Random.NextDouble() * (speedMax - speedMin) + speedMin;
        }

        public Vector2 GetRandomSpawnPosition()
        {
            // TODO
            return new Vector2((float)Random.NextDouble(), (float)Random.NextDouble());
        }
        
        public void SpawnBalloon()
        {
            if (balloonCount < totalBalloons)
            {
                //Spawn balloon in random position after checking if it doesn't overlap with a corner or other balloon
                Balloon balloon = Instantiate(balloonPrefab, transform.position, Quaternion.identity, balloonsParent.transform);
                balloon.number = GetRandomNumber(minNumber, maxNumber);
                balloon.rotationSpeed = GetRandomSpeed(minRotationSpeed, maxRotationSpeed);
                balloon.moveSpeed = GetRandomSpeed(minMoveSpeed, maxMoveSpeed);
            }
        }

        public bool AreAllBalloonsPopped()
        {
            //Go to next level
            return balloons.Count == 0;
        }

        public bool PopBallon(Balloon balloon)
        {
            if (lastPoppedBalloon < balloon) 
            {
                //Wrong X
                return false;
            }
            else
            {
                //Destroy balloon
                lastPoppedBalloon = balloon;
                balloons.Remove(balloon);
                Destroy(balloon);
                return true;
            }
        }
    }
}
