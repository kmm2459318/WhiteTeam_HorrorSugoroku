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
            // 通常時はPerlinノイズで滑らかに変化
            blinkLight.intensity = Mathf.PerlinNoise(Time.time * blinkSpeed, 0f) * maxIntensity;
        }
        else
        {
            // 消えかけるときは激しく点滅（ランダム値）
            blinkLight.intensity = Random.Range(0f, maxIntensity / 2f);
        }
    }
}
