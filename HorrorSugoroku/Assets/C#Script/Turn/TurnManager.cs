using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText;
    private int currentTurn = 0;
    public bool turnStay = false;

    public PlayerSaikoro playerSaikoro;
    public CurseSlider curseSlider; // 呪いゲージ管理

    // 次のターンへ進む処理
    public void NextTurn()
    {
        if (!turnStay)
        {
            turnStay = true;
            currentTurn++;
            PlayerPrefs.SetInt("Turn", currentTurn);
            UpdateTurnText();

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
        currentTurn = PlayerPrefs.GetInt("Turn", 0);
        UpdateTurnText();

        if (curseSlider == null)
        {
            curseSlider = FindObjectOfType<CurseSlider>(); // シーン内の CurseSlider を自動取得
            if (curseSlider == null)
            {
                Debug.LogError("[TurnManager] CurseSlider is not found in the scene.");
            }
        }
    }

    public void IncreaseDashPointPerTurn()
    {
        // 呪いゲージを増加させるロジックをここに追加
        Debug.Log("CurseSlider: IncreaseDashPointPerTurn called.");
    }
    // **ターンの最後にCardCanvasを表示するメソッド**
    private void ShowCardCanvasAfterTurn()
    {
        if (curseSlider != null)
        {
            curseSlider.ShowCardCanvas1();
            Debug.Log("[TurnManager] CardCanvas is now displayed.");
        }
    }
    public void ShowCardCanvas()
    {
        // カードキャンバスを表示するロジックをここに追加
        Debug.Log("CurseSlider: ShowCardCanvas called.");
    }

}