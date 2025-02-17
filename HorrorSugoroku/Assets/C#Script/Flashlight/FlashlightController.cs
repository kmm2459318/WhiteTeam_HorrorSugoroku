using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Header("懐中電灯設定")]
    public Light flashlight; // 懐中電灯のライト
    private float maxBattery = 100f; // 最大電池残量
    private float batteryDrainPerTurn = 20f; // 1ターンあたりの消費量
    private float maxRange = 22f; // ライトの最大Range
    private float minRange = 0f;  // ライトの最小Range

    [Header("UI設定")]
    public Image batteryImage; // 残量ゲージ用のImage
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

    // ターンが進んだときに呼び出される
    public void OnTurnAdvanced()
    {
        // 電池を消費する
        currentBattery -= batteryDrainPerTurn;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

        // 残量がなくなったらライトを消す
        if (currentBattery <= 0)
        {
            flashlight.enabled = false;
        }
        else
        {
            // 電池残量に応じてライトのRangeを更新
            UpdateFlashlightRange();
        }

        UpdateBatteryUI();
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
            UpdateFlashlightRange();
        }

        UpdateBatteryUI();
    }

    private void UpdateBatteryUI()
    {
        if (batteryImage != null)
        {
            // 塗りつぶし割合を設定
            batteryImage.fillAmount = currentBattery / maxBattery;
        }
    }

    private void UpdateFlashlightRange()
    {
        // 電池残量の割合を計算してRangeを設定
        float range = Mathf.Lerp(minRange, maxRange, currentBattery / maxBattery);
        flashlight.range = range;
    }
}
