using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // サイコロスクリプト
    private GridCell currentCell;       // プレイヤーが移動完了したセル
    private bool wasMoving = false;     // 前回の移動状態

    void Start()
    {
        // 必要なスクリプトを自動取得
        if (playerSaikoro == null)
        {
            playerSaikoro = GetComponent<PlayerSaikoro>();
        }
    }

    void Update()
    {
        // プレイヤーの移動が完了したタイミングを監視
        if (wasMoving && !playerSaikoro.idoutyu)
        {
            TriggerCurrentCellEvent();
            if (currentCell != null)
            {
                Debug.Log($"プレイヤーが {currentCell.name} に到達しました。");
            }
        }

        // 状態を更新
        wasMoving = playerSaikoro.idoutyu;
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが通過したセルを記録
        GridCell cell = other.GetComponent<GridCell>();
        if (cell != null)
        {
            currentCell = cell;
        }
    }

    private void TriggerCurrentCellEvent()
    {
        // イベントを発火
        if (currentCell != null)
        {
            Debug.Log($"イベント発動: {currentCell.name}");
            currentCell.ExecuteEvent();
        }
        else
        {
            Debug.LogWarning("現在のセルが設定されていません。");
        }
    }
}