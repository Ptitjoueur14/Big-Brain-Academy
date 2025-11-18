using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Games.Maths.MalletMath
{
    public class MalletMath : MonoBehaviour
    {
        [Header("Number Blocks")] 
        public GameObject blocksParent;
        public int totalSum; // The number to obtain by adding all the numbers of the blocks
        public List<NumberBlock> numberBlocks; // The list of number blocks in the game
        public List<NumberBlock> currentNumberBlocks; // The list of remaining number blocks in the game
        public NumberBlock blockPrefab;

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
        
        [Header("Number Blocks Size")]
        public int numberBlocksSize; // The size of each number block (based on total number blocks count)

        private Random Random = new(DateTime.Now.Millisecond);

        private void Start()
        {
            switch (Difficulty.DifficultyLevel)
            {
                case DifficultyLevel.Easy: // 3-4 blocks with numbers [1;4]
                    minNumberBlocksCount = 3;
                    maxNumberBlocksCount = 4;
                    minNumberBlockNumber = 1;
                    maxNumberBlockNumber = 4;
                    minNumberBlockRemoveCount = 1;
                    maxNumberBlockRemoveCount = 2;
                    break;
                case DifficultyLevel.Medium: // 4-5 blocks with numbers [1;6]
                    minNumberBlocksCount = 4;
                    maxNumberBlocksCount = 5;
                    minNumberBlockNumber = 1;
                    maxNumberBlockNumber = 6;
                    minNumberBlockRemoveCount = 1;
                    maxNumberBlockRemoveCount = 3;
                    break;
                case DifficultyLevel.Hard: // 5-7 blocks with numbers [1;9]
                    minNumberBlocksCount = 5;
                    maxNumberBlocksCount = 7;
                    minNumberBlockNumber = 1;
                    maxNumberBlockNumber = 9;
                    minNumberBlockRemoveCount = 2;
                    maxNumberBlockRemoveCount = 3;
                    break;
                case DifficultyLevel.Expert: // 7-9 blocks with numbers [1;4]
                    minNumberBlocksCount = 7;
                    maxNumberBlocksCount = 9;
                    minNumberBlockNumber = -15;
                    maxNumberBlockNumber = 15;
                    minNumberBlockRemoveCount = 3;
                    maxNumberBlockRemoveCount = 4;
                    break;
            }
            totalNumberBlocksCount = Random.Next(minNumberBlocksCount, maxNumberBlocksCount + 1);
        }
        
        // Gets a random list of numbers of count blocksCount and of sum totalSum*
        // Example : 
        // GetNumberList(17, 4) = {4, 7, 3, 3}
        public List<int> GetNumberList2(int sum, int blocksCount)
        {
            // Sum: 6, Blocks: 3 - [1;4]
            // 0: 3
            // 1 : 3
            // 2 : 1 
            List<int> result = new List<int>();
            int minNumber = minNumberBlockNumber;
            int maxNumber = maxNumberBlockNumber;
            int remainingSum = sum;
            int remainingBlocks = blocksCount;
            
            for (int i = 0; i < blocksCount; i++)
            {
                remainingBlocks--;
                
                int minPossibleRemaining = remainingBlocks * minNumber;
                int maxPossibleRemaining = remainingBlocks * maxNumber;
                
                int low = Mathf.Max(minNumber, remainingSum - maxPossibleRemaining);
                int high = Mathf.Min(maxNumber, remainingSum - minPossibleRemaining);
                
                int number = Random.Next(low, high + 1);
                result.Add(number);
                remainingSum -= number;
                Debug.Log($"Block : {i}, Number : {number}. Min Number : {low}, Max Number : {high}. Total : {sum - remainingSum}, Remaining : {remainingSum}");
            }
            return result;
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
                int number = Random.Next(minNumberBlockNumber, maxNumberBlockNumber + 1);
                result.Add(number);
                sum += number;
                Debug.Log($"Block : {i}, Number : {number}. Total : {sum}");
            }
            return result;
        }

        public void GetNumberBlockList(List<int> numberList)
        {
            for (int i = 0; i < numberList.Count; i++)
            {
                // TODO : Get position of block
                NumberBlock numberBlock = Instantiate(blockPrefab, blocksParent.transform.position, Quaternion.identity, blocksParent.transform);
                numberBlock.number = numberList[i];
                numberBlock.numberBlockIndex = i + 1;
                numberBlocks.Add(numberBlock);
                numberBlocksCount++;
            }
        }

        public void RemoveRandomBlock(List<NumberBlock> numberBlocksList)
        {
            if (numberBlocksList.Count != 0)
            {
                numberBlocksList.RemoveAt(Random.Next(0, numberBlocksList.Count));
            }
        }

        public void RemoveRandomNumberBlocks()
        {
            numberBlockRemoveCount = Random.Next(minNumberBlockRemoveCount, maxNumberBlockRemoveCount + 1);
            currentNumberBlocks = new List<NumberBlock>(numberBlocks);
            for (int i = 0; i < numberBlockRemoveCount; i++)
            {
                RemoveRandomBlock(currentNumberBlocks);
            }
            totalSum = CalculateSum(currentNumberBlocks);
            Debug.Log($"Total sum : {totalSum}");
        }

        public int CalculateSum(List<NumberBlock> numberBlocksList)
        {
            int result = 0;
            numberBlocksList.ForEach(block => result += block.number);
            return result;
        }
        
        public void PrintALlNumberBlocks(List<NumberBlock> numberBlocksList)
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