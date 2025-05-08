using UnityEngine;

public class SetRenderingLayer : MonoBehaviour
{
    public int renderingLayer = 1; // ”CˆÓ‚ÌRendering Layer‚Éİ’è

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
