using NUnit.Framework;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace TimerTests
{
    public class TimerTests
    {
        public Timer Timer;
        
        [SetUp]
        public void Setup()
        {
            Timer = new GameObject().AddComponent<Timer>();
            // Reset Timers
            Timer.gameTimer = 0;
            Timer.currentLevelTimer = 0;
            Timer.previousLevelTimer = 0;
            
            Timer.gameTimerText = new GameObject().AddComponent<TextMeshProUGUI>();
            Timer.currentLevelTimerText = new GameObject().AddComponent<TextMeshProUGUI>(); 
            Timer.previousLevelTimerText = new GameObject().AddComponent<TextMeshProUGUI>(); 
            Timer.gameTimerText.text = "0.000 s";
            Timer.currentLevelTimerText.text = "0.000 s";
            Timer.previousLevelTimerText.text = "0.000 s";
        }
        
        [TearDown]
        public void Teardown()
        {
            if (Timer != null)
            {
                // Clean up the GameObject after each test
                Object.DestroyImmediate(Timer.gameObject);
            }
        }

        [Test]
        public void GetSeconds()
        {
            Assert.AreEqual(0, Timer.GetSeconds(0));
            Assert.AreEqual(0, Timer.GetSeconds(542));
            Assert.AreEqual(9, Timer.GetSeconds(9452));
        }
        
        [Test]
        public void GetMilliseconds()
        {
            Assert.AreEqual(0, Timer.GetMilliseconds(0));
            Assert.AreEqual(542, Timer.GetMilliseconds(542));
            Assert.AreEqual(452, Timer.GetMilliseconds(9452));
        }
        
        [Test]
        public void DisplayTimer()
        {
            Timer.gameTimer = 3527; // 3527 ms -> "3.527 s"
            Assert.AreEqual("3.527 s", Timer.DisplayTimer(Timer.gameTimer));
        }
        
        [Test]
        public void UpdateTimerTexts()
        {
            Timer.gameTimer = 3527; // 3527 ms -> "3.527 s"
            Timer.currentLevelTimer = 1258; // 1258 ms -> "1.258 s"
            Timer.UpdateTimerTexts();
            Assert.AreEqual("3.527 s", Timer.gameTimerText.text);
            Assert.AreEqual("1.258 s", Timer.currentLevelTimerText.text);
        }
        
        [Test]
        public void UpdateLevelTimers()
        {
            Timer.currentLevelTimer = 1258; // 1258 ms -> "1.258 s"
            Timer.UpdateLevelTimers();
            
            Assert.AreEqual(0, Timer.currentLevelTimer);
            Assert.AreEqual("0.000 s", Timer.DisplayTimer(Timer.currentLevelTimer));
            Assert.AreEqual("0.000 s", Timer.currentLevelTimerText.text);
            
            Assert.AreEqual(1258, Timer.previousLevelTimer);
            Assert.AreEqual("1.258 s", Timer.DisplayTimer(Timer.previousLevelTimer));
            Assert.AreEqual("1.258 s", Timer.previousLevelTimerText.text);
        }
    }
}