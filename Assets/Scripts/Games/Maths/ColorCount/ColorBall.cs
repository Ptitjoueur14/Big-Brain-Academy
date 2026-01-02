using UnityEngine;
using UnityEngine.UIElements;

namespace Games.Maths.ColorCount
{

    public enum BallSpawnSide
    {
        LeftSide,
        RightSide,
    }
    public class ColorBall : MonoBehaviour
    {
        public int ballIndex; // The index of the ball in the list of balls
        
        [Header("Ball Color")]
        public Color ballColor;
        public Sprite redBallSprite;
        public Sprite blueBallSprite;
        
        [Header("Ball Spawn Side")]
        public BallSpawnSide ballSpawnSide; // The side of the screen where the ball spawns
        public Vector2 ballSpawnPositionLeft;
        public Vector2 ballSpawnPositionRight;
        
        //public Rigidbody rb;
        public SpriteRenderer sr;
        public Animator animator;

        private void Start()
        {
            if (ballSpawnSide == BallSpawnSide.LeftSide)
            {
                transform.position = ballSpawnPositionLeft;
                animator.Play("BallMovementLeft");
            }
            else
            {
                transform.position = ballSpawnPositionRight;
                animator.Play("BallMovementRight");
            }
            if (ballColor == Color.red)
            {
                sr.sprite = redBallSprite;
            }
            else
            {
                sr.sprite = blueBallSprite;
            }
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                animator.speed = 1.5f;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                animator.speed = 1;
            }
        }

        public override string ToString()
        {
            string result = "";
            if (ballColor == Color.red)
            {
                result += "Red";
            }
            else
            {
                result += "Blue";
            }
            result += " Ball " + ballIndex.ToString();
            return result;
        }
    }
}