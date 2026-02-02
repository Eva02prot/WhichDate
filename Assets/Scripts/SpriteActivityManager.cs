using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteActivityManager : MonoBehaviour
{
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
            targetTrans.gameObject.SetActive(!targetTrans.gameObject.activeSelf);
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

    public void ClearSnapshot()
    {
        mSnapshot = null;
        mHasSnapshot = false;
    }
}
