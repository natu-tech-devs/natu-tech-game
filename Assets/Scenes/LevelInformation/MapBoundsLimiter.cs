using UnityEngine;
using UnityEngine.UI;

public class MapBoundsLimiter : MonoBehaviour
{
    public RectTransform map;
    public RectTransform viewport;

    void LateUpdate()
    {
        // Calcula os limites do mapa e da viewport
        Vector3[] mapCorners = new Vector3[4];
        map.GetWorldCorners(mapCorners);
        
        Vector3[] viewportCorners = new Vector3[4];
        viewport.GetWorldCorners(viewportCorners);

        Vector3 mapMin = mapCorners[0];
        Vector3 mapMax = mapCorners[2];
        Vector3 viewportMin = viewportCorners[0];
        Vector3 viewportMax = viewportCorners[2];

        // Calcula a posição do mapa em relação à viewport
        Vector3 pos = map.localPosition;
        
        float offsetX = 0;
        float offsetY = 0;

        if (mapMin.x > viewportMin.x)
        {
            offsetX = viewportMin.x - mapMin.x;
        }
        else if (mapMax.x < viewportMax.x)
        {
            offsetX = viewportMax.x - mapMax.x;
        }

        if (mapMin.y > viewportMin.y)
        {
            offsetY = viewportMin.y - mapMin.y;
        }
        else if (mapMax.y < viewportMax.y)
        {
            offsetY = viewportMax.y - mapMax.y;
        }

        // Aplica a correção de posição ao mapa
        map.localPosition += new Vector3(offsetX, offsetY, 0);
    }
}
