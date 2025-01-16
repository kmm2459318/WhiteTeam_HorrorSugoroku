using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    public Color startColor = Color.white;  // �����F
    public Color endColor = Color.red;     // �_�Ŏ��̐F
    public float blinkSpeed = 1.0f;        // �_�ő��x

    private Material material;            // �}�e���A����ێ�����ϐ�
    private float time;                   // �������Ԍv���p

    void Start()
    {
        // Renderer����}�e���A�����擾
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer��������܂���I");
        }
    }

    void Update()
    {
        if (material != null)
        {
            // ���ԂɊ�Â��ĐF����
            time += Time.deltaTime * blinkSpeed;
            float t = Mathf.Abs(Mathf.Sin(time)); // 0�`1�͈̔͂ŕω�
            material.color = Color.Lerp(startColor, endColor, t);
        }
    }
}
