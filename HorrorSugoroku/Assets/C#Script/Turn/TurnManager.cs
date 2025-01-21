using TMPro; // TextMeshProを使う場合
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro用のターン数表示
    private int currentTurn = 1; // 現在のターン番号

    public FlashlightController flashlightController; // 懐中電灯コントローラーを参照

    // 次のターンに進む処理
    public void NextTurn()
    {
        currentTurn++; // ターンを進める
        UpdateTurnText(); // UIのテキストを更新

        // 懐中電灯のターン進行処理を呼び出し
        if (flashlightController != null)
        {
            flashlightController.OnTurnAdvanced();
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
