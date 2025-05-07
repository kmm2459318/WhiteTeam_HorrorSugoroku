using UnityEngine;

public class PerlinNoiseLight : MonoBehaviour
{
    [SerializeField]
    float maxIntensity;

    [SerializeField]
    float blinkSpeed;

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
            blinkLight.intensity = Mathf.PerlinNoise(Time.time * blinkSpeed, 0) * maxIntensity;
        }
        else //����������ƌ������_��
        {
            blinkLight.intensity = Random.Range(0, maxIntensity / 2);
        }

    }
}
