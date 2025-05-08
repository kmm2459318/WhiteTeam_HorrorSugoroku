using UnityEngine;

public class AddOutline : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color outlineColor = Color.black;
    public float outlineSize = 1.0f;

    void Start()
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetColor("_OutlineColor", outlineColor);
        mpb.SetFloat("_OutlineSize", outlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}
