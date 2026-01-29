using UnityEngine;

[ExecuteAlways]
[DisallowMultipleComponent]
public sealed class SpriteViewportAnchor : MonoBehaviour
{
    [Header("Reference")]
    public Camera Camera;

    [Header("Viewport Anchor (0..1)")]
    [Tooltip("(0,0)=左下角, (1,1)=右上角")]
    public Vector2 ViewportAnchor = new Vector2(0.5f, 0.5f);

    [Header("Behavior")]
    [Tooltip("在编辑器中拖动物体时，自动更新 ViewportAnchor")]
    public bool AutoUpdateAnchorInEditor = true;

    [Tooltip("运行时是否强制应用锚点")]
    public bool ApplyAnchorInPlayMode = true;

    [Tooltip("保持当前 Z 值（2D 常用）")]
    public bool KeepZ = true;

    // ---------------- private ----------------

    private int mLastScreenWidth;
    private int mLastScreenHeight;
    private Vector3 mLastWorldPos;

    private void Reset()
    {
        Camera = Camera.main;
    }

    private void OnEnable()
    {
        mLastWorldPos = transform.position;
        ApplyAnchorToTransform();
    }

    private void OnValidate()
    {
        if (Camera == null)
            Camera = Camera.main;

        if (!Application.isPlaying && AutoUpdateAnchorInEditor)
        {
            UpdateAnchorFromTransform();
        }

        ApplyAnchorToTransform();
    }

    private void LateUpdate()
    {
        if (Camera == null)
            Camera = Camera.main;

        if (Camera == null)
            return;

        bool resolutionChanged =
            Screen.width != mLastScreenWidth ||
            Screen.height != mLastScreenHeight;

        bool positionChanged = transform.position != mLastWorldPos;

        if (!Application.isPlaying)
        {
            if (AutoUpdateAnchorInEditor && positionChanged)
            {
                UpdateAnchorFromTransform();
            }

            if (resolutionChanged || positionChanged)
            {
                ApplyAnchorToTransform();
            }
        }
        else
        {
            if (!ApplyAnchorInPlayMode)
                return;

            if (resolutionChanged)
            {
                ApplyAnchorToTransform();
            }
            else
            {
                ApplyAnchorToTransform();
            }
        }

        mLastScreenWidth = Screen.width;
        mLastScreenHeight = Screen.height;
        mLastWorldPos = transform.position;
    }

    private void UpdateAnchorFromTransform()
    {
        float z = KeepZ ? transform.position.z : 0.0f;
        float depth = z - Camera.transform.position.z;

        Vector3 viewport =
            Camera.WorldToViewportPoint(
                new Vector3(
                    transform.position.x,
                    transform.position.y,
                    Camera.transform.position.z + depth
                )
            );

        ViewportAnchor = new Vector2(
            Mathf.Clamp01(viewport.x),
            Mathf.Clamp01(viewport.y)
        );
    }

    private void ApplyAnchorToTransform()
    {
        float z = KeepZ ? transform.position.z : 0.0f;
        float depth = z - Camera.transform.position.z;

        Vector3 world =
            Camera.ViewportToWorldPoint(
                new Vector3(ViewportAnchor.x, ViewportAnchor.y, depth)
            );

        world.z = z;
        transform.position = world;
    }
}
