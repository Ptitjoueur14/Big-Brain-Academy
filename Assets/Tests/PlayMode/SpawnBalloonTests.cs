using System.Collections.Generic;
using Games.Maths;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode
{
    public class SpawnBallonTests
    {
        public BalloonBurst BalloonBurst;

        [SetUp]
        public void Setup()
        {
            Difficulty.DifficultyLevel = DifficultyLevel.Easy;
            BalloonBurst = new GameObject().AddComponent<BalloonBurst>();
            BalloonBurst.balloons = new List<Balloon>();
            
            BalloonBurst.minBalloonCount = 3;
            BalloonBurst.maxBalloonCount = 4;
            BalloonBurst.balloonCount = 0;

            BalloonBurst.minX = -5; BalloonBurst.maxX = 5;
            BalloonBurst.minY = -5; BalloonBurst.maxY = 5;
            BalloonBurst.minDistanceBetweenBalloons = 0.5f;

            BalloonBurst.minRotation = 0; BalloonBurst.maxRotation = 5;
            BalloonBurst.minMoveSpeed = 0; BalloonBurst.maxMoveSpeed = 0.1f;
            BalloonBurst.minRotationSpeed = 0; BalloonBurst.maxRotationSpeed = 0.05f;
            
            GameObject prefab = new GameObject("BalloonPrefab");
            BalloonBurst.balloonPrefab = prefab.AddComponent<Balloon>();
            
            BalloonBurst.balloonsParent = new GameObject("Parent");
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
        public void SpawnBalloon_Easy()
        {
            // Act
            Balloon newBalloon = BalloonBurst.SpawnBalloon();

            // Assert
            Assert.IsNotNull(newBalloon);
            Assert.AreEqual(1, BalloonBurst.balloons.Count);
            Assert.IsTrue(BalloonBurst.balloons.Contains(newBalloon));
            
            // Random value range checks
            Assert.That(BalloonBurst.totalBalloons, Is.InRange(3, 4));
            Assert.That(newBalloon.number, Is.InRange(0, 7));
            Assert.That(newBalloon.transform.rotation.eulerAngles.z, Is.InRange(0f, 5f));
            Assert.That(newBalloon.rotationSpeed, Is.InRange(0f, 0.05f));
            Assert.That(newBalloon.moveSpeed, Is.InRange(0f, 0.1f));

            Object.DestroyImmediate(newBalloon);
        }
        
        [Test]
        public void SpawnAllBalloons_Easy()
        {
            // Act
            BalloonBurst.SpawnAllBalloons();
            Balloon newBalloon = BalloonBurst.balloons[2];

            // Assert
            Assert.AreEqual(BalloonBurst.totalBalloons, BalloonBurst.balloons.Count);
            
            // Random value range checks;
            Assert.That(BalloonBurst.totalBalloons, Is.InRange(3, 4));
            Assert.That(newBalloon.number, Is.InRange(0, 7));
            Assert.That(newBalloon.transform.rotation.eulerAngles.z, Is.InRange(0f, 5f));
            Assert.That(newBalloon.rotationSpeed, Is.InRange(0f, 0.05f));
            Assert.That(newBalloon.moveSpeed, Is.InRange(0f, 0.1f));
            
            bool impossibleBalloon = BalloonBurst.SpawnBalloon();
            Assert.IsFalse(impossibleBalloon);

            Object.DestroyImmediate(newBalloon);
        }
    }
}
