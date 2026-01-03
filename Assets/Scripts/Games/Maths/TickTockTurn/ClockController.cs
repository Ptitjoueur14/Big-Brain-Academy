using UnityEngine;

namespace Games.Maths.TickTockTurn
{
    public class ClockController : MonoBehaviour
    {
        [Header("Clock Hands")]
        public Transform hourHand;
        public Transform minuteHand;

        [Header("Clock Time")]
        public TickTockTurn tickTockTurn;
        [Range(1, 12)]
        public int currentSector = 12;
        public int previousSector = 12;
        public bool isDragging;
        
        private const float DegreesPerSector = 360f / 12f;
        
        void OnMouseDown()
        {
            isDragging = true;
            Debug.Log("Clock clicked");
        }

        void OnMouseUp()
        {
            isDragging = false;
            Debug.Log("Clock released");
        }

        void Update()
        {
            if (!isDragging)
                return;

            UpdateClockFromMouse();
        }

        private void UpdateClockFromMouse()
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

        private void ApplyRotation()
        {
            float zRotation = -(currentSector % 12 * DegreesPerSector);
            minuteHand.localRotation = Quaternion.Euler(0, 0, zRotation);
            hourHand.localRotation = Quaternion.Euler(0, 0, zRotation / 12f);
        }

        private void SendTimeToGame()
        {
            int minutes = currentSector % 12 * 5;
            int hours = currentSector;
            
            tickTockTurn.currentMinuteClockSector = currentSector;
            tickTockTurn.currentHourClockSector = currentSector / 12f;

            tickTockTurn.currentMinute = currentSector % 12 * 5;
            tickTockTurn.currentHour = currentSector % 12 / 12;
            tickTockTurn.currentTime = tickTockTurn.currentHour * 60 + tickTockTurn.currentMinute;
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
            return sectorDelta;
        }
    }
}