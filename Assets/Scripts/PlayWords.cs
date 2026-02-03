using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWords : ResettableBehaviour
{
    private void OnEnable()
    {
        PlayAllWords();
    }

    public void PlayAllWords()
    {
        int count = transform.childCount;

        for (int i = 0; i < count; ++i)
        {
            Transform child = transform.GetChild(i);
            var tween = child.GetComponent<DOTweenAnimation>();
            tween?.DOPlay();
        }
    }

    public override void Reset()
    {
        int count = transform.childCount;

        for (int i = 0; i < count; ++i)
        {
            Transform child = transform.GetChild(i);
            var tween = child.GetComponent<DOTweenAnimation>();
            tween?.DOGotoAndPause(0.0f);
        }
    }

}
