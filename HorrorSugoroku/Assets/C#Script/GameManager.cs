using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public GameObject newEnemyPrefab; // 新しいエネミーモデルのプレファブ
    public GameObject EnemyCopy;

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

        //地図のかけら仮
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
    }
    public void MpPlus()
    {
        mapPiece++;
        Debug.Log(mapPiece);
    }

    public void NextTurn()
    {
        isPlayerTurn = !isPlayerTurn; // ターンを切り替える
        UpdateTurnText(); // UIのテキストを更新

        if (isPlayerTurn)
        {
            playerTurnCount++; // プレイヤーのターン数をカウント
            Debug.Log("Player Turn Count: " + playerTurnCount); // デバッグログ

            // プレイヤーのターンが5ターン目になったらエネミーモデルを変更
            if (playerTurnCount == 5)
            {
                //ChangeEnemyModel();
            }

            playerSaikoro.StartRolling();
        }
        else
        {
            StartCoroutine(enemySaikoro.EnemyTurn());

            if (EnemyCopyOn)
            {
                StartCoroutine(enemyCopySaikoro.EnemyTurn());
            }
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

    private void ChangeEnemyModel()
    {
        if (currentEnemyModel != null && newEnemyPrefab != null)
        {
            // 現在のエネミーの位置と回転を保存
            Vector3 currentEnemyPosition = currentEnemyModel.transform.position;
            Quaternion currentEnemyRotation = currentEnemyModel.transform.rotation;

            // 新しいエネミーモデルのインスタンスを生成
            GameObject newEnemyModel = Instantiate(newEnemyPrefab, currentEnemyPosition, currentEnemyRotation);


            // 現在のエネミーモデルを削除
            Destroy(currentEnemyModel);

            // 新しいエネミーモデルを現在のエネミーモデルとして設定
            currentEnemyModel = newEnemyModel;

            Debug.Log("Enemy model has been changed and positioned.");
        }
        else
        {
            Debug.LogError("Enemy models are not assigned!");
        }
    }
}