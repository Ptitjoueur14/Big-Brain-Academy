using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Games.Maths
{
    public class Balloon : MonoBehaviour
    {
        [Header("Balloon")]
        public int balloonIndex; // The index of the balloon in the list of balloons of the BalloonBurst
        public float number; //The number shown on the balloon -> balloon value
        public TMP_Text numberText;
        
        [Header("Movement Settings")]
        public Rigidbody2D rb;
        public float moveSpeed;
        public float rotationSpeed;
        public Vector2 moveDirection;
        
        [Header("Collision Settings")]
        public float collisionRadius = 0.5f;   // Approximate radius of balloon for collision
        public float collisionPush = 0.1f;     // How much to separate on collision
        public float screenPadding = 0.3f;  // Optional extra space from borders
        
        [Header("Expert Settings")]
        public bool isNumberFraction; // If the number displayed is a fraction
        public int fractionUpNumber; // The numerator of the fraction
        public int fractionDownNumber; // The denominator of the fraction
        public GameObject fractionNumberParent; // The parent of the fraction GameObjects
        public TMP_Text fractionUpText; 
        public TMP_Text fractionDownText;
        public TMP_Text fractionMiddleText; // The fraction separator (ex : "-----")
        public TMP_Text negativeSignText; // The negative sign if the fraction is negative
        
        [Header("Pop Effect")]
        public ParticleSystem popEffect; // The particle to play when the balloon is popped
        
        [Header("Balloon Color")]
        public SpriteRenderer graphics;
        public Color balloonColor;
        
        private System.Random Random;
        private Camera cam;
        private static List<Balloon> allBalloons = new List<Balloon>(); // The list of surrounding balloons
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        private void Start()
        {
            Random = new System.Random((int)number);
            float angle = Random.Next(0, 360);
            moveDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
            
            cam = Camera.main;
            if (numberText == null)
            {
                numberText = GetComponentInChildren<TMP_Text>();
            }
        }

        private void OnEnable()
        {
            allBalloons.Add(this);
        }

        private void OnDisable()
        {
            allBalloons.Remove(this);
        }

        private void FixedUpdate()
        {
            // Rotate balloon
            rb.MoveRotation(rb.rotation + rotationSpeed * Time.fixedDeltaTime);

            // Move balloon

            rb.linearVelocity = moveDirection * moveSpeed;
            
            BounceIfOutOfBounds();
            BounceOffOtherBalloons();
        }
        
        private void BounceIfOutOfBounds()
        {
            Vector3 pos = transform.position;
            Vector3 viewport = cam.WorldToViewportPoint(pos);
            bool bounced = false;

            // Horizontal bounce
            if (viewport.x < screenPadding)
            {
                moveDirection.x = Mathf.Abs(moveDirection.x); // bounce right
                bounced = true;
            }
            else if (viewport.x > 1f - screenPadding)
            {
                moveDirection.x = -Mathf.Abs(moveDirection.x); // bounce left
                bounced = true;
            }

            // Vertical bounce
            if (viewport.y < screenPadding)
            {
                moveDirection.y = Mathf.Abs(moveDirection.y); // bounce up
                bounced = true;
            }
            else if (viewport.y > 1f - screenPadding)
            {
                moveDirection.y = -Mathf.Abs(moveDirection.y); // bounce down
                bounced = true;
            }

            if (bounced)
            {
                // Clamp the balloon to inside the viewport
                viewport.x = Mathf.Clamp(viewport.x, screenPadding, 1f - screenPadding);
                viewport.y = Mathf.Clamp(viewport.y, screenPadding, 1f - screenPadding);
                transform.position = cam.ViewportToWorldPoint(viewport);
                moveDirection.Normalize();
            }
        }

        private void BounceOffOtherBalloons()
        {
            foreach (Balloon other in allBalloons)
            {
                if (other == this)
                {
                    continue;
                }

                Vector2 diff = rb.position - other.rb.position;
                float dist = diff.magnitude;
                float minDist = collisionRadius + other.collisionRadius;

                if (dist < minDist && dist > 0f)
                {
                    // Push balloons apart
                    Vector2 push = diff.normalized * ((minDist - dist) * 0.5f);
                    rb.position += push;

                    // Reflect movement angle
                    moveDirection = Vector2.Reflect(moveDirection, diff.normalized).normalized;
                }
            }
        }

        public static bool operator <(Balloon balloon1, Balloon balloon2)
        {
            return balloon1.number < balloon2.number; 
        }
    
        public static bool operator >(Balloon balloon1, Balloon balloon2)
        {
            return balloon1.number > balloon2.number;
        }

        public override string ToString()
        {
            string res = "Balloon " + balloonIndex + ": Number " + number;
            if (isNumberFraction)
            {
                res += " (" + fractionUpNumber + " / " + fractionDownNumber + ")";
            }
            return res;
        }

        public float CalculateFraction()
        {
            return (float) fractionUpNumber / fractionDownNumber;
        }
    }
}
