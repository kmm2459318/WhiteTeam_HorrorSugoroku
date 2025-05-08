using UnityEngine;

public class SetRenderingLayer : MonoBehaviour
{
    public int renderingLayer = 1; // �C�ӂ�Rendering Layer�ɐݒ�

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.renderingLayerMask = (uint)(1 << renderingLayer);
            Debug.Log("Rendering Layer set to: " + renderingLayer);
        }
        else
        {
            Debug.LogError("Renderer not found!");
        }
    }
}
