using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro用のターン数表示
    public TMP_Text turnIndicatorText; // 新しいターン表示用のテキスト
    private bool isPlayerTurn = true; // プレイヤーのターンかどうかを示すフラグ

    public PlayerSaikoro playerSaikoro;
    public EnemySaikoro enemySaikoro;

    private void Start()
    {
        UpdateTurnText(); // 初期ターン表示
        playerSaikoro.StartRolling(); // プレイヤーのターンを開始
    }

    public void NextTurn()
    {
        isPlayerTurn = !isPlayerTurn; // ターンを切り替える
        UpdateTurnText(); // UIのテキストを更新

        if (isPlayerTurn)
        {
            playerSaikoro.StartRolling();
        }
        else
        {
            StartCoroutine(enemySaikoro.EnemyTurn());
        }
    }

    private void UpdateTurnText()
    {

        if (turnIndicatorText != null)
        {
            if (isPlayerTurn)
            {
                turnIndicatorText.text = "PlayerTurn"; // プレイヤーのターン表示
            }
            else
            {
                turnIndicatorText.text = "EnemyTurn"; // エネミーのターン表示
            }
        }
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}