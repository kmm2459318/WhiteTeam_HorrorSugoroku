using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // シーン管理用
using System.Collections;

public class JumpScareAnimation : MonoBehaviour
{
    public Animator animator; // アニメーターを設定
    public Button startButton; // アニメーション開始ボタン
    private string triggerName = "StartAttack"; // トリガーの名前
    private float moveDuration = 0.25f; // 移動にかかる時間（デフォルト値）
    private Vector3 targetPosition = new Vector3(0f, 0f, 0f); // 目標の位置
    private Vector3 initialPosition; // 最初の位置
    private float timeReset = 2f;
    public JumpScareAnimation jumpScareAnimation; // JumpScareAnimation クラスのインスタンス

    void Start()
    {
        // 最初の位置を記録
        initialPosition = transform.position;

        // シーン遷移後にアニメーションを開始するためのリスナーを追加
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 例: ゲームオーバー時にアニメーションを開始
        if (jumpScareAnimation != null)
        {
            jumpScareAnimation.StartAnimation(); // StartAnimation メソッドを呼び出し
        }
    }

    // シーンがロードされた後にアニメーションを開始
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // もしジャンプスケアシーンに遷移したら、アニメーションを開始
        if (scene.name == "Jump Scare") // シーン名を確認
        {
            StartCoroutine(DelayStartAnimation()); // アニメーションを遅延して開始
        }
    }

    // シーン遷移後に少し遅れてアニメーションを開始するコルーチン
    private IEnumerator DelayStartAnimation()
    {
        // 少し待機してからアニメーションを開始
        yield return new WaitForSeconds(0.5f); // 少し遅らせてからアニメーションを開始

        // 座標を目標地点にスムーズに移動させる
        StartCoroutine(MoveObject());
    }

    // オブジェクトをスムーズに移動させるコルーチン
    private IEnumerator MoveObject()
    {
        float timeElapsed = 0f;

        // 現在の位置から目標位置に向かって滑らかに移動
        while (timeElapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 最終的な位置を確実に設定
        transform.position = targetPosition;

        // 移動が完了した後にアニメーションを開始
        if (animator != null)
        {
            animator.enabled = true; // アニメーターを有効にする
            animator.SetTrigger(triggerName); // トリガーを発火してアニメーションを再生
        }

        // アニメーション開始後2秒後にResetObject()を呼び出す
        yield return new WaitForSeconds(timeReset);
        ResetObject();
    }

    // 入力された移動時間を更新するメソッド
    private void UpdateMoveDuration(string input)
    {
        float parsedValue;
        if (float.TryParse(input, out parsedValue))
        {
            moveDuration = parsedValue; // 入力された値で移動時間を更新
        }
    }

    private void ResetObject()
    {
        transform.position = initialPosition;
    }

    public void StartAnimation()
    {
        // 座標を目標地点にスムーズに移動させる
        StartCoroutine(MoveObject());
    }
}
