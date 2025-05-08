using UnityEngine;

public class SetCameraLayer : MonoBehaviour
{
    public Camera targetCamera; // 3Dオブジェクトを表示するカメラ
    public int renderingLayer = 1; // 任意のRendering Layerに設定

    void Start()
    {
        if (targetCamera != null)
        {
            // カメラのCulling Maskを設定
            targetCamera.cullingMask = 1 << renderingLayer;
            Debug.Log("Camera Culling Mask set to layer: " + renderingLayer);
        }
        else
        {
            Debug.LogError("Target camera not assigned!");
        }
    }
}
