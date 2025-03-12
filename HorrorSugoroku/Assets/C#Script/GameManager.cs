using UnityEngine;
using TMPro;
using System.Collections;
using SmoothigTransform;
using UnityEngine.AI;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro用のターン数表示
    public TMP_Text turnIndicatorText; // 新しいターン表示用のテキスト
    public bool isPlayerTurn = true; // プレイヤーのターンかどうかを示すフラグ
    public bool EnemyCopyOn1 = false;
    public bool EnemyCopyOn2 = false;
    public bool EnemyCopyOn3 = false;
    public bool EnemyCopyOn4 = false;
    public bool EnemyCopyOn5 = false;
    public int enemyTurnFinCount = 0;
    public int mapPiece = 0;
    public int Doll = 0; // 人形の数

    public PlayerSaikoro playerSaikoro;
    public EnemySaikoro enemySaikoro;
    public EnemySaikoro enemyCopySaikoro;
    public NavMeshAgent agent;
    public CutIn cutIn;

    public GameObject currentEnemyModel; // 現在のエネミーモデル
    public GameObject EnemyCopy1; // コピーエネミーモデル
    public GameObject EnemyCopy1demo;
    public GameObject EnemyCopy2;
    public GameObject EnemyCopy3;
    public GameObject EnemyCopy4;
    public GameObject EnemyCopy5;
    public GameObject newEnemyModelPrefab; // 新しいエネミーモデルのプレハブ
    public GameObject newEnemyModelPrefab2; // 6つ目のピースで変更する新しいエネミーモデルのプレハブ
    public GameObject optionCanvas; // OptionCanvasを追加

    //public GameObject MiniMapObj; // マップキャンバス

    private int playerTurnCount = 0; // プレイヤーのターン数をカウントする変数
    private MiniMap miniMap; // MiniMap クラスのインスタンス

    public AudioSource footstepSound; // 足音を管理するAudioSource

    public DiceController diceController;
    private bool wasFootstepPlayingBeforePause = false;

    private void Start()
    {
        Application.targetFrameRate = 60;

        //MiniMapObj.SetActive(false); // マップキャンバスを非表示にする
        //MiniMapObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1064, -574); // UIの座標を設定
        //agent.enabled = false;
        UpdateTurnText(); // 初期ターン表示
        playerSaikoro.StartRolling(); // プレイヤーのターンを開始

        miniMap = FindObjectOfType<MiniMap>(); // MiniMap クラスのインスタンスを取得
    }

    private void Update()
    {
        // 持っている人形の数を増やす
        if (Input.GetKeyDown(KeyCode.R))
        {
            //MpPlus();
            Doll++; 
        }

        if (optionCanvas != null)
        {
            if (optionCanvas.activeSelf)
            {
                if (footstepSound.isPlaying)
                {
                    wasFootstepPlayingBeforePause = true;
                    footstepSound.Stop();
                }
                return;
            }
            else if (wasFootstepPlayingBeforePause)
            {
                footstepSound.Play();
                wasFootstepPlayingBeforePause = false;
            }
        }

        ////Qキーでミニマップを表示
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    //MiniMapObj.SetActive(true);
        //    RectTransform miniMapRect = MiniMapObj.GetComponent<RectTransform>();
        //    Vector2 currentPos = miniMapRect.anchoredPosition;

        //    if (currentPos.y < -273) // 目標位置に達していなければ10ずつ加算
        //    {
        //        miniMapRect.anchoredPosition = new Vector2(currentPos.x, currentPos.y + 10);
        //    }

        //}
        //else if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    //MiniMapObj.SetActive(false);
        //    RectTransform miniMapRect = MiniMapObj.GetComponent<RectTransform>();
        //    Vector2 currentPos = miniMapRect.anchoredPosition;

        //    if (currentPos.y > -574) // 元の位置に戻るように10ずつ減算
        //    {
        //        miniMapRect.anchoredPosition = new Vector2(currentPos.x, currentPos.y - 10);
        //    }
        //}

        //
        if (mapPiece >= 1)
        {
            Destroy(EnemyCopy1demo);
            EnemyCopy1.SetActive(true);
            EnemyCopyOn1 = true;
        }
        if (mapPiece >= 2)
        {
            EnemyCopy2.SetActive(true);
            EnemyCopyOn2 = true;
        }
        if (mapPiece >= 3)
        {
            EnemyCopy3.SetActive(true);
            EnemyCopyOn3 = true;
        }
        if (mapPiece >= 4)
        {
            EnemyCopy4.SetActive(true);
            EnemyCopyOn4 = true;
        }
        if (mapPiece >= 5)
        {
            EnemyCopy5.SetActive(true);
            EnemyCopyOn5 = true;
        }

        if (enemyTurnFinCount == 2)
        {
            enemyTurnFinCount = 0;
            NextTurn();
        }

        // マップのピースが3枚手に入ったらエネミーモデルを変更
        /*if (mapPiece == 3 || mapPiece == 6)
        {
            ChangeEnemyModel(mapPiece); // 引数を渡してメソッドを呼び出す
        }*/
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

            //サイコロの状態リセット
            diceController.ResetDiceState();

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

    public bool CanPlaceDoll()
    {
        return Doll > 0; // 1つ以上人形を持っていれば配置可能
    }

    public void PlaceDoll()
    {
        if (Doll > 0)
        {
            Doll--; // 1つ配置したので減らす
        }
    }
}
