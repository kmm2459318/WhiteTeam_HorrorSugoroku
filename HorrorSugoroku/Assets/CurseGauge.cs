using UnityEngine;
using UnityEngine.UI;

public class CurseGauge : MonoBehaviour
{
    public Slider curseSlider;  // スライダー（呪いゲージ）
    public float maxCurse = 300f;  // 呪いゲージの最大値
    public float curseIncrement = 5f;  // 一ターンごとの増加値
    private float currentCurse = 0f;  // 現在の呪いゲージ
    private float turnsPassed = 0f;  // 経過ターン数

    void Update()
    {
        // ゲームのターンが経過するごとに呪いゲージを増加させる処理
        turnsPassed += Time.deltaTime; // ターン数を時間経過でカウント（1秒ごと）

        // 1秒が経過したらターンとしてカウントし、ゲージを増加
        if (turnsPassed >= 1f)  // 1ターン（1秒）
        {
            turnsPassed = 0f;  // ターンのカウントリセット
            IncreaseCurse();  // 呪いゲージを増加
        }

        // スライダーの値を更新
        curseSlider.value = currentCurse;
    }

    void IncreaseCurse()
    {
        // 呪いゲージを増加（最大値を超えないように制限）
        currentCurse += curseIncrement;
        if (currentCurse > maxCurse)
        {
            currentCurse = maxCurse;
            // 最大値に達したときの処理（プレイヤーに悪影響を与えるなど）
        }
    }
}
