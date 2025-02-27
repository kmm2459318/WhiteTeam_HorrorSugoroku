using UnityEngine;

public class DiceCameraController : MonoBehaviour
{
    public Camera diceCamera; // �T�C�R���p�̃J����
    public RenderTexture diceRenderTexture; // �쐬����Render Texture

    void Start()
    {
        if (diceCamera != null && diceRenderTexture != null)
        {
            diceCamera.targetTexture = diceRenderTexture;
        }
    }
}
