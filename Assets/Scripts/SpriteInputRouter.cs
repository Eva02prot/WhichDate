using UnityEngine;

public class SpriteInputRouter : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (mainCamera == null)
            return;

        if (!TryGetPointerDownScreenPos(out Vector2 screenPos))
            return;

        Vector2 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider == null)
            return;

        ISpriteClickable clickable = hit.collider.GetComponent<ISpriteClickable>();
        if (clickable != null)
        {
            clickable.OnSpriteClick(worldPos);
        }
    }

    private bool TryGetPointerDownScreenPos(out Vector2 screenPos)
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetMouseButtonDown(0))
        {
            screenPos = Input.mousePosition;
            return true;
        }
#endif
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                screenPos = t.position;
                return true;
            }
        }

        screenPos = default;
        return false;
    }
}
