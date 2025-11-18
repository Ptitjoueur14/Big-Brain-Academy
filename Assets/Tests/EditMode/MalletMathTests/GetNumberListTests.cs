using System.Collections.Generic;
using Games.Maths;
using Games.Maths.MalletMath;
using NUnit.Framework;
using UnityEngine;

namespace MalletMathTests
{
    public class GetNumberListTests
    {
        public MalletMath MalletMath;
        
        [SetUp]
        public void Setup()
        {
            MalletMath = new GameObject().AddComponent<MalletMath>();
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
        public void GetNumberList_Easy()
        {
            Difficulty.DifficultyLevel = DifficultyLevel.Easy;
            MalletMath.minNumberBlockNumber = 1;
            MalletMath.maxNumberBlockNumber = 4;
            
            int blocksCount = 3;
            List<int> numberBlocks = MalletMath.GetNumberList(blocksCount);
            Assert.AreEqual(blocksCount, numberBlocks.Count);
            foreach (int number in numberBlocks)
            {
                Assert.That(number, Is.InRange(1, 4));
            }
        }
    }
}