using UnityEngine;

public class DiceCameraController : MonoBehaviour
{
    public Camera diceCamera; // サイコロ用のカメラ
    public RenderTexture diceRenderTexture; // 作成したRender Texture

    void Start()
    {
        if (diceCamera != null && diceRenderTexture != null)
        {
            diceCamera.targetTexture = diceRenderTexture;
        }
    }
}
