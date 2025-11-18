using UnityEngine;

namespace Games.Maths.MalletMath
{
    public class NumberBlock : MonoBehaviour
    {
        public int number; // The number of the block
        public int numberBlockIndex; // The index of the block in the list of number blocks

        public Rigidbody2D rb;
        public SpriteRenderer sr;
        public Color blockColor;

        public override string ToString()
        {
            return "NumberBlock " + numberBlockIndex + ": Number: " + number;
        }
    }
}