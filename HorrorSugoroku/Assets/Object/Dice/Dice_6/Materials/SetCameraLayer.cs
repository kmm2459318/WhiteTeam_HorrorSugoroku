using UnityEngine;

public class SetCameraLayer : MonoBehaviour
{
    public Camera targetCamera; // 3D�I�u�W�F�N�g��\������J����
    public int renderingLayer = 1; // �C�ӂ�Rendering Layer�ɐݒ�

    void Start()
    {
        if (targetCamera != null)
        {
            // �J������Culling Mask��ݒ�
            targetCamera.cullingMask = 1 << renderingLayer;
            Debug.Log("Camera Culling Mask set to layer: " + renderingLayer);
        }
        else
        {
            Debug.LogError("Target camera not assigned!");
        }
    }
}
