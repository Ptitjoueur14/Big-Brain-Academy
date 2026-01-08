using System;
using TMPro;
using UnityEngine;

namespace Modes
{
    public class BrainCloud : MonoBehaviour
    {
        public TMP_Text brainMassNumberText;
        public Animator animator;
        public float destroyDelay; // The time in seconds to wait before destroying the brain cloud

        private void Start()
        {
            animator.Play("BrainCloudMovement");
            Destroy(gameObject, destroyDelay);
        }
    }
}