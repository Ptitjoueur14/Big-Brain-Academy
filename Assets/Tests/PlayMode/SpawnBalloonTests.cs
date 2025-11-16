using System.Collections.Generic;
using Games.Maths;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace BalloonBurstTests
{
    public class SpawnBalloonTests
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
            BalloonBurst.totalBalloons = 3;

            BalloonBurst.minNumber = 0;
            BalloonBurst.maxNumber = 7;

            BalloonBurst.minX = -5; BalloonBurst.maxX = 5;
            BalloonBurst.minY = -5; BalloonBurst.maxY = 5;
            BalloonBurst.minDistanceBetweenBalloons = 0.5f;

            BalloonBurst.minRotation = 0; BalloonBurst.maxRotation = 5;
            BalloonBurst.minMoveSpeed = 0; BalloonBurst.maxMoveSpeed = 0.1f;
            BalloonBurst.minRotationSpeed = 0; BalloonBurst.maxRotationSpeed = 0.05f;
            
            GameObject prefab = new GameObject("BalloonPrefab");
            prefab.AddComponent<Rigidbody2D>();
            
            Balloon balloon = prefab.AddComponent<Balloon>();
            BalloonBurst.balloonPrefab = balloon;
            
            SpriteRenderer renderer = prefab.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(new Texture2D(5, 5), new Rect(0, 0, 5, 5), new Vector2(0.5f, 0.5f));
            balloon.graphics = renderer;
            
            BalloonBurst.balloonColors = new List<Sprite> { renderer.sprite };
            
            GameObject number= new GameObject("NumberText");
            number.transform.SetParent(prefab.transform);
            TextMeshProUGUI numberText = number.AddComponent<TextMeshProUGUI>();
            balloon.numberText = numberText;
            
            //Fractions
            balloon.fractionNumberParent = new GameObject("Fraction Parent");
            balloon.fractionNumberParent.transform.SetParent(prefab.transform);
            
            balloon.fractionUpText = new GameObject("FractionUp").AddComponent<TextMeshProUGUI>();
            balloon.fractionMiddleText = new GameObject("FractionMiddle").AddComponent<TextMeshProUGUI>();
            balloon.fractionDownText = new GameObject("FractionDown").AddComponent<TextMeshProUGUI>();
            balloon.negativeSignText = new GameObject("NegativeSign").AddComponent<TextMeshProUGUI>();
            
            balloon.fractionUpText.transform.SetParent(balloon.fractionNumberParent.transform); 
            balloon.fractionMiddleText.transform.SetParent(balloon.fractionNumberParent.transform); 
            balloon.fractionDownText.transform.SetParent(balloon.fractionNumberParent.transform); 
            balloon.negativeSignText.transform.SetParent(balloon.fractionNumberParent.transform); 

            BalloonBurst.maxFractionNumber = 9;
            BalloonBurst.fractionProportion = 0.3f;
            
            BalloonBurst.balloons.Clear();
            BalloonBurst.balloonCount = 0;
            
            BalloonBurst.balloonsParent = new GameObject("Parent");
        }

        [TearDown]
        public void Teardown()
        {
            if (BalloonBurst != null)
            {
                Object.DestroyImmediate(BalloonBurst.gameObject);
                if (BalloonBurst.balloonPrefab != null)
                    Object.DestroyImmediate(BalloonBurst.balloonPrefab.gameObject);
                if (BalloonBurst.balloonsParent != null)
                    Object.DestroyImmediate(BalloonBurst.balloonsParent);
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
            float z = newBalloon.transform.rotation.eulerAngles.z;
            if (z > 180f)
            {
                z -= 360f;
            }
            Assert.That(z, Is.InRange(-5f, 5f));
            Assert.That(newBalloon.rotationSpeed, Is.InRange(-0.05f, 0.05f));
            Assert.That(newBalloon.moveSpeed, Is.InRange(0f, 0.1f));

            Object.DestroyImmediate(newBalloon);
        }
        
        [Test]
        public void SpawnAllBalloons_Easy()
        {
            // Act
            BalloonBurst.SpawnAllBalloons();

            // Assert
            Assert.That(BalloonBurst.totalBalloons, Is.InRange(3, 4));
            Assert.AreEqual(BalloonBurst.totalBalloons, BalloonBurst.balloons.Count);
            Assert.Greater(BalloonBurst.balloons.Count, 1);
            Balloon newBalloon = BalloonBurst.balloons[1];
            
            // Random value range checks;
            Assert.That(newBalloon.number, Is.InRange(0, 7));
            float z = newBalloon.transform.rotation.eulerAngles.z;
            if (z > 180f)
            {
                z -= 360f;
            }
            Assert.That(z, Is.InRange(-5f, 5f));
            Assert.That(newBalloon.rotationSpeed, Is.InRange(-0.05f, 0.05f));
            Assert.That(newBalloon.moveSpeed, Is.InRange(0f, 0.1f));
            
            bool impossibleBalloon = BalloonBurst.SpawnBalloon();
            Assert.IsFalse(impossibleBalloon);

            Object.DestroyImmediate(newBalloon);
        }
    }
}
