using TMPro; // TextMeshProを使う場合
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro用のターン数表示
    private int currentTurn = 1; // 現在のターン番号
    public PlayerSaikoro PlayerSaikoro;

    // 次のターンに進む処理
    public void NextTurn()
    {
        if (!PlayerSaikoro.idoutyu && !PlayerSaikoro.saikorotyu)
        {
            currentTurn++; // ターンを進める
            UpdateTurnText(); // UIのテキストを更新
            PlayerSaikoro.DiceRoll();
        }
    }

    // ターン表示を更新するメソッド
    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = "" + currentTurn; // テキストにターン数を表示
        }
    }

    // ゲーム開始時に初期化
    private void Start()
    {
        UpdateTurnText(); // 初期ターン表示
    }
}