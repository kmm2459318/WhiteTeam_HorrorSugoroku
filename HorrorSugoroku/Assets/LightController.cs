using UnityEngine;

public class LightBlinker : MonoBehaviour
{
    [SerializeField] float maxIntensity = 1.0f;
    [SerializeField] float blinkSpeed = 1.0f;

    Light blinkLight;
    int flashAdjustValue = 7;

    void Start()
    {
        blinkLight = this.gameObject.GetComponent<Light>();
    }

    void Update()
    {
        if (blinkLight.intensity > maxIntensity / flashAdjustValue)
        {
            // �ʏ펞��Perlin�m�C�Y�Ŋ��炩�ɕω�
            blinkLight.intensity = Mathf.PerlinNoise(Time.time * blinkSpeed, 0f) * maxIntensity;
        }
        else
        {
            // ����������Ƃ��͌������_�Łi�����_���l�j
            blinkLight.intensity = Random.Range(0f, maxIntensity / 2f);
        }
    }
}
