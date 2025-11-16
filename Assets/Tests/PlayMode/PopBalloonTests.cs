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
                BalloonBurst.balloons.Clear();
                Object.DestroyImmediate(BalloonBurst.gameObject);
            }
        }
        
        [Test]
        public void PopBalloon_Correct_NoComparison_FirstBalloon()
        {
            Balloon newBalloon = new GameObject().AddComponent<Balloon>();
            newBalloon.number = 3;
            BalloonBurst.balloons.Add(newBalloon);
            BalloonBurst.balloonCount = 1;

            // Act
            bool result = BalloonBurst.PopBalloon(newBalloon);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(BalloonBurst.balloons.Contains(newBalloon));
            Assert.AreEqual(0, BalloonBurst.balloonCount);
            Assert.AreEqual(3, BalloonBurst.lastPoppedBalloonNumber);
            
            Object.DestroyImmediate(newBalloon);
        }
        
        [Test]
        public void PopBalloon_Correct_SmallestBalloon()
        {
            Balloon bigBalloon = new GameObject().AddComponent<Balloon>();
            bigBalloon.number = 7;
            BalloonBurst.balloons.Add(bigBalloon);
            BalloonBurst.balloonCount++;
            
            Balloon smallBalloon = new GameObject().AddComponent<Balloon>();
            smallBalloon.number = 5;
            BalloonBurst.balloons.Add(smallBalloon);
            BalloonBurst.balloonCount++;

            // Act
            bool result = BalloonBurst.PopBalloon(smallBalloon);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(BalloonBurst.balloons.Contains(smallBalloon));
            Assert.AreEqual(1, BalloonBurst.balloonCount);
            Assert.AreEqual(5, BalloonBurst.lastPoppedBalloonNumber);
            
            Object.DestroyImmediate(bigBalloon);
            Object.DestroyImmediate(smallBalloon);
        }

        [Test]
        public void PopBalloon_Wrong_NotSmallestBalloon()
        {
            Balloon smallBalloon = new GameObject().AddComponent<Balloon>();
            smallBalloon.number = 3;
            BalloonBurst.balloons.Add(smallBalloon);
            BalloonBurst.balloonCount++;

            // Try to pop a bigger balloon (wrong order)
            Balloon bigBalloon = new GameObject().AddComponent<Balloon>();
            bigBalloon.number = 6;
            BalloonBurst.balloons.Add(bigBalloon);
            BalloonBurst.balloonCount++;

            // Act
            bool result = BalloonBurst.PopBalloon(bigBalloon);

            // Assert
            Assert.IsFalse(result); // should not pop
            Assert.IsTrue(BalloonBurst.balloons.Contains(bigBalloon)); // still in list
            Assert.AreEqual(2, BalloonBurst.balloonCount);
            
            Object.DestroyImmediate(smallBalloon);
            Object.DestroyImmediate(bigBalloon);
        }
        
        [Test]
        public void PopBalloon_MultipleBalloons()
        {
            Balloon balloon1 = new GameObject().AddComponent<Balloon>();
            balloon1.number = -87;
            BalloonBurst.balloons.Add(balloon1);
            
            Balloon balloon2 = new GameObject().AddComponent<Balloon>();
            balloon2.number = -21;
            BalloonBurst.balloons.Add(balloon2);
            
            Balloon balloon3 = new GameObject().AddComponent<Balloon>();
            balloon3.number = 14;
            BalloonBurst.balloons.Add(balloon3);

            BalloonBurst.balloonCount = 3;

            // Act
            bool result2 = BalloonBurst.PopBalloon(balloon2);
            Assert.IsFalse(result2);
            bool result3 = BalloonBurst.PopBalloon(balloon3);
            Assert.IsFalse(result3);
            
            bool result1 = BalloonBurst.PopBalloon(balloon1);
            Assert.IsTrue(result1);
            Assert.IsFalse(BalloonBurst.balloons.Contains(balloon1));
            Assert.AreEqual(-87, BalloonBurst.lastPoppedBalloonNumber);
            Assert.AreEqual(2, BalloonBurst.balloonCount);
            
            result3 = BalloonBurst.PopBalloon(balloon3);
            Assert.IsFalse(result3);
            result2 = BalloonBurst.PopBalloon(balloon2);
            Assert.IsTrue(result2);
            Assert.IsFalse(BalloonBurst.balloons.Contains(balloon2));
            Assert.AreEqual(-21, BalloonBurst.lastPoppedBalloonNumber);
            Assert.AreEqual(1, BalloonBurst.balloonCount);
            
            Object.DestroyImmediate(balloon1);
            Object.DestroyImmediate(balloon2);
            Object.DestroyImmediate(balloon3);
            BalloonBurst.balloons.Clear();
        }
    }
}
