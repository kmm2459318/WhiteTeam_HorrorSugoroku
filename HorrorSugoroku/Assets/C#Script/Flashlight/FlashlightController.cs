using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Header("懐中電灯設定")]
    public Light flashlight; // 懐中電灯のライト
    public float maxBattery = 100f; // 最大電池残量
    public float batteryDrainRate = 1f; // 電池が減る速度（毎秒）

    [Header("UI設定")]
    public Slider batterySlider; // 残量ゲージ用のスライダー

    private float currentBattery;

    void Start()
    {
        currentBattery = maxBattery; // 初期化
        UpdateBatteryUI();
    }

    void Update()
    {
        if (currentBattery > 0)
        {
            // 電池を消費する
            currentBattery -= batteryDrainRate * Time.deltaTime;
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

            // 残量がなくなったらライトを消す
            if (currentBattery <= 0)
            {
                flashlight.enabled = false;
            }

            UpdateBatteryUI();
        }
    }

    public void AddBattery(float amount)
    {
        // 電池を拾ったときの処理
        currentBattery += amount;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

        // 電池が復活した場合、ライトを再度点ける
        if (currentBattery > 0)
        {
            flashlight.enabled = true;
        }

        UpdateBatteryUI();
    }

    private void UpdateBatteryUI()
    {
        if (batterySlider != null)
        {
            batterySlider.value = currentBattery / maxBattery;
        }
    }
}
