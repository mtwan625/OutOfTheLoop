using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterAnimator : MonoBehaviour
{
    Animator animator;
    WireManager wireManager;
    public bool isAnimating = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        wireManager = GameObject.Find("GameManager").GetComponent<WireManager>();
    }

    void Update()
    {
        if (wireManager.isHolding && wireManager.isCutting)
        {
            animator.SetTrigger("Cut");
            wireManager.isCutting = false;
            isAnimating = true;
        }
    }

    public void AnimationEnded()
    {
        isAnimating = false;
    }
}
