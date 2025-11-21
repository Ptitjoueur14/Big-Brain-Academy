using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Games.Maths.MalletMath
{
    public class NumberBlock : MonoBehaviour
    {
        public int number; // The number of the block
        public int numberBlockIndex; // The index of the block in the list of number blocks
        public TMP_Text numberText; // The number shown on the block

        public Rigidbody2D rb;
        public SpriteRenderer sr;
        public Color blockColor;

        private void Start()
        {
            numberText.text = number.ToString();
        }

        public override string ToString()
        {
            return "NumberBlock " + numberBlockIndex + ": Number: " + number;
        }
    }
}