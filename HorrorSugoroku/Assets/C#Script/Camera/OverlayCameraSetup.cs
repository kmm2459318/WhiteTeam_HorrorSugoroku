using UnityEngine;
using UnityEngine.Rendering.Universal; // �ǉ�

public class OverlayCameraSetup : MonoBehaviour
{
    public Camera subCamera; // Overlay�p�̃J����

    void Start()
    {
        if (subCamera != null)
        {
            // UniversalAdditionalCameraData ���擾���ARender Type �� Overlay �ɐݒ�
            var cameraData = subCamera.GetComponent<UniversalAdditionalCameraData>();
            if (cameraData != null)
            {
                cameraData.renderType = CameraRenderType.Overlay;
            }

            subCamera.rect = new Rect(0f, 0f, 0.3f, 0.3f);

            // ���C���J�������擾���AOverlay�J�������X�^�b�N�ɒǉ�
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
