using Games.Maths;
using NUnit.Framework;
using UnityEngine;
using Assert = NUnit.Framework.Assert;

namespace BalloonTests
{
    public class BalloonComparisonTests
    {
        [Test] 
        public void BalloonComparison_Lower()
        { 
            Balloon balloon1 = new GameObject().AddComponent<Balloon>();
            balloon1.number = 3;

            Balloon balloon2 = new GameObject().AddComponent<Balloon>();
            balloon2.number = 7;

            Assert.IsTrue(balloon1 < balloon2);
            Assert.IsFalse(balloon2 < balloon1);
        }
        
        [Test] 
        public void BalloonComparison_Greater()
        {
            Balloon balloon1 = new GameObject().AddComponent<Balloon>();
            balloon1.number = 3;

            Balloon balloon2 = new GameObject().AddComponent<Balloon>();
            balloon2.number = 7;
            
            Assert.IsTrue(balloon2 > balloon1);
            Assert.IsFalse(balloon1 > balloon2);
        }
        
        [Test] 
        public void CalculateFraction()
        {
            Balloon expertBalloon = new GameObject().AddComponent<Balloon>();
            expertBalloon.fractionUpNumber = 2;
            expertBalloon.fractionDownNumber = 3;
            Assert.AreEqual(0.66f, expertBalloon.CalculateFraction(), 0.01f);
            
            Object.DestroyImmediate(expertBalloon);
        }

    }
}