using UnityEngine;
using TMPro;  // TextMeshPro 用

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText;  // TextMeshPro 用のターン数表示
    private int currentTurn = 1;  // 現在のターン番号

    public PlayerSaikoro playerSaikoro;  // プレイヤーのサイコロ管理（次のターンに進む処理）

    // 次のターンに進む処理
    public void NextTurn()
    {
        currentTurn++;  // ターンを進める
        PlayerPrefs.SetInt("Turn", currentTurn);
        UpdateTurnText();  // UIのテキストを更新する
    }


    // ターン表示を更新するメソッド
    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = "Turn: " + currentTurn;  // テキストにターン番号を表示
        }
    }

    // ゲーム開始時に初期化
    private void Start()
    {
        UpdateTurnText();  // 初期ターン表示
    }
}
