using UnityEngine;

public class BreakerController : MonoBehaviour
{
    public bool breaker = false;
    private Light[] taggedLights;

    void Start()
    {
        // "Light"タグが付いた全ライトを取得
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("Light");

        taggedLights = new Light[lightObjects.Length];
        for (int i = 0; i < lightObjects.Length; i++)
        {
            taggedLights[i] = lightObjects[i].GetComponent<Light>();
        }

        BreakerOff(); // 初期状態はオフ
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BreakerHantei();
        }
    }

    public void BreakerHantei()
    {
            BreakerOn();
    }

    void BreakerOn()
    {
        breaker = true;
        foreach (Light light in taggedLights)
        {
            if (light != null)
                light.enabled = true;
        }
    }

    void BreakerOff()
    {
        breaker = false;
        foreach (Light light in taggedLights)
        {
            if (light != null)
                light.enabled = false;
        }
    }
}
