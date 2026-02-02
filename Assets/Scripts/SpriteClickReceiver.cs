using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteClickReceiver : MonoBehaviour, ISpriteClickable { 
    [Header("Debug")][SerializeField]
    private bool logClick = true;

    [Header("Callbacks")]
    [SerializeField] private UnityEvent onClick;

    public void OnSpriteClick(Vector2 worldPos)
    {
        if (logClick) 
        {
            Debug.Log($"SpriteClickReceiver Clicked: {name}", this); 
        }

        onClick?.Invoke();
    }
}