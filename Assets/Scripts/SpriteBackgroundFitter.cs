using UnityEngine;

public enum BackgroundFitMode
{
    Cover,
    Contain
}

[ExecuteAlways]
[DisallowMultipleComponent]
public sealed class SpriteBackgroundFitter : MonoBehaviour
{
    public Camera Camera;
    public SpriteRenderer Renderer;
    public BackgroundFitMode FitMode = BackgroundFitMode.Cover;

    // 是否把背景中心对齐到相机中心
    public bool FollowCamera = true;

    private int LastW;
    private int LastH;

    private void Reset()
    {
        Camera = Camera.main;
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Apply();
    }

    private void LateUpdate()
    {
        if (Screen.width != LastW || Screen.height != LastH)
        {
            Apply();
        }
        else if (!Application.isPlaying)
        {
            // 编辑器里也保持正确
            Apply();
        }
    }

    public void Apply()
    {
        if (Camera == null) Camera = Camera.main;
        if (Renderer == null) Renderer = GetComponent<SpriteRenderer>();
        if (Camera == null || Renderer == null || Renderer.sprite == null) return;

        LastW = Screen.width;
        LastH = Screen.height;

        if (!Camera.orthographic)
        {
            // 背景铺满通常用正交相机更稳定
            Camera.orthographic = true;
        }

        // 相机可视范围（世界单位）
        float viewH = Camera.orthographicSize * 2.0f;
        float viewW = viewH * Camera.aspect;

        // Sprite 在世界中的原始尺寸（世界单位，受 PPU 影响）
        Vector2 spriteSize = Renderer.sprite.bounds.size;

        float scaleX = viewW / spriteSize.x;
        float scaleY = viewH / spriteSize.y;

        float scale = (FitMode == BackgroundFitMode.Cover) ? Mathf.Max(scaleX, scaleY) : Mathf.Min(scaleX, scaleY);
        transform.localScale = new Vector3(scale, scale, 1.0f);

        if (FollowCamera)
        {
            Vector3 camPos = Camera.transform.position;
            transform.position = new Vector3(camPos.x, camPos.y, transform.position.z);
        }
    }
}