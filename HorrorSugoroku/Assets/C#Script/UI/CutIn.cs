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
        if (!isExecutingFade && !curseSlider.isCardCanvas1 && !curseSlider.isCardCanvas2)
        {
            if (gameManager.isPlayerTurn != previousTurnState)
            {
                isExecutingFade = true;
                Text_CutIn.SetActive(true);
                StartCoroutine(FadeIn());
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

    // フェードイン処理
    public IEnumerator FadeIn()
    {
        Text_CutIn.SetActive(true);
        TEXT.text = gameManager.isPlayerTurn ? "DICE TURN" : "SEARCH TURN";

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

        // フェードアウト後に次のフェードインが必要なら実行
        if (needsNextFadeIn)
        {
            needsNextFadeIn = false;
            isExecutingFade = true;
            Text_CutIn.SetActive(true);
            StartCoroutine(FadeIn());
        }

        Debug.Log("FadeOut 完了: isExecutingFade を false にリセット");
    }
}
