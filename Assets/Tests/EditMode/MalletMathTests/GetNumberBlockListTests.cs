using System.Collections.Generic;
using Games.Maths;
using Games.Maths.MalletMath;
using NUnit.Framework;
using UnityEngine;

namespace MalletMathTests
{
    public class GetNumberBlockListTests
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
        public void GetNumberBlockList_Easy()
        {
            Difficulty.DifficultyLevel = DifficultyLevel.Easy;
            MalletMath.minNumberBlockNumber = 1;
            MalletMath.maxNumberBlockNumber = 4;

            int blocksCount = 4;
            List<int> numberBlocks = new List<int> { 1, 3, 2, 4 };
            MalletMath.GetNumberBlockList(numberBlocks);
            Assert.AreEqual(blocksCount, MalletMath.numberBlocks.Count);
            int i = 0;
            foreach (NumberBlock numberBlock in MalletMath.numberBlocks)
            {
                Assert.That(numberBlock.number, Is.InRange(1, 4));
                Assert.AreEqual(i + 1, numberBlock.numberBlockIndex);
                i++;
            }
        }
    }
}