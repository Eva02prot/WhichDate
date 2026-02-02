using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : ResettableBehaviour
{
    [Header("Switch GameObjects")]
    [SerializeField]
    private List<GameObject> targets = new List<GameObject>();

    [SerializeField]
    private bool loop = true;

    [SerializeField]
    private int startIndex = 0;

    [Header("Optional")]
    [SerializeField]
    private bool initializeOnAwake = true;

    [SerializeField]
    private int currentIndex = -1;

    private GameObject mCurrentObj;

    private DOTweenAnimation mAnimator;

    private void Awake()
    {
        if (initializeOnAwake)
        {
            Initialize(startIndex);
        }
    }

    public void Initialize(int index)
    {
        if (targets == null || targets.Count == 0)
        {
            currentIndex = -1;
            return;
        }

        index = Mathf.Clamp(index, 0, targets.Count - 1);

        currentIndex = index;
        ApplyVisible(currentIndex);

        SaveCurrentData(gameObject.name, currentIndex);
    }

    public void SwitchNext()
    {
        if (mAnimator)
            mAnimator.DOGotoAndPause(0f);

        if (targets == null || targets.Count == 0) return;

        if (currentIndex < 0)
        {
            Initialize(startIndex);
            return;
        }

        int next = currentIndex + 1;
        if (next >= targets.Count)
        {
            if (!loop) return;
            next = 0;
        }

        currentIndex = next;
        ApplyVisible(currentIndex);

        SaveCurrentData(gameObject.name, currentIndex);
    }

    private void ApplyVisible(int visibleIndex)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            GameObject go = targets[i];
            if (go == null)
                continue;
            go.SetActive(i == visibleIndex);
        }
    }

    public void SaveCurrentData(string name, int index)
    {
        if (GameControllor.Instance == null)
            return;

        GameControllor.Instance.SetIndex(name, index);
    }

    public void PlayIndexAnimation()
    {
        mCurrentObj = targets[currentIndex];
        mAnimator = mCurrentObj.GetComponent<DOTweenAnimation>();
        if (mAnimator)
            mAnimator.DOPlay();
    }

    public override void Reset()
    {
        startIndex = 0;
        currentIndex = -1;
        mCurrentObj = null;
        mAnimator = null;
        Initialize(startIndex);
    }
}
