using UnityEngine;
using UnityEngine.UI;

public class ZoomHandler : MonoBehaviour
{
    public RectTransform map;
    public float zoomSpeed = 0.1f;
    public float minZoom = 1f;
    public float maxZoom = 2f;

    private Vector2 originalSize;

    void Start()
    {
        originalSize = map.sizeDelta;
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float zoom = Input.mouseScrollDelta.y * zoomSpeed;
            Vector2 newSize = map.sizeDelta + new Vector2(zoom, zoom);
            newSize = Vector2.Max(newSize, originalSize * minZoom);
            newSize = Vector2.Min(newSize, originalSize * maxZoom);
            map.sizeDelta = newSize;
        }
    }
}