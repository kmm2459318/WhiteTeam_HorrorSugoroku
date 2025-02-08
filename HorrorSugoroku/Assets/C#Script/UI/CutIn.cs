using TMPro;
using UnityEngine;
using System.Collections;

public class CutIn : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] private TextMeshProUGUI TEXT;
    [SerializeField, Header("フェイドイン")]
    public float FadeInTime = 1.0f;  // フェードイン時間
    [SerializeField, Header("フェイドアウト")]
    public float FadeOutTime = 1.0f; // フェードアウト時間
    [SerializeField, Header("表示時間")]
    public float DisplayTime = 1.5f; // 完全に表示する時間

    private bool previousTurnState = false; // 前回のターン状態

    void Start()
    {
        TEXT.text = "";
        TEXT.color = new Color(1.0f, 1.0f, 1.0f, 0.0f); // 完全に透明
        previousTurnState = gameManager.isPlayerTurn; // 初期状態を保存
    }

    void Update()
    {
        // `isPlayerTurn` が true に変わったときに 1 回だけ `NextTurn()` を実行
        if (gameManager.isPlayerTurn && !previousTurnState)
        {
            StartCoroutine(FadeIn());
        }

        // 現在の状態を更新
        previousTurnState = gameManager.isPlayerTurn;
    }

    // フェイドインの処理
    IEnumerator FadeIn()
    {
        TEXT.text = "PLAYER TURN";
        float elapsedTime = 0f;

        while (elapsedTime < FadeInTime)
        {
            float alpha = elapsedTime / FadeInTime; // 0 → 1 に変化
            TEXT.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TEXT.color = new Color(1, 1, 1, 1); // 完全に表示
        yield return new WaitForSeconds(DisplayTime); // 表示時間待機

        StartCoroutine(FadeOut());
    }

    // フェイドアウトの処理
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < FadeOutTime)
        {
            float alpha = 1 - (elapsedTime / FadeOutTime); // 1 → 0 に変化
            TEXT.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TEXT.color = new Color(1, 1, 1, 0); // 完全に透明
        TEXT.text = ""; // フェードアウト後テキストを消す
    }
}
