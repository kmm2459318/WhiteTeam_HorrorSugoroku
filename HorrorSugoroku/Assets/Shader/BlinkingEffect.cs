using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    public Color startColor = Color.white;  // 初期色
    public Color endColor = Color.red;     // 点滅時の色
    public float blinkSpeed = 1.0f;        // 点滅速度

    private Material material;            // マテリアルを保持する変数
    private float time;                   // 内部時間計測用

    void Start()
    {
        // Rendererからマテリアルを取得
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("Rendererが見つかりません！");
        }
    }

    void Update()
    {
        if (material != null)
        {
            // 時間に基づいて色を補間
            time += Time.deltaTime * blinkSpeed;
            float t = Mathf.Abs(Mathf.Sin(time)); // 0～1の範囲で変化
            material.color = Color.Lerp(startColor, endColor, t);
        }
    }
}
