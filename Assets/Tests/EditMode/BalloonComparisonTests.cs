using NUnit.Framework;
using UnityEngine;
using Assert = NUnit.Framework.Assert;

namespace Tests.EditMode
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
    }
}