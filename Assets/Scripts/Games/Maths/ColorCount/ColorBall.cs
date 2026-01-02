using UnityEngine;

namespace Games.Maths.ColorCount
{
    public class ColorBall : MonoBehaviour
    {
        public int ballIndex; // The index of the ball in the list of balls
        
        [Header("Ball Color")]
        public Color ballColor;
        public Sprite redBallSprite;
        public Sprite blueBallSprite;
        
        public Rigidbody rb;
        public SpriteRenderer sr;
        public Animator animator;

        private void Start()
        {
            if (ballColor == Color.red)
            {
                sr.sprite = redBallSprite;
            }
            else
            {
                sr.sprite = blueBallSprite;
            }
            animator.Play("BallMovementAnimation");
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