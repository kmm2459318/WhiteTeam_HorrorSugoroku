using UnityEngine;
using UnityEngine.UI;

public class CurseSlider : MonoBehaviour
{
    [SerializeField] Slider DashGage;
    [SerializeField] SceneChanger3D sceneChanger;
    [SerializeField] GameObject CardCanvas;
    [SerializeField] Button showButton;
    [SerializeField] Button hideButton;
    [SerializeField] Button extraButton;

    public float maxDashPoint = 300;
    public float dashIncreasePerTurn = 5;

    float dashPoint = 0;

    void Start()
    {
        DashGage.maxValue = maxDashPoint;
        DashGage.value = dashPoint;

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
        DashGage.value = dashPoint;

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
        DashGage.value = dashPoint;

        Debug.Log($"[CurseSlider] Dash Point Increased: {dashPoint}/{maxDashPoint}");

        // ※ ShowCardCanvas() の呼び出しは削除
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

    // 追加のボタンアクション
    public void ExtraButtonAction()
    {
        Debug.Log("Extra Button Clicked!");
    }
}
