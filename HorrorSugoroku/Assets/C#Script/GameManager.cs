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
        if (currentEnemyModel == null)
        {
            Debug.LogError("currentEnemyModel is not assigned.");
            return;
        }

        if (newEnemyModelPrefab == null)
        {
            Debug.LogError("newEnemyModelPrefab is not assigned.");
            return;
        }

        // 新しいエネミーモデルをアクティブにする
        newEnemyModelPrefab.SetActive(true);
        Debug.Log("New enemy model is now active: " + newEnemyModelPrefab.activeInHierarchy);

        // 新しいエネミーモデルを元のエネミーモデルの位置に移動
        StartCoroutine(MoveAndSetupNewEnemyModel());
    }

    private IEnumerator MoveAndSetupNewEnemyModel()
    {
        // 新しいエネミーモデルを元のエネミーモデルの位置に移動
        newEnemyModelPrefab.transform.position = currentEnemyModel.transform.position;
        newEnemyModelPrefab.transform.rotation = currentEnemyModel.transform.rotation;
        Debug.Log("New enemy model moved to position: " + newEnemyModelPrefab.transform.position);

        // フレームの終わりまで待つ
        yield return new WaitForEndOfFrame();

        // 新しいエネミーモデルにエネミーの仕様を適用
        EnemySaikoro oldEnemySaikoro = currentEnemyModel.GetComponent<EnemySaikoro>();
        EnemySaikoro newEnemySaikoro = newEnemyModelPrefab.GetComponent<EnemySaikoro>();
        if (newEnemySaikoro != null)
        {
            // 必要なコンポーネントや設定を引き継ぐ
            newEnemySaikoro.player = oldEnemySaikoro.player;
            newEnemySaikoro.wallLayer = oldEnemySaikoro.wallLayer;
            newEnemySaikoro.discoveryBGM = oldEnemySaikoro.discoveryBGM;
            newEnemySaikoro.undetectedBGM = oldEnemySaikoro.undetectedBGM;
            newEnemySaikoro.footstepSound = oldEnemySaikoro.footstepSound;
            newEnemySaikoro.enemyController = oldEnemySaikoro.enemyController;
            newEnemySaikoro.gameManager = this;
            newEnemySaikoro.enemyLookAtPlayer = oldEnemySaikoro.enemyLookAtPlayer;
            newEnemySaikoro.playerCloseMirror = oldEnemySaikoro.playerCloseMirror;
            newEnemySaikoro.mokushi = oldEnemySaikoro.mokushi;
            newEnemySaikoro.idoukagen = oldEnemySaikoro.idoukagen;
            newEnemySaikoro.skill1 = oldEnemySaikoro.skill1;
            newEnemySaikoro.skill2 = oldEnemySaikoro.skill2;
            newEnemySaikoro.isTrapped = oldEnemySaikoro.isTrapped;

            // アニメーションや状態を引き継ぐ
            newEnemySaikoro.SetAnimationState(oldEnemySaikoro.GetCurrentAnimationState());
        }

        // EnemyControllerの設定を引き継ぐ
        EnemyController oldEnemyController = currentEnemyModel.GetComponent<EnemyController>();
        EnemyController newEnemyController = newEnemyModelPrefab.GetComponent<EnemyController>();
        if (newEnemyController != null)
        {
            newEnemyController.gameManager = oldEnemyController.gameManager;
            newEnemyController.enemySaikoro = newEnemySaikoro;
        }

        // EnemyLookAtPlayerの設定を引き継ぐ
        EnemyLookAtPlayer oldEnemyLookAtPlayer = currentEnemyModel.GetComponent<EnemyLookAtPlayer>();
        EnemyLookAtPlayer newEnemyLookAtPlayer = newEnemyModelPrefab.GetComponent<EnemyLookAtPlayer>();
        if (newEnemyLookAtPlayer != null)
        {
            newEnemyLookAtPlayer.player = oldEnemyLookAtPlayer.player;
            newEnemyLookAtPlayer.wallLayer = oldEnemyLookAtPlayer.wallLayer;
        }

        // SmoothTransformの設定を引き継ぐ
        SmoothTransform oldSmoothTransform = currentEnemyModel.GetComponent<SmoothTransform>();
        SmoothTransform newSmoothTransform = newEnemyModelPrefab.GetComponent<SmoothTransform>();
        if (newSmoothTransform != null)
        {
            newSmoothTransform.TargetPosition = oldSmoothTransform.TargetPosition;
            newSmoothTransform.TargetRotation = oldSmoothTransform.TargetRotation;
            newSmoothTransform.PosFact = oldSmoothTransform.PosFact;
            newSmoothTransform.RotFact = oldSmoothTransform.RotFact;
        }

        if (EnemyCopyOn)
        {
            enemyCopySaikoro = newEnemyModelPrefab.GetComponent<EnemySaikoro>();
        }

        // currentEnemyModelを新しいエネミーモデルに更新
        currentEnemyModel = newEnemyModelPrefab;

        Debug.Log("New enemy model setup complete.");
    }
    private IEnumerator MoveOldEnemyModelFarAway(EnemySaikoro oldEnemySaikoro)
    {
        yield return new WaitForEndOfFrame(); // 新しいエネミーモデルの移動が完了するのを待つ

        // 元のエネミーモデルを遠くに移動
        oldEnemySaikoro.transform.position = new Vector3(1000, 1000, 1000); // 遠くに移動
        Debug.Log("Old enemy model moved to a distant location: " + oldEnemySaikoro.transform.position);
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