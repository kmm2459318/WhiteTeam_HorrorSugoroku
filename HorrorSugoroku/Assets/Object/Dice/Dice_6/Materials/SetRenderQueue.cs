using UnityEngine;

public class SetRenderingLayer : MonoBehaviour
{
    public int renderingLayer = 1; // 任意のRendering Layerに設定

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
