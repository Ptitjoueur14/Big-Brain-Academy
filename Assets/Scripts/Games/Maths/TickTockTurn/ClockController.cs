using System;
using UnityEngine;

namespace Games.Maths.TickTockTurn
{
    public class ClockController : MonoBehaviour
    {
        [Header("Clock Hands")]
        public Transform hourHand;
        public Transform minuteHand;
        public float hourHandAngle;
        public float minuteHandAngle;

        [Header("Clock Time")]
        public TickTockTurn tickTockTurn;
        [Range(1, 12)]
        public int currentSector = 12;
        [Range(1, 12)]
        public int previousSector = 12;
        public bool isDragging;
        
        private const float DegreesPerSector = 360f / 12f;
        
        private void OnMouseDown()
        {
            isDragging = true;
            Debug.Log("Clock clicked");
        }

        private void OnMouseUp()
        {
            isDragging = false;
            Debug.Log("Clock released");
        }

        private void Update()
        {
            if (!isDragging)
                return;

            UpdateClockFromMouse();
        }

        public void UpdateClockFromMouse()
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mouseWorld - transform.position;

            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle += 360f;
            }

            int newSector = Mathf.RoundToInt(angle / DegreesPerSector);
            
            if (newSector == 0) // Top = 12th sector
            {
                newSector = 12;
            }
            newSector = Mathf.Clamp(newSector, 1, 12);

            if (newSector == currentSector)
            {
                return;
            }
            
            int deltaSectors = GetSectorDelta(currentSector, newSector);
            int deltaMinutes = deltaSectors * 5;
            tickTockTurn.turnedTime += deltaMinutes;
            Debug.Log($"Added {deltaMinutes} time");

            previousSector = currentSector;
            currentSector = newSector;
            ApplyRotation();
            SendTimeToGame();
        }

        public void ApplyRotation()
        {
            float angleMinutes = -(currentSector % 12 * DegreesPerSector);
            minuteHand.localRotation = Quaternion.Euler(0, 0, angleMinutes);
            
            float angleHours = -(tickTockTurn.currentHourClockSector % 12 * DegreesPerSector);
            hourHand.localRotation = Quaternion.Euler(0, 0, angleHours);
        }

        public void SendTimeToGame()
        {
            tickTockTurn.currentMinuteClockSector = currentSector;
        }
        
        // Returns the amount of sectors moved (left or right)
        public int GetSectorDelta(int startSector, int endSector)
        {
            int sectorDelta = endSector - startSector;

            if (startSector == 12 && endSector == 1)
            {
                sectorDelta = 1;
            }
            
            else if (startSector == 1 && endSector == 12)
            {
                sectorDelta = -1;
            }
            
            Debug.Log($"Moved the clock {startSector} -> {endSector} with sector delta: {sectorDelta}");
            UpdateClock(sectorDelta);
            return sectorDelta;
        }

        public void UpdateClock(int sectorDelta)
        {
            tickTockTurn.currentHourClockSector += (float) Math.Round(sectorDelta / 12f, 4);
            float angle = sectorDelta * DegreesPerSector;
            minuteHandAngle += (float) Math.Round(angle, 4);
            hourHandAngle += (float) Math.Round(angle / 12f, 4);
            minuteHandAngle %= 360;
            hourHandAngle %= 360;
        }
    }
}