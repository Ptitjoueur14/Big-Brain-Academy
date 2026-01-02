using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

namespace Games.Maths.MalletMath
{
    public class MalletMath : MonoBehaviour
    {
        [Header("Number Blocks")] 
        public GameObject blocksParent;
        public int totalSum; // The number to obtain by adding all the numbers of the blocks
        public int currentSum;
        public TMP_Text totalSumText; // The sum indicator
        public List<NumberBlock> numberBlocks; // The list of number blocks in the game
        public List<NumberBlock> currentNumberBlocks; // The list of remaining number blocks in the game
        public NumberBlock blockPrefab; // The block number to instantiate
        public NumberBlock blockPrefabBig; // The block number in Easy and Medium
        public NumberBlock blockPrefabSmall; // The block number in Hard (smaller)
        public NumberBlock blockPrefabTiny; // The block number in Expert (even smaller)
        
        [Header("Number Blocks Count")] 
        public int totalNumberBlocksCount;
        public int numberBlocksCount;

        [Header("Number Blocks Count Interval")]
        public int minNumberBlocksCount;
        public int maxNumberBlocksCount;

        [Header("Number Blocks Number Interval")]
        public int minNumberBlockNumber;
        public int maxNumberBlockNumber;
        
        [Header("Number Block Remove Count Interval")]
        public int minNumberBlockRemoveCount;
        public int maxNumberBlockRemoveCount;
        public int numberBlockRemoveCount;

        [Header("Expert Settings")] 
        public float negativeProportion; // The chance of a number block being a negative number in Expert
        
        [Header("Number Blocks Height")]
        public float numberBlocksHeight; // The size of each number block (based on total number blocks count)
        
        [Header("Number Block Colors")]
        public List<Sprite> numberBlockColors;
        public List<Color> colors;
        
        private Random Random = new(DateTime.Now.Millisecond);

        private void Start()
        {
            totalSumText.gameObject.SetActive(true);
            
            switch (Difficulty.DifficultyLevel)
            {
                case DifficultyLevel.Easy: // 3 blocks with numbers [1;4]
                    minNumberBlocksCount = 3;
                    maxNumberBlocksCount = 3;
                    minNumberBlockNumber = 1;
                    maxNumberBlockNumber = 5;
                    minNumberBlockRemoveCount = 1;
                    maxNumberBlockRemoveCount = 2;
                    numberBlocksHeight = 2f;
                    blockPrefab = blockPrefabBig;
                    break;
                case DifficultyLevel.Medium: // 4-5 blocks with numbers [1;7]
                    minNumberBlocksCount = 4;
                    maxNumberBlocksCount = 5;
                    minNumberBlockNumber = 1;
                    maxNumberBlockNumber = 7;
                    minNumberBlockRemoveCount = 1;
                    maxNumberBlockRemoveCount = 3;
                    numberBlocksHeight = 2f;
                    blockPrefab = blockPrefabBig;
                    break;
                case DifficultyLevel.Hard: // 5-7 blocks with numbers [1;9]
                    minNumberBlocksCount = 5;
                    maxNumberBlocksCount = 7;
                    minNumberBlockNumber = 1;
                    maxNumberBlockNumber = 9;
                    minNumberBlockRemoveCount = 2;
                    maxNumberBlockRemoveCount = 3;
                    numberBlocksHeight = 1.4f;
                    blockPrefab = blockPrefabSmall;
                    break;
                case DifficultyLevel.Expert: // 7-8 blocks with numbers [-10;10]
                    minNumberBlocksCount = 7;
                    maxNumberBlocksCount = 8;
                    minNumberBlockNumber = -10;
                    maxNumberBlockNumber = 10;
                    minNumberBlockRemoveCount = 3;
                    maxNumberBlockRemoveCount = 5;
                    numberBlocksHeight = 1f;
                    blockPrefab = blockPrefabTiny;
                    break;
            }
            SpawnAllNumberBlocks();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
            {
                ClickBlock();
            }
        }

        // Gets a random list of numbers of count blocksCount and of sum totalSum*
        // Example : 
        // GetNumberList(17, 4) = {4, 7, 3, 3}
        public List<int> GetNumberList(int blocksCount)
        {
            List<int> result = new List<int>();
            int sum = 0;
            
            for (int i = 0; i < blocksCount; i++)
            {
                int number;
                if (Difficulty.DifficultyLevel == DifficultyLevel.Expert)
                {
                    if (Random.NextDouble() < negativeProportion) // Negative number
                    {
                        number = Random.Next(minNumberBlockNumber, 0);
                    }
                    else // Positive number
                    {
                        number = Random.Next(1, maxNumberBlockNumber + 1);
                    }
                }
                else
                {
                    number = Random.Next(minNumberBlockNumber, maxNumberBlockNumber + 1);
                }
                result.Add(number);
                sum += number;
                //Debug.Log($"Block : {i}, Number : {number}. Total : {sum}");
            }
            return result;
        }

        public void ColorBlock(NumberBlock numberBlock)
        {
            int colorIndex = Mathf.Abs(numberBlock.number) % numberBlockColors.Count;
            numberBlock.sr.sprite = numberBlockColors[colorIndex];
            numberBlock.blockColor = colors[colorIndex];
        }

        public List<NumberBlock> GetNumberBlockList(List<int> numberList)
        {
            List<NumberBlock> result = new List<NumberBlock>();
            for (int i = 0; i < numberList.Count; i++)
            {
                // TODO : Get position of block
                Vector3 blockPosition = new Vector3(0, -3.9f + i * numberBlocksHeight, 0);
                Debug.Log($"Spawned block {i} at Y = {-3.9f + i * numberBlocksHeight}");
                NumberBlock numberBlock = Instantiate(blockPrefab, blockPosition, Quaternion.identity, blocksParent.transform);
                
                numberBlock.number = numberList[i];
                numberBlock.numberBlockIndex = i + 1;
                numberBlock.name = numberBlock.ToString();
                
                ColorBlock(numberBlock); // Add color to the block based on number
                
                result.Add(numberBlock);
            }
            //PrintAllNumberBlocks(result);
            return result;
        }

        public void RemoveRandomBlock(List<NumberBlock> numberBlocksList)
        {
            if (numberBlocksList.Count != 0)
            {
                int index = Random.Next(0, numberBlocksList.Count);
                NumberBlock removedBlock = numberBlocksList[index];
                //Debug.Log($"Removed block : {removedBlock}");
                numberBlocksList.RemoveAt(index);
            }
        }
        
        // Removes from the list numberBlocks numberBlockRemoveCount blocks and calculates the sum of the smaller list after the deletions
        public int RemoveRandomBlocks()
        {
            numberBlockRemoveCount = Random.Next(minNumberBlockRemoveCount, maxNumberBlockRemoveCount + 1);
            List<NumberBlock> removedNumberBlocks = new List<NumberBlock>(numberBlocks);
            for (int i = 0; i < numberBlockRemoveCount; i++)
            {
                RemoveRandomBlock(removedNumberBlocks);
            }
            int sum = CalculateSum(removedNumberBlocks);
            //Debug.Log($"Total sum to make : {sum}");
            return sum;
        }

        // Calculates the sum of the numbers in the list
        public int CalculateSum(List<NumberBlock> numberBlocksList)
        {
            int sum = 0;
            numberBlocksList.ForEach(block => sum += block.number);
            return sum;
        }

        public void SpawnAllNumberBlocks()
        {
            totalNumberBlocksCount = Random.Next(minNumberBlocksCount, maxNumberBlocksCount + 1);
            List<int> numberList = GetNumberList(totalNumberBlocksCount);
            numberBlocks = GetNumberBlockList(numberList);
            numberBlocksCount = numberBlocks.Count;
            
            totalSum = RemoveRandomBlocks();
            currentSum = CalculateSum(numberBlocks);
            totalSumText.text = totalSum.ToString();
        }
        
         public bool ClickBlock()
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (!hit)
            {
                return false;
            }
            NumberBlock hitBlock = hit.collider.GetComponent<NumberBlock>();
            if (!hitBlock)
            {
                return false;
            }
            bool isPopCorrect = PopBlock(hitBlock);
            /*
            if (isPopCorrect && AreAllBalloonsPopped()) // All balloons popped : Restart game
            {
                lastPoppedBalloonNumber = -100;
                SpawnAllBalloons(); //FIX
                
                Timer timer = GameManager.Instance.GetComponent<Timer>();
                GameManager.Instance.IncreaseLevelsSolved(); //Increment levels solved counter
                Debug.Log($"Time taken to solve level : {timer.DisplayTimer(timer.currentLevelTimer)}. Total time : {timer.DisplayTimer(timer.gameTimer)}. Levels solved : {GameManager.Instance.levelsSolved}");
                timer.UpdateLevelTimers();
            }
            */
            /*
            else if (!isPopCorrect) // Wrong Pop : Increment Wrong Clicks (error clicks)
            {
                GameManager.Instance.IncreaseWrongClicks();
            }
            */
            return true;
        }
        
        // Try to pop the balloon and return true if it is correct (balloons popped in ascending order)
        public bool PopBlock(NumberBlock numberBlock)
        {
            if (numberBlocksCount == 0)
            {
                return false;
            }

            // Balloon number is greater than the previous one : Destroy balloon
            //Debug.Log($"Popped {numberBlock}");
            numberBlocks.Remove(numberBlock);
            currentSum -= numberBlock.number;
            
            //SpawnPopEffect(balloon);
            
            Destroy(numberBlock.gameObject);
            numberBlocksCount--;
            
            CheckBlockCorrect();
            
            return true;
        }

        public void RemoveAllNumberBlocks()
        {
            foreach (NumberBlock numberBlock in numberBlocks)
            { 
                Destroy(numberBlock.gameObject);
            }
            numberBlocks.Clear();
            currentSum = 0;
            totalSum = 0;
            numberBlocksCount = 0;
        }

        // Checks to see if the removed block allows to match the requested sum, or lose
        public bool CheckBlockCorrect()
        {
            if (currentSum == totalSum) // Goal sum reached -> Go to next level
            {
                GameManager.Win();
                
                RemoveAllNumberBlocks();
                SpawnAllNumberBlocks(); // Start a new level
                return true;
            }
            if (!CanReach(totalSum, 0, 0))
            {
                //Debug.Log($"Can't reach {totalSum} with the current blocks");
                GameManager.Lose();
                RemoveAllNumberBlocks();
                SpawnAllNumberBlocks(); // Start a new level
                return false;
            }
            return true; // Still possible -> continue;
        }

        public bool CanReach(int targetSum, int index, int sum)
        {
            if (sum == targetSum)
            {
                return true;
            }
            if (index >= numberBlocks.Count)
            {
                return false;
            }
            int number = numberBlocks[index].number;
            return CanReach(targetSum, index + 1, sum + number) ||
                   CanReach(targetSum, index + 1, sum); //include or exclude the current block
        }
        
        public void PrintAllNumberBlocks(List<NumberBlock> numberBlocksList)
        {
            string result = "";
            for (int i = 0; i < numberBlocksList.Count; i++)
            {
                result += numberBlocksList[i];
                if (i != numberBlocksList.Count - 1)
                {
                    result += "\n";
                }
            }
            Debug.Log(result);
        }
    }
}