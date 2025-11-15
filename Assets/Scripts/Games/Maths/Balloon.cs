using System;
using UnityEngine;

namespace Games.Maths
{
    public class Balloon : MonoBehaviour
    {
        public float moveSpeed;
        public float rotationSpeed;
        public Rigidbody2D rb;
        public int number; //The number shown on the balloon -> balloon value
        public SpriteRenderer graphics;
        public System.Random Random;
        public int randomMoveDirection;

        private void Awake()
        {
            Random = new System.Random(number);
            randomMoveDirection = Random.Next(0, 360);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            // TODO : Balloon moves in random direction and avoids border/other balloons
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); // FIX
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
