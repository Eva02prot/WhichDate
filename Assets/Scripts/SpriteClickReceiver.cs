using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class SpriteClickReceiver : MonoBehaviour, ISpriteClickable
{
    [Header("Debug")]
    [SerializeField] private bool logClick = true;

    [Header("Switch GameObjects")]
    [SerializeField] private List<GameObject> targets = new List<GameObject>();
    [SerializeField] private bool loop = true;
    [SerializeField] private int startIndex = 0;

    [Header("Optional")]
    [SerializeField] private bool initializeOnAwake = true;

    [SerializeField]
    private int currentIndex = -1;

    private DOTweenAnimation mTween;
    private GameObject mCurrentObj;

    private void Awake()
    {
        if (initializeOnAwake)
        {
            Initialize(startIndex);
        }
    }

    public void OnSpriteClick(Vector2 worldPos)
    {
        if (logClick)
        {
            Debug.Log($"SpriteClickReceiver Clicked: {name}", this);
        }

        if(mTween)
            mTween.DOPlay();
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
    }

    public void SwitchNext()
    {
        if (targets == null || targets.Count == 0)
            return;

        if (currentIndex < 0)
        {
            Initialize(startIndex);
            return;
        }

        int next = currentIndex + 1;

        if (next >= targets.Count)
        {
            if (!loop)
                return;

            next = 0;
        }

        currentIndex = next;
        ApplyVisible(currentIndex);

        if (mCurrentObj)
            mTween = mCurrentObj.GetComponent<DOTweenAnimation>();

        if (mTween)
            mTween.DOPlayBackwards();
    }

    private void ApplyVisible(int visibleIndex)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            GameObject go = targets[i];
            if (go == null)
                continue;

            go.SetActive(i == visibleIndex);
            mCurrentObj = go;
        }
    }
}