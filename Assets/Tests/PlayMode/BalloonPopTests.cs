using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode
{
    public class BalloonPopTests
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
        public void PopBalloon_Correct()
        {
            // Create last popped balloon (number 5)
            Balloon lastBalloon = new GameObject().AddComponent<Balloon>();
            lastBalloon.number = 5;
            BalloonBurst.lastPoppedBalloon = lastBalloon;

            // Create new balloon to pop (number 3)
            Balloon newBalloon = new GameObject().AddComponent<Balloon>();
            newBalloon.number = 3;

            BalloonBurst.balloons.Add(newBalloon);

            // Act
            bool result = BalloonBurst.PopBallon(newBalloon);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(BalloonBurst.lastPoppedBalloon, newBalloon);
            Assert.IsFalse(BalloonBurst.balloons.Contains(newBalloon));
            
            Object.DestroyImmediate(lastBalloon);
            Object.DestroyImmediate(newBalloon);
        }

        [Test]
        public void PopBalloon_Wrong()
        {
            // Last popped is 3
            Balloon lastBalloon = new GameObject().AddComponent<Balloon>();
            lastBalloon.number = 3;
            BalloonBurst.lastPoppedBalloon = lastBalloon;

            // Try to pop a balloon with number 7 (wrong order)
            Balloon wrongBalloon = new GameObject().AddComponent<Balloon>();
            wrongBalloon.number = 7;
            BalloonBurst.balloons.Add(wrongBalloon);

            // Act
            bool result = BalloonBurst.PopBallon(wrongBalloon);

            // Assert
            Assert.IsFalse(result);                      // should not pop
            Assert.IsTrue(BalloonBurst.balloons.Contains(wrongBalloon)); // still in list
            
            Object.DestroyImmediate(lastBalloon);
            Object.DestroyImmediate(wrongBalloon);
        }
        
        [Test]
        public void AreAllBalloonsPopped_ReturnsTrueWhenEmpty()
        {
            BalloonBurst.balloons.Clear();
            Assert.IsTrue(BalloonBurst.AreAllBalloonsPopped());
        }

        [Test]
        public void AreAllBalloonsPopped_ReturnsFalseWhenNotEmpty()
        {
            Balloon balloon = new GameObject().AddComponent<Balloon>();
            BalloonBurst.balloons.Add(balloon);
 
            Assert.IsFalse(BalloonBurst.AreAllBalloonsPopped());
            Object.DestroyImmediate(BalloonBurst);
        }
    }
}
