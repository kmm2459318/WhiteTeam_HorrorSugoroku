using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using SmoothigTransform;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro用のターン数表示
    public TMP_Text turnIndicatorText; // 新しいターン表示用のテキスト
    public bool isPlayerTurn = true; // プレイヤーのターンかどうかを示すフラグ
    public bool EnemyCopyOn = false;
    public int enemyTurnFinCount = 0;
    public int mapPiece = 0;

    public PlayerSaikoro playerSaikoro;
    public EnemySaikoro enemySaikoro;
    public EnemySaikoro enemyCopySaikoro;

    public GameObject currentEnemyModel; // 現在のエネミーモデル
    public GameObject EnemyCopy; // コピーエネミーモデル
    public GameObject newEnemyModelPrefab; // 新しいエネミーモデルのプレハブ

    private int playerTurnCount = 0; // プレイヤーのターン数をカウントする変数

    private void Start()
    {
        UpdateTurnText(); // 初期ターン表示
        playerSaikoro.StartRolling(); // プレイヤーのターンを開始
    }

    private void Update()
    {
        if (mapPiece >= 10)
        {
            Debug.Log("クリアすれ。");
            SceneManager.LoadScene("Gameclear");
        }

        // 地図のかけら仮
        if (Input.GetKeyDown(KeyCode.R))
        {
            MpPlus();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            EnemyCopy.SetActive(true);
            EnemyCopyOn = true;
        }

        if (enemyTurnFinCount == 2)
        {
            enemyTurnFinCount = 0;
            NextTurn();
        }

        // マップのピースが3枚手に入ったらエネミーモデルを変更
        if (mapPiece == 3)
        {
            ChangeEnemyModel();
        }
    }

    public void ChangeEnemyModel()
    {
        currentEnemyModel.SetActive(false);
        newEnemyModelPrefab.SetActive(true);
    }

    // ターンの切り替えを行うメソッド
    public void NextTurn()
    {
        isPlayerTurn = !isPlayerTurn; // ターンを切り替える
        UpdateTurnText(); // UIのテキストを更新

        if (isPlayerTurn)
        {
            // サイコロを振る
            playerSaikoro.DiceRoll();

            playerTurnCount++; // プレイヤーのターン数をカウント
            Debug.Log("Player Turn Count: " + playerTurnCount); // デバッグログ

            playerSaikoro.StartRolling();
        }
        else
        {
            // 新しいエネミーにアクセス先を変更
            if (enemySaikoro != null)
            {
                Debug.Log("Starting enemy turn for new enemy.");
                StartCoroutine(enemySaikoro.EnemyTurn());
            }

            if (EnemyCopyOn && enemyCopySaikoro != null)
            {
                StartCoroutine(enemyCopySaikoro.EnemyTurn());
            }
        }
    }
    public void MpPlus()
    {
        mapPiece++;
        Debug.Log(mapPiece);
    }

    private void UpdateTurnText()
    {
        if (turnIndicatorText != null)
        {
            turnIndicatorText.text = isPlayerTurn ? "PlayerTurn" : "EnemyTurn"; // ターン表示を更新
        }
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}