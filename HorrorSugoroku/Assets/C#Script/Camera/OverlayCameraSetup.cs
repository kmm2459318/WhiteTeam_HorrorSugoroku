using UnityEngine;
using UnityEngine.Rendering.Universal; // 追加

public class OverlayCameraSetup : MonoBehaviour
{
    public Camera subCamera; // Overlay用のカメラ

    void Start()
    {
        if (subCamera != null)
        {
            // UniversalAdditionalCameraData を取得し、Render Type を Overlay に設定
            var cameraData = subCamera.GetComponent<UniversalAdditionalCameraData>();
            if (cameraData != null)
            {
                cameraData.renderType = CameraRenderType.Overlay;
            }

            subCamera.rect = new Rect(0f, 0f, 0.3f, 0.3f);

            // メインカメラを取得し、Overlayカメラをスタックに追加
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                var mainCameraData = mainCamera.GetComponent<UniversalAdditionalCameraData>();
                if (mainCameraData != null)
                {
                    mainCameraData.cameraStack.Add(subCamera);
                }
            }
        }
    }
}
