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
    public GameObject EnemyCopy;
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


    private void ChangeEnemyModel()
    {
        if (currentEnemyModel != null)
        {
            // 元のエネミーモデルを非アクティブにする
            currentEnemyModel.SetActive(false);
        }

        // 新しいエネミーモデルをアクティブにする
        if (newEnemyModelPrefab != null)
        {
            newEnemyModelPrefab.SetActive(true);

            // 新しいエネミーモデルにエネミーの仕様を適用
            EnemySaikoro oldEnemySaikoro = currentEnemyModel.GetComponent<EnemySaikoro>();
            enemySaikoro = newEnemyModelPrefab.GetComponent<EnemySaikoro>();
            if (enemySaikoro != null)
            {
                // 必要なコンポーネントや設定を引き継ぐ
                enemySaikoro.player = oldEnemySaikoro.player;
                enemySaikoro.wallLayer = oldEnemySaikoro.wallLayer;
                enemySaikoro.discoveryBGM = oldEnemySaikoro.discoveryBGM;
                enemySaikoro.undetectedBGM = oldEnemySaikoro.undetectedBGM;
                enemySaikoro.footstepSound = oldEnemySaikoro.footstepSound;
                enemySaikoro.enemyController = oldEnemySaikoro.enemyController;
                enemySaikoro.gameManager = this;
                enemySaikoro.enemyLookAtPlayer = oldEnemySaikoro.enemyLookAtPlayer;
                enemySaikoro.playerCloseMirror = oldEnemySaikoro.playerCloseMirror;
                enemySaikoro.mokushi = oldEnemySaikoro.mokushi;
                enemySaikoro.idoukagen = oldEnemySaikoro.idoukagen;
                enemySaikoro.skill1 = oldEnemySaikoro.skill1;
                enemySaikoro.skill2 = oldEnemySaikoro.skill2;
                enemySaikoro.isTrapped = oldEnemySaikoro.isTrapped;

                // アニメーションや状態を引き継ぐ
                enemySaikoro.SetAnimationState(oldEnemySaikoro.GetCurrentAnimationState());
                enemySaikoro.transform.position = oldEnemySaikoro.transform.position;
                enemySaikoro.transform.rotation = oldEnemySaikoro.transform.rotation;
            }

            if (EnemyCopyOn)
            {
                enemyCopySaikoro = newEnemyModelPrefab.GetComponent<EnemySaikoro>();
            }

            // currentEnemyModelを新しいエネミーモデルに更新
            currentEnemyModel = newEnemyModelPrefab;
        }
        else
        {
            Debug.LogError("newEnemyModelPrefab is not assigned.");
        }
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

            // エネミーのアニメーションをIdleに切り替える
            /*enemySaikoro.SetIdle();
            if (EnemyCopyOn)
            {
                enemyCopySaikoro.SetIdle();
            }*/
        }
        else
        {
            // エネミーのアニメーションをRunに切り替える
            /*enemySaikoro.SetRun();
            if (EnemyCopyOn)
            {
                enemyCopySaikoro.SetRun();
            }*/

            StartCoroutine(enemySaikoro.EnemyTurn());

            if (EnemyCopyOn)
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