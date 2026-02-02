using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimController : MonoBehaviour
{
    private Animator mAnimator;
    private string mStateName;

    bool isOpen = false;

    private void Awake()
    {
        if (mAnimator == null)
            mAnimator = GetComponent<Animator>();
        mAnimator.speed = 0.0f;
    }

    public void Play()
    {
        if (mAnimator == null) return;
        mAnimator.speed = 1f;
        mAnimator.Play(mStateName, 0, 0f);
        mAnimator.Update(0f);
    }

    public void ResetToStart()
    {
        if (mAnimator == null) return;
        mAnimator.speed = 0f;
        mAnimator.Play(mStateName, 0, 0f);
        mAnimator.Update(0f);
    }

    public void PlayReverse()
    {
        if (mAnimator == null) return;
        mAnimator.speed = -1f;
        mAnimator.Play(mStateName, 0, 1f);
        mAnimator.Update(0f);
    }

    public void Pause()
    {
        if (mAnimator == null) return;
        mAnimator.speed = 0f;
    }

    public void Resume()
    {
        if (mAnimator == null) return;
        mAnimator.speed = 1f;
    }

    public void onTouch() {
        if (mAnimator == null) return;
        isOpen = !isOpen;
        if (isOpen)
            mAnimator.speed = 5f;
        else 
            mAnimator.speed = -5f;
        mAnimator.Play(mStateName, 0, 0f);
        mAnimator.Update(0f);
    }
}

