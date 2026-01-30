using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDataSelecter : MonoBehaviour
{
    [Header("Store")]
    [SerializeField] private int defaultIndex = 0;

    [Header("Targets (只显示一个)")]
    [SerializeField] private List<GameObject> targets = new List<GameObject>();

    [Header("Options")]
    [SerializeField] private bool applyOnEnable = true;
    [SerializeField] private bool clampIndex = true;



    [SerializeField] private int currentAppliedIndex = int.MinValue;

    private void OnEnable()
    {
        if (applyOnEnable)
        {
            Apply();
        }
    }

    [ContextMenu("Apply")]
    public void Apply()
    {
        int index = GameControllor.Instance.GetIndex(gameObject.name, defaultIndex);
        ApplyIndex(index);
    }

    public void ApplyIndex(int index)
    {
        if (targets == null || targets.Count == 0)
        {
            currentAppliedIndex = int.MinValue;
            return;
        }

        int resolvedIndex = index;

        if (clampIndex)
        {
            resolvedIndex = Mathf.Clamp(resolvedIndex, 0, targets.Count - 1);
        }
        else
        {
            if (resolvedIndex < 0 || resolvedIndex >= targets.Count)
            {
                currentAppliedIndex = index;
                return;
            }
        }

        for (int i = 0; i < targets.Count; i++)
        {
            GameObject go = targets[i];
            if (go == null)
                continue;

            go.SetActive(i == resolvedIndex);
        }

        currentAppliedIndex = index;
    }
}
