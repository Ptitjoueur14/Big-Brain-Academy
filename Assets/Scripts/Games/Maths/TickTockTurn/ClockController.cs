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

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90f;
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

            tickTockTurn.currentMinute = minutes;
            tickTockTurn.currentHour = hours;
            tickTockTurn.currentTime = hours * 60 + minutes;
        }
    }
}