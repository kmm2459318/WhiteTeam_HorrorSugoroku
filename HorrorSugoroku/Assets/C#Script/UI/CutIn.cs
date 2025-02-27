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
    private bool isExecutingFade = false;  // `FadeIn()` を1回だけ呼び出すためのフラグ


    void Start()
    {
        Text_CutIn.SetActive(false);
        TEXT.text = "";
        TEXT.color = new Color(1.0f, 1.0f, 1.0f, 0.0f); // 完全に透明
        previousTurnState = gameManager.isPlayerTurn; // 初期状態を保存
    }

    void Update()
    {
        if (!isExecutingFade && curseSlider.isCardCanvas1 == false && curseSlider.isCardCanvas2 == false)
        {
            //DICE TURN表示
            if (gameManager.isPlayerTurn && !previousTurnState)
            {
                isExecutingFade = true; // フェード処理中にする
                Text_CutIn.SetActive(true);
                StartCoroutine(FadeIn());
            }
            //SEARCH TURN表示
            else if (!gameManager.isPlayerTurn && previousTurnState)
            {
                isExecutingFade = true; // フェード処理中にする
                Text_CutIn.SetActive(true);
                StartCoroutine(FadeIn());
            }
        }

        //現在の状態を更新
        previousTurnState = gameManager.isPlayerTurn;
    }

    // フェイドインの処理
    public IEnumerator FadeIn()
    { 
        Text_CutIn.SetActive(true);
        TEXT.text = gameManager.isPlayerTurn ? "DICE TURN" : "SEARCH TURN";

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
        Text_CutIn.SetActive(false);

        isExecutingFade = false;
        Debug.Log("FadeOut 完了: isExecutingFade を false にリセット");
    }
}