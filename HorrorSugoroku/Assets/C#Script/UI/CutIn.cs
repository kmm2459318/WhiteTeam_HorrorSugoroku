using TMPro;
using UnityEngine;
using System.Collections;

public class CutIn : MonoBehaviour
{
    public GameManager gameManager;
    public CurseSlider curseSlider;

    [SerializeField] private TextMeshProUGUI TEXT;
    [SerializeField, Header("フェイドイン")]
    public float FadeInTime = 1.0f;  // フェードイン時間
    [SerializeField, Header("フェイドアウト")]
    public float FadeOutTime = 1.0f; // フェードアウト時間
    [SerializeField, Header("表示時間")]
    public float DisplayTime = 1.5f; // 完全に表示する時間

    public GameObject Text_CutIn;
    private bool previousTurnState = false; // 前回のターン状態
    private bool isExecutingFade = false;  // フェード処理中フラグ
    private bool isTurnChangedDuringFade = false; // フェード中にターンが変わったか
    private bool needsNextFadeIn = false;  // 次のフェードインが必要か

    void Start()
    {
        Text_CutIn.SetActive(false);
        TEXT.text = "";
        TEXT.color = new Color(1.0f, 1.0f, 1.0f, 0.0f); // 完全に透明
        previousTurnState = gameManager.isPlayerTurn; // 初期状態を保存
    }

    void Update()
    {
        // カードが表示中なら待機
        if (!isExecutingFade && !curseSlider.isCardCanvas1 && !curseSlider.isCardCanvas2)
        {
            if (gameManager.isPlayerTurn != previousTurnState)
            {
                isExecutingFade = true;
                StartCoroutine(ShowCutIn());
            }
        }
        else if (isExecutingFade && gameManager.isPlayerTurn != previousTurnState)
        {
            // フェード中にターンが変わったら即座にフェードアウトし、新しいターンの表示を予約
            isTurnChangedDuringFade = true;
            needsNextFadeIn = true;
        }

        previousTurnState = gameManager.isPlayerTurn;
    }

    // カードキャンバスが閉じるまで待機してからフェードインを開始
    public IEnumerator ShowCutIn()
    {
        Debug.Log("ShowCutIn() 開始: カードキャンバスの状態を確認");

        // カードキャンバスが非表示になるのを待機
        yield return StartCoroutine(WaitForCardCanvasToClose());

        // Text_CutInがアクティブであることを確認
        Text_CutIn.SetActive(true);

        Debug.Log("カードキャンバスが閉じたのでText_CutInを表示");

        // プレイヤーターンに応じて表示するテキストを設定
        TEXT.text = gameManager.isPlayerTurn ? "DICE TURN" : "SEARCH TURN";

        StartCoroutine(FadeIn());
    }

    // フェードイン処理
    public IEnumerator FadeIn()
    {
        Debug.Log("FadeIn 開始: 表示する文字 = " + TEXT.text);

        float elapsedTime = 0f;
        while (elapsedTime < FadeInTime)
        {
            if (isTurnChangedDuringFade)
            {
                isTurnChangedDuringFade = false;
                StartCoroutine(FadeOut());
                yield break;
            }

            float alpha = elapsedTime / FadeInTime; // 0 → 1 に変化
            TEXT.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TEXT.color = new Color(1, 1, 1, 1); // 完全に表示

        float displayTime = 0f;
        while (displayTime < DisplayTime)
        {
            if (isTurnChangedDuringFade)
            {
                isTurnChangedDuringFade = false;
                StartCoroutine(FadeOut());
                yield break;
            }
            displayTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadeOut());
    }

    // フェードアウト処理
    IEnumerator FadeOut()
    {
        Debug.Log("FadeOut 開始");

        float elapsedTime = 0f;

        while (elapsedTime < FadeOutTime)
        {
            float alpha = 1 - (elapsedTime / FadeOutTime); // 1 → 0 に変化
            TEXT.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TEXT.color = new Color(1, 1, 1, 0); // 完全に透明
        TEXT.text = "";
        Text_CutIn.SetActive(false);

        isExecutingFade = false;

        Debug.Log("FadeOut 完了");

        // フェードアウト後に次のフェードインが必要なら実行
        if (needsNextFadeIn)
        {
            needsNextFadeIn = false;
            isExecutingFade = true;
            StartCoroutine(ShowCutIn());
        }
    }

    // CardCanvas1 または CardCanvas2 が閉じるまで待機
    private IEnumerator WaitForCardCanvasToClose()
    {
        Debug.Log("WaitForCardCanvasToClose() 開始");

        while (curseSlider.isCardCanvas1 || curseSlider.isCardCanvas2)
        {
            yield return null; // 1フレーム待機
        }

        Debug.Log("WaitForCardCanvasToClose() 完了: カードキャンバスが閉じた");
    }
}
