using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro用のターン数表示
    public TMP_Text turnIndicatorText; // 新しいターン表示用のテキスト
    public bool isPlayerTurn = true; // プレイヤーのターンかどうかを示すフラグ

    public PlayerSaikoro playerSaikoro;
    public EnemySaikoro enemySaikoro;

    public GameObject currentEnemyModel; // 現在のエネミーモデル
    public GameObject newEnemyPrefab; // 新しいエネミーモデルのプレファブ

    private int playerTurnCount = 0; // プレイヤーのターン数をカウントする変数

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
            playerTurnCount++; // プレイヤーのターン数をカウント
            Debug.Log("Player Turn Count: " + playerTurnCount); // デバッグログ

            // プレイヤーのターンが5ターン目になったらエネミーモデルを変更
            if (playerTurnCount == 5)
            {
                ChangeEnemyModel();
            }

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

    private void ChangeEnemyModel()
    {
        if (currentEnemyModel != null && newEnemyPrefab != null)
        {
            // 現在のエネミーの位置と回転を保存
            Vector3 currentEnemyPosition = currentEnemyModel.transform.position;
            Quaternion currentEnemyRotation = currentEnemyModel.transform.rotation;

            // 新しいエネミーモデルのインスタンスを生成
            GameObject newEnemyModel = Instantiate(newEnemyPrefab, currentEnemyPosition, currentEnemyRotation);

            // 新しいモデルの位置とレンダラーの状態を確認
            Debug.Log("New Enemy Model Position: " + newEnemyModel.transform.position);
            Renderer[] renderers = newEnemyModel.GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0)
            {
                foreach (Renderer renderer in renderers)
                {
                    Debug.Log("Renderer " + renderer.name + " Enabled: " + renderer.enabled);
                    renderer.enabled = true; // レンダラーを有効にする
                }
            }
            else
            {
                Debug.LogError("New Enemy Model does not have any Renderer components!");
            }

            // 現在のエネミーモデルを削除
            Destroy(currentEnemyModel);

            Debug.Log("Enemy model has been changed and positioned.");
        }
        else
        {
            Debug.LogError("Enemy models are not assigned!");
        }
    }
}