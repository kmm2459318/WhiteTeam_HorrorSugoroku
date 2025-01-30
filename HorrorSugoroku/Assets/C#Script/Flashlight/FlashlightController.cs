using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("懐中電灯設定")]
    public Light flashlight; // 懐中電灯のライト

    void Start()
    {
        flashlight.enabled = true; // ライトは常にON
    }
}
