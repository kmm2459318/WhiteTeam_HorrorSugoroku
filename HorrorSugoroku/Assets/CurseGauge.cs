using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        DashGage.maxValue = maxDashPoint;
        DashGage.value = dashPoint; // スライダーの初期値を0に設定

        // CardCanvasは非表示にしない（開始時に表示されるように）
        // もし初期状態で非表示にしたい場合は、インスペクターで設定してください。

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
    }

    // ターン経過時にゲージを増やす
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
    // 正解：public 修飾子を付ける
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

        // 例えば、シーン遷移する場合
        // UnityEngine.SceneManagement.SceneManager.LoadScene("NextSceneName");
    }
}
