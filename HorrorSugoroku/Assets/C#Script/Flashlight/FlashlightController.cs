using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Header("懐中電灯設定")]
    public Light flashlight; // 懐中電灯のライト
    private float maxBattery = 100f; // 最大電池残量
    private float batteryDrainRate = 10f; // 電池が減る速度（毎秒）

    [Header("UI設定")]
    public Slider batterySlider; // 残量ゲージ用のスライダー
    public Button batteryButton; // 電池回復用のボタン   // 仮

    private float currentBattery;

    void Start()
    {
        currentBattery = maxBattery; // 初期化
        UpdateBatteryUI();

        if (batteryButton != null)
        {
            // ボタンにクリックイベントを登録
            batteryButton.onClick.AddListener(() => AddBattery(10f));   // バッテリーの回復量 仮に10に設定
        }
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
