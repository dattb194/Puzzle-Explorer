using UnityEngine;

[ExecuteAlways]
public class CameraFit : MonoBehaviour
{
    public float targetAspect; 
    public float orthographicSize;

    void Update()
    {
        float currentAspect = (float)Screen.width / (float)Screen.height;

        float scale = targetAspect / currentAspect;

        Camera.main.orthographicSize = orthographicSize * scale;
    }
}
