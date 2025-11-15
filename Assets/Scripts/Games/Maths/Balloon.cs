using UnityEngine;

namespace Games.Maths
{
    public class Balloon : MonoBehaviour
    {
        public GameObject balloon;
        public float moveSpeed;
        public float rotationSpeed;
        public Rigidbody2D rb;
        public int number; //The number shown on the balloon -> balloon value
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public static bool operator <(Balloon balloon1, Balloon balloon2)
        {
            return balloon1.number < balloon2.number; 
        }
    
        public static bool operator >(Balloon balloon1, Balloon balloon2)
        {
            return balloon1.number > balloon2.number;
        }
    }
}
