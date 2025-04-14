using UnityEngine;

public class DynamicLightRang : MonoBehaviour
{
    [Header("設定")]
    public float changeSpeed = 1f; // 速度を調整する
    public float stepSize = 1f;    // 増減の値を調整する

    private float currentValue;    // 現在の値
    private float targetValue;     // 目標値
    private const float minValue = -126.342f; // 範囲の最小値
    private const float maxValue = -95.996f;  // 範囲の最大値

    void Start()
    {
        ResetValues();
    }

    void Update()
    {
        // 目標値に到達したらリセット
        if (Mathf.Approximately(currentValue, targetValue))
        {
            ResetValues();
        }
        else
        {
            // ±stepSizeずつランダムに変化
            float step = Random.Range(0, 2) == 0 ? stepSize : -stepSize;
            currentValue = Mathf.MoveTowards(currentValue, targetValue, changeSpeed * Time.deltaTime);

            // 回転角度を更新
            transform.rotation = Quaternion.Euler(currentValue, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }

    private void ResetValues()
    {
        currentValue = Random.Range(minValue, maxValue); // 現在の値をランダムに設定
        targetValue = Random.Range(minValue, maxValue);  // 目標値をランダムに設定
    }
}