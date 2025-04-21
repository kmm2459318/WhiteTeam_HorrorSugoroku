using UnityEngine;

public class AlwaysBrightLight : MonoBehaviour
{
    [SerializeField] private Light[] alwaysBrightLights; // 常に明るくするライト
    [SerializeField] private float maxIntensity = 2f; // 最大の明るさ

    private void Start()
    {
        // 指定したライトを常に明るく設定
        foreach (Light light in alwaysBrightLights)
        {
            if (light != null)
            {
                light.enabled = true; // ライトをオンにする
                light.intensity = maxIntensity; // 明るさを最大にする
                Debug.Log($"ライト '{light.name}' を常に明るい状態に設定しました");
            }
        }
    }
}