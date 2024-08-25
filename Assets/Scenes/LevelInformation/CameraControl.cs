using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private float zoomOutMin = 1;
    [SerializeField]
    private float zoomOutMax = 12;

    [SerializeField]
    private CanvasRenderer mapRenderer;
    private Vector3 dragOrigin;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.GetComponent<RectTransform>().rect.width / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.GetComponent<RectTransform>().rect.width / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.GetComponent<RectTransform>().rect.height / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.GetComponent<RectTransform>().rect.height / 2f;
        // mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        // mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        // mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void Update()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            dragOrigin = camera.ScreenToWorldPoint((touchZero.position + touchOne.position) / 2);
            camera.transform.position = ClampCamera(camera.transform.position + new Vector3(difference, difference, 0) * 0.075f);

            Zoom(difference * 0.075f);


        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - camera.ScreenToWorldPoint(Input.mousePosition);
            camera.transform.position = ClampCamera(camera.transform.position + difference);
        }
    }

    private void Zoom(float increment)
    {
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = camera.orthographicSize;
        float camWidth = camera.aspect * camHeight;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

}
