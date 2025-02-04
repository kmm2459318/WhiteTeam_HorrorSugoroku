using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurseSlider : MonoBehaviour
{
    [SerializeField] Slider DashGage;
    [SerializeField] SceneChanger3D sceneChanger; // SceneChanger3D への参照を追加
    [SerializeField] GameObject CardCanvas; // CardCanvasを参照
    [SerializeField] Button showButton; // ボタン1: CardCanvasを表示
    [SerializeField] Button hideButton; // ボタン2: CardCanvasを非表示
    [SerializeField] Button extraButton; // ボタン3: 追加のアクション用ボタン

    public float maxDashPoint = 300; // 最大値
    public float dashIncreasePerTurn = 5; // 1ターンごとの増加量

    float dashPoint = 0; // 初期値を0に設定
    float currentVelocity = 0;

    public GameManager gameManager;
    public TurnManager turnManager;
    private bool saikorotyu;

    void Start()
    {
        DashGage.maxValue = maxDashPoint;
        DashGage.value = dashPoint; // スライダーの初期値を0に設定

        // 各ボタンのクリックイベントにメソッドを登録
        if (showButton != null)
        {
            showButton.onClick.AddListener(ShowCardCanvas);
        }
        if (hideButton != null)
        {
            hideButton.onClick.AddListener(HideCardCanvas);
        }
        if (extraButton != null)
        {
            extraButton.onClick.AddListener(ExtraButtonAction);
        }

        // 初期状態でカードは非表示にしておく
        HideCardCanvas();
    }

    void Update()
    {
        DashGage.value = dashPoint; // ゲージの値を更新

        // ゲージがマックスになった場合にゲームオーバー処理を呼び出す
        if (dashPoint >= maxDashPoint)
        {
            if (sceneChanger != null)
            {
                sceneChanger.HandleGameOver();
            }
        }

        // プレイヤーターン終了のタイミングでカードを表示させる処理
        if (gameManager.isPlayerTurn && !saikorotyu) // プレイヤーターン中でサイコロが振られていない場合
        {
            // 例えばキー入力でターン終了をトリガー
            if (Input.GetKeyDown(KeyCode.E)) // Eキーで次のターン
            {
                EndTurnWithCardDisplay(); // カード表示後、次のターンへ進む
            }
        }
    }

    // ターン終了前にカードを表示させる
    public void EndTurnWithCardDisplay()
    {
        // ターン終了処理を遅延させる
        StartCoroutine(ShowCardCanvasBeforeEndTurn(1f)); // 1秒の遅延を加えてカードを表示
    }

    // カードを表示するCoroutine
    private IEnumerator ShowCardCanvasBeforeEndTurn(float delayTime)
    {
        // カードを表示
        ShowCardCanvas();

        // 指定した遅延時間だけ待機（カード表示後）
        yield return new WaitForSeconds(delayTime); // ここで遅延を追加

        // その後、ターン終了処理を呼び出す
        turnManager.NextTurn(); // ターンを進める処理（ターン管理を進める）
        gameManager.NextTurn(); // ゲーム進行管理を進める
    }

    // ゲージの値を増加させる
    public void IncreaseDashPointPerTurn()
    {
        dashPoint = Mathf.Min(dashPoint + dashIncreasePerTurn, maxDashPoint);
        DashGage.value = dashPoint; // 変更後すぐに適用

        // ゲージが20の倍数に達した場合にCardCanvasを表示
        if ((int)(dashPoint / 20) > (int)((dashPoint - dashIncreasePerTurn) / 20))
        {
            ShowCardCanvas();
        }

        Debug.Log($"[CurseSlider] Dash Point Increased: {dashPoint}/{maxDashPoint}");
    }

    // CardCanvasを表示する
    public void ShowCardCanvas()
    {
        if (CardCanvas != null)
        {
            CardCanvas.SetActive(true);
        }
    }

    // CardCanvasを非表示にする
    public void HideCardCanvas()
    {
        if (CardCanvas != null)
        {
            CardCanvas.SetActive(false);
        }
    }

    // 追加のボタンアクション（例: シーン遷移など）
    public void ExtraButtonAction()
    {
        // ここで追加の処理を行う
        Debug.Log("Extra Button Clicked!");
    }
}
