using UnityEngine;

[ExecuteAlways]
public class PixelPerfectCinemachine : MonoBehaviour
{
    public float pixelsPerUnit = 100f;
    public float unitsPerPixel => 1f / pixelsPerUnit;

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / unitsPerPixel) * unitsPerPixel;
        pos.y = Mathf.Round(pos.y / unitsPerPixel) * unitsPerPixel;
        transform.position = pos;
    }
}

