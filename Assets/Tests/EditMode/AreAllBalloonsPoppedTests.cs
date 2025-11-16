using System.Collections.Generic;
using Games.Maths;
using NUnit.Framework;
using UnityEngine;

namespace BalloonBurstTests
{
    public class AreAllBalloonsPoppedTests
    {
        public BalloonBurst BalloonBurst;
        
        [SetUp]
        public void Setup()
        {
            BalloonBurst = new GameObject().AddComponent<BalloonBurst>();
            BalloonBurst.balloons = new List<Balloon>(); 
        }
        
        [TearDown]
        public void Teardown()
        {
            if (BalloonBurst != null)
            {
                // Clean up the GameObject after each test
                Object.DestroyImmediate(BalloonBurst.gameObject);
            }
        }
        
        [Test]
        public void AreAllBalloonsPopped_Empty()
        {
            BalloonBurst.balloons.Clear();
            Assert.IsTrue(BalloonBurst.AreAllBalloonsPopped());
        }

        [Test]
        public void AreAllBalloonsPopped_NotEmpty()
        {
            Balloon balloon = new GameObject().AddComponent<Balloon>();
            BalloonBurst.balloons.Add(balloon);
 
            Assert.IsFalse(BalloonBurst.AreAllBalloonsPopped());
            Object.DestroyImmediate(BalloonBurst);
        }
    }
}
