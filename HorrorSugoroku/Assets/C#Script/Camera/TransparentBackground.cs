using UnityEngine;

public class TransparentBackground : MonoBehaviour
{
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0, 0, 0, 0);
        }
    }
}
