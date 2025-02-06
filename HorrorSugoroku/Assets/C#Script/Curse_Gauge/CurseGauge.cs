using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CurseSlider : MonoBehaviour
{
    [SerializeField] Slider DashGage;
    [SerializeField] SceneChanger3D sceneChanger;
    [SerializeField] GameObject CardCanvas;
    [SerializeField] Button extraButton;
    [SerializeField] Button hideButton;
    [SerializeField] Button cursegiveButton;

    [SerializeField] private Master_Curse master_Curse; // Master_Curse への参照

    public float maxDashPoint = 300;
    public float dashIncreasePerTurn = 5; // 初期増加量

    int Count = 1; // 何回呪いカードを選んだか
    float dashPoint = 0;

    public GameManager gameManager;
    public TurnManager turnManager;
    private bool saikorotyu;

    void Start()
    {
        DashGage.maxValue = maxDashPoint;
        DashGage.value = dashPoint;

        // ボタンのリスナーを一度クリアして、重複登録を防ぐ
        if (extraButton != null)
        {
            extraButton.onClick.RemoveAllListeners();
            extraButton.onClick.AddListener(() => { ExtraButtonAction(); HideCardCanvas(); });
        }
        if (hideButton != null)
        {
            hideButton.onClick.RemoveAllListeners();
            hideButton.onClick.AddListener(() => { HideCardCanvasAndModifyDashIncrease(); HideCardCanvas(); });
        }
        if (cursegiveButton != null)
        {
            cursegiveButton.onClick.RemoveAllListeners();
            cursegiveButton.onClick.AddListener(() => { CursegiveButtonAction(); HideCardCanvas(); });
        }

        HideCardCanvas();
    }

    void Update()
    {
        // ゲージ更新
        DashGage.value = dashPoint;

        // ゲージが最大値に達した場合にゲームオーバー処理
        if (dashPoint >= maxDashPoint)
        {
            if (sceneChanger != null)
            {
                sceneChanger.HandleGameOver();
            }
        }

        // プレイヤーターン中で、サイコロが振られていない場合に次のターンへ
        if (gameManager.isPlayerTurn && !saikorotyu)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EndTurnWithCardDisplay();
            }
        }
    }

    private void EndTurnWithCardDisplay()
    {
        // すでにターンが終了している場合は何もしない
        if (!gameManager.isPlayerTurn) return;
    }

    public void IncreaseDashPointPerTurn()
    {
        dashPoint = Mathf.Min(dashPoint + dashIncreasePerTurn, maxDashPoint);
        DashGage.value = dashPoint;

        Debug.Log("今の呪いゲージ量: " + dashPoint);

        // 20の倍数に達した場合にCardCanvasを表示
        if ((int)(dashPoint) >= Count * 20)
        {
            ShowCardCanvas();
            Count++;
        }
    }

    private void ShowCardCanvas()
    {
        if (CardCanvas != null)
        {
            CardCanvas.SetActive(true);
        }
    }

    public void ExtraButtonAction()
    {
        Debug.Log("Extra Button Clicked!");
    }

    public void HideCardCanvas()
    {
        if (CardCanvas != null)
        {
            CardCanvas.SetActive(false);
        }
    }

    public void HideCardCanvasAndModifyDashIncrease()
    {
        dashIncreasePerTurn += master_Curse.CurseSheet[1].TurnIncrease;
        Debug.Log("[CurseSlider] TurnIncrease: " + master_Curse.CurseSheet[1].TurnIncrease);
        Debug.Log("[CurseSlider] Dash Increase Per Turn set to: " + dashIncreasePerTurn);
    }

    public void CursegiveButtonAction()
    {
        Debug.Log("[CursegiveButton] Before: DashPoint = " + dashPoint);

        dashPoint = Mathf.Min(dashPoint + 15, maxDashPoint);
        DashGage.value = dashPoint;

        Debug.Log("[CursegiveButton] After: DashPoint = " + dashPoint);
    }
}