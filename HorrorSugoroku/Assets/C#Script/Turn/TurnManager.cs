using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText;
    private int currentTurn = 0;
    public bool turnStay = false;

    public PlayerSaikoro playerSaikoro;
    public CurseSlider curseSlider; // 呪いゲージ管理
    public FlashlightController flashlightController; // フラッシュライト管理

    // 次のターンへ進む処理
    public void NextTurn()
    {
        if (!turnStay)
        {
            turnStay = true;
            currentTurn++;
            PlayerPrefs.SetInt("Turn", currentTurn);
            UpdateTurnText();

            // サイコロを振る
            playerSaikoro.DiceRoll();

            // フラッシュライトのターン進行処理
            if (flashlightController != null)
            {
                flashlightController.OnTurnAdvanced();
            }

           


        }
    }

    public void TurnCurse()
    {
        // 呪いゲージ増加
        if (curseSlider != null)
        {
            curseSlider.IncreaseDashPointPerTurn();
            Debug.Log("[TurnManager] IncreaseDashPointPerTurn() called.");
        }
        else
        {
            Debug.LogError("[TurnManager] CurseSlider is not assigned!");
        }
    }
    // ターン数のUIを更新
    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = "Turn: " + currentTurn;
        }
    }

    // 初期化処理
    private void Start()
    {
        UpdateTurnText();
        PlayerPrefs.SetInt("Turn", 0);
    }
    
}



