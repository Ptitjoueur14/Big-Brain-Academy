using System.Collections.Generic;
using Games.Maths;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode
{
    public class PopBalloonTests
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
        public void PopBalloon_Correct_Comparison_PreviousBalloon()
        {
            // Create last popped balloon (number 5)
            Balloon lastBalloon = new GameObject().AddComponent<Balloon>();
            lastBalloon.number = 3;
            BalloonBurst.lastPoppedBalloon = lastBalloon;

            // Create new balloon to pop (number 3)
            Balloon newBalloon = new GameObject().AddComponent<Balloon>();
            newBalloon.number = 5;

            BalloonBurst.balloons.Add(newBalloon);

            // Act
            bool result = BalloonBurst.PopBalloon(newBalloon);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(BalloonBurst.lastPoppedBalloon, newBalloon);
            Assert.IsFalse(BalloonBurst.balloons.Contains(newBalloon));
            
            Object.DestroyImmediate(lastBalloon);
            Object.DestroyImmediate(newBalloon);
        }
        
        [Test]
        public void PopBalloon_Correct_NoComparison_FirstBalloon()
        {
            // Create new balloon to pop (number 3)
            Balloon newBalloon = new GameObject().AddComponent<Balloon>();
            newBalloon.number = 3;

            BalloonBurst.balloons.Add(newBalloon);

            // Act
            bool result = BalloonBurst.PopBalloon(newBalloon);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(BalloonBurst.lastPoppedBalloon, newBalloon);
            Assert.IsFalse(BalloonBurst.balloons.Contains(newBalloon));
            
            Object.DestroyImmediate(newBalloon);
        }

        [Test]
        public void PopBalloon_Wrong_Comparison_PreviousBalloon()
        {
            // Last popped is 3
            Balloon lastBalloon = new GameObject().AddComponent<Balloon>();
            lastBalloon.number = 3;
            BalloonBurst.lastPoppedBalloon = lastBalloon;

            // Try to pop a balloon with number 2 (wrong order)
            Balloon wrongBalloon = new GameObject().AddComponent<Balloon>();
            wrongBalloon.number = 2;
            BalloonBurst.balloons.Add(wrongBalloon);

            // Act
            bool result = BalloonBurst.PopBalloon(wrongBalloon);

            // Assert
            Assert.IsFalse(result);                      // should not pop
            Assert.IsTrue(BalloonBurst.balloons.Contains(wrongBalloon)); // still in list
            
            Object.DestroyImmediate(lastBalloon);
            Object.DestroyImmediate(wrongBalloon);
        }
        
        [Test]
        public void PopBalloon_Wrong_NoComparison_FirstBalloon()
        {
            Balloon otherBalloon = new GameObject().AddComponent<Balloon>();
            otherBalloon.number = 3;
            BalloonBurst.balloons.Add(otherBalloon);
            
            // Try to pop a balloon with number 5 (wrong order)
            Balloon wrongBalloon = new GameObject().AddComponent<Balloon>();
            wrongBalloon.number = 5;
            BalloonBurst.balloons.Add(wrongBalloon);

            // Act
            bool result = BalloonBurst.PopBalloon(wrongBalloon);

            // Assert
            Assert.IsFalse(result);                      // should not pop
            Assert.IsTrue(BalloonBurst.balloons.Contains(wrongBalloon)); // still in list
            
            Object.DestroyImmediate(otherBalloon);
            Object.DestroyImmediate(wrongBalloon);
        }
    }
}
