using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpriteActivityManager : ResettableBehaviour
{
    public List<string> defaultActiveList;
    private struct ChildState
    {
        public Transform Child;
        public bool ActiveSelf;
    }

    private ChildState[] mSnapshot;
    private bool mHasSnapshot;

    public void SwitchActivityByName(string name) {
        var targetTrans = transform.Find(name);

        if (targetTrans != null)
        {
            bool isActive = !targetTrans.gameObject.activeSelf;
            targetTrans.gameObject.SetActive(isActive);
        }
    }

    public void SetActiveByName(string name) {
        var targetTrans = transform.Find(name);
        if (targetTrans != null)
        {
            targetTrans.gameObject.SetActive(true);
        }
    }

    public void SetDisactiveByName(string name)
    {
        var targetTrans = transform.Find(name);
        if (targetTrans != null)
        {
            targetTrans.gameObject.SetActive(false);
        }
    }

    public void ToggleActivityByName(string name)
    {
        SaveSnapshot();
        Transform target = null;

        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            bool isTarget = child.name == name;

            child.gameObject.SetActive(isTarget);

            if (isTarget)
                target = child;
        }

#if UNITY_EDITOR
        if (target == null)
        {
            Debug.LogWarning($"Child '{name}' not found");
        }
#endif
    }

    public void SaveSnapshot()
    {
        int count = transform.childCount;
        mSnapshot = new ChildState[count];

        for (int i = 0; i < count; ++i)
        {
            Transform child = transform.GetChild(i);
            mSnapshot[i] = new ChildState
            {
                Child = child,
                ActiveSelf = child.gameObject.activeSelf
            };
        }

        mHasSnapshot = true;

    }

    public void RestoreSnapshot()
    {
        if (!mHasSnapshot || mSnapshot == null)
            return;

        for (int i = 0; i < mSnapshot.Length; ++i)
        {
            Transform child = mSnapshot[i].Child;
            if (child == null)
                continue;

            child.gameObject.SetActive(mSnapshot[i].ActiveSelf);
        }
    }

    public void CloseAllChildren()
    {
        int count = transform.childCount;

        for (int i = 0; i < count; ++i) { 
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }

    public void ClearSnapshot()
    {
        mSnapshot = null;
        mHasSnapshot = false;
    }

    public override void Reset()
    {
        CloseAllChildren();

        foreach (var name in defaultActiveList)
        {
            var targetTrans = transform.Find(name);
            if (targetTrans != null)
            {
                targetTrans.gameObject.SetActive(true);
            }
        }
    }
}
