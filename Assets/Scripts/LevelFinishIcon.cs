using System;
using UnityEngine;

public class LevelFinishIcon : MonoBehaviour
{
    public Animator animator;
    public float animationTime;

    private void Start()
    {
        animator.Play("LevelFinish");
        Destroy(gameObject, animationTime);
    }
}