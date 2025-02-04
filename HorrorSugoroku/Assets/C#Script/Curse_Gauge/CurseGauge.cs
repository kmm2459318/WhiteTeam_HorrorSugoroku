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
    [SerializeField] Button showButton;
    [SerializeField] Button hideButton;
    [SerializeField] Button extraButton;

    [SerializeField] private Master_Curse master_Curse; // Master_Curse への参照

    public float maxDashPoint = 300;
    public float dashIncreasePerTurn = 5; // 初期増加量

    float dashPoint = 0;

    public GameManager gameManager;
    public TurnManager turnManager;
    private bool saikorotyu;

    void Start()
    {
        DashGage.maxValue = maxDashPoint;
        DashGage.value = dashPoint;

        // ボタンの設定
        if (showButton != null)
        {
            showButton.onClick.AddListener(ShowCardCanvas);
        }
        if (hideButton != null)
        {
            hideButton.onClick.AddListener(HideCardCanvasAndModifyDashIncrease);
        }
        if (extraButton != null)
        {
            extraButton.onClick.AddListener(ExtraButtonAction);
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

        // 増加量を確認
        //Debug.Log($"Dash Increase Per Turn: {dashIncreasePerTurn}");
    }

    private void EndTurnWithCardDisplay()
    {
        throw new NotImplementedException();
    }

    public void IncreaseDashPointPerTurn()
    {
        // Master_Curse の状態によって増加量を変更
        if (master_Curse != null && master_Curse.isCursed)
        {
            dashIncreasePerTurn = 10;  // 呪い状態なら増加量を10に設定
        }
        else
        {
            dashIncreasePerTurn = 5;   // 通常状態なら増加量を5に設定
        }

        dashPoint = Mathf.Min(dashPoint + dashIncreasePerTurn, maxDashPoint);
        DashGage.value = dashPoint;

        // 20の倍数に達した場合にCardCanvasを表示
        if ((int)(dashPoint / 20) > (int)((dashPoint - dashIncreasePerTurn) / 20))
        {
            ShowCardCanvas();
        }

        Debug.Log($"[CurseSlider] Dash Point Increased: {dashPoint}/{maxDashPoint}, Increase Per Turn: {dashIncreasePerTurn}");
    }

    public void ShowCardCanvas()
    {
        if (CardCanvas != null)
        {
            CardCanvas.SetActive(true);
        }
    }

    public void HideCardCanvas()
    {
        if (CardCanvas != null)
        {
            CardCanvas.SetActive(false);
        }
    }

    // 2番目のボタンが押された時に増加量を変更
    public void HideCardCanvasAndModifyDashIncrease()
    {
        if (CardCanvas != null)
        {
            CardCanvas.SetActive(false);
        }

        // Master_Curse の状態によって増加量を変更
        if (master_Curse != null && master_Curse.isCursed)
        {
            Debug.Log("iii");
            dashIncreasePerTurn = 10;  // 呪い状態なら増加量を10に設定
        }
        else
        {
            Debug.Log("あああ");
            dashIncreasePerTurn = 5;   // 通常状態なら増加量を5に設定
        }

        Debug.Log(master_Curse.CurseSheet[1].TurnIncrease);
        Debug.Log("[CurseSlider] Dash Increase Per Turn set to: " + dashIncreasePerTurn);
    }

    public void ExtraButtonAction()
    {
        Debug.Log("Extra Button Clicked!");
    }
}

