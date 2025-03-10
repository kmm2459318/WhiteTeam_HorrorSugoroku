using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using SmoothigTransform;
using UnityEngine.AI;
using JetBrains.Annotations;

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
    public NavMeshAgent agent;
    public CutIn cutIn;

    public GameObject currentEnemyModel; // 現在のエネミーモデル
    public GameObject EnemyCopy; // コピーエネミーモデル
    public GameObject newEnemyModelPrefab; // 新しいエネミーモデルのプレハブ
    public GameObject newEnemyModelPrefab2; // 6つ目のピースで変更する新しいエネミーモデルのプレハブ
    public GameObject MiniMapObj; // マップキャンバス

    private int playerTurnCount = 0; // プレイヤーのターン数をカウントする変数
    private MiniMap miniMap; // MiniMap クラスのインスタンス

    public AudioSource footstepSound; // 足音を管理するAudioSource

    private void Start()
    {
        MiniMapObj.SetActive(false); // マップキャンバスを非表示にする
        agent.enabled = false;
        UpdateTurnText(); // 初期ターン表示
        playerSaikoro.StartRolling(); // プレイヤーのターンを開始

        miniMap = FindObjectOfType<MiniMap>(); // MiniMap クラスのインスタンスを取得
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

        //Qキーでミニマップを表示
        if (Input.GetKey(KeyCode.Q))
        {
            MiniMapObj.SetActive(true);
            Debug.Log("Qキー入力");
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            MiniMapObj.SetActive(false);
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
        if (mapPiece == 3 || mapPiece == 6)
        {
            ChangeEnemyModel(mapPiece); // 引数を渡してメソッドを呼び出す
        }
    }

    public void ChangeEnemyModel(int mapPieceCount)
    {
        Animator currentAnimator = currentEnemyModel.GetComponent<Animator>();
        Animator newAnimator = null;

        if (mapPieceCount == 3)
        {
            newAnimator = newEnemyModelPrefab.GetComponent<Animator>();
        }
        else if (mapPieceCount == 6)
        {
            newAnimator = newEnemyModelPrefab2.GetComponent<Animator>();
        }

        if (currentAnimator != null && newAnimator != null)
        {
            AnimatorStateInfo currentState = currentAnimator.GetCurrentAnimatorStateInfo(0);
            newAnimator.Play(currentState.fullPathHash, -1, currentState.normalizedTime);
        }

        currentEnemyModel.SetActive(false);

        if (mapPieceCount == 3)
        {
            newEnemyModelPrefab.SetActive(true);
        }
        else if (mapPieceCount == 6)
        {
            newEnemyModelPrefab2.SetActive(true);
            newEnemyModelPrefab.SetActive(false); // 6つ目のピースで変更する際に前のモデルを非アクティブにする
        }
    }

    // ターンの切り替えを行うメソッド
    public void NextTurn()
    {
        isPlayerTurn = !isPlayerTurn; // ターンを切り替える
        UpdateTurnText(); // UIのテキストを更新

        if (isPlayerTurn)
        {
            agent.enabled = false;
            // プレイヤーのターンになったら足音を停止
            
            
                Debug.Log("ttttt");
                footstepSound.Stop(); // 足音を完全に停止
            

            // サイコロを振る
            playerSaikoro.DiceRoll();

            playerTurnCount++; // プレイヤーのターン数をカウント
            Debug.Log("Player Turn Count: " + playerTurnCount); // デバッグログ

            playerSaikoro.StartRolling();
        }
        else
        {
            agent.enabled = true;
            // エネミーのターン
            if (enemySaikoro != null)
            {
                Debug.Log("Starting enemy turn for new enemy.");
                StartCoroutine(enemySaikoro.EnemyTurn());
            }

            if (EnemyCopyOn && enemyCopySaikoro != null)
            {
                StartCoroutine(enemyCopySaikoro.EnemyTurn());
            }

            // エネミーターンになったら足音を再開
            if (footstepSound != null && !footstepSound.isPlaying)
            {
                footstepSound.Play(); // 足音を再開
            }
        }
    }

    public void MpPlus()
    {
        mapPiece++;
        Debug.Log("地図のかけらを獲得");

        // MiniMap を更新
        if (miniMap != null)
        {
            miniMap.UpdateMiniMap();
        }
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
