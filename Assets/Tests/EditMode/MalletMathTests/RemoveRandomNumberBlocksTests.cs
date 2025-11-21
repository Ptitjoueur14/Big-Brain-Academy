using System.Collections.Generic;
using Games.Maths;
using Games.Maths.MalletMath;
using NUnit.Framework;
using UnityEngine;

namespace MalletMathTests
{
    public class RemoveRandomNumberBlocksTests
    {
        public MalletMath MalletMath;

        [SetUp]
        public void Setup()
        {
            MalletMath = new GameObject().AddComponent<MalletMath>();
            MalletMath.numberBlocks = new List<NumberBlock>();
            MalletMath.blockPrefab = new GameObject().AddComponent<NumberBlock>();
            MalletMath.blocksParent = new GameObject("BlocksParent");
        }

        [TearDown]
        public void Teardown()
        {
            if (MalletMath != null)
            {
                // Clean up the GameObject after each test
                Object.DestroyImmediate(MalletMath.gameObject);
            }
        }

        [Test]
        public void RemoveRandomBlocksTest()
        {
            Difficulty.DifficultyLevel = DifficultyLevel.Easy;
            MalletMath.minNumberBlockNumber = 1;
            MalletMath.maxNumberBlockNumber = 4;
            MalletMath.minNumberBlockRemoveCount = 1;
            MalletMath.maxNumberBlockRemoveCount = 4;
            
            List<int> numberBlocks = new List<int> { 1, 3, 2, 4 };
            MalletMath.GetNumberBlockList(numberBlocks);
            MalletMath.RemoveRandomBlocks();
            MalletMath.PrintAllNumberBlocks(MalletMath.numberBlocks);
            Debug.Log($"Removed {MalletMath.numberBlockRemoveCount} blocks");
            Assert.AreEqual(MalletMath.numberBlockRemoveCount, MalletMath.numberBlocks.Count - MalletMath.currentNumberBlocks.Count);
            MalletMath.PrintAllNumberBlocks(MalletMath.currentNumberBlocks);
            
        }
    }
}