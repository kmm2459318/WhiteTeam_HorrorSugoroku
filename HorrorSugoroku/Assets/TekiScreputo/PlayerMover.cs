using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // サイコロスクリプト
    public GridCell currentCell;        // プレイヤーが移動完了したセル（インスペクターから設定可能）
    public GameObject detectionBox;     // 四角いオブジェクト
    private GridCell targetCell;        // プレイヤーが次に到達するセル
    private bool wasMoving = false;     // 前回の移動状態
    private HashSet<GridCell> visibleCells = new HashSet<GridCell>(); // 表示されているマスのセット

    void Start()
    {
        // 必要なスクリプトを自動取得
        if (playerSaikoro == null)
        {
            playerSaikoro = GetComponent<PlayerSaikoro>();
        }

        // 初期状態で現在のマスを表示
        UpdateVisibleCells();
    }

    void Update()
    {
        // プレイヤーの移動が完了したタイミングを監視
        if (wasMoving && !playerSaikoro.idoutyu)
        {
            // プレイヤーが完全に止まったマスでイベントを発火
            if (targetCell != null)
            {
                currentCell = targetCell;
                TriggerCurrentCellEvent();
                targetCell = null; // イベント発火後にターゲットセルをリセット

                // 現在のマスを更新
                UpdateVisibleCells();
            }
        }

        // 状態を更新
        wasMoving = playerSaikoro.idoutyu;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 四角いオブジェクトがマスに触れたときの処理
        if (other.gameObject == detectionBox)
        {
            GridCell cell = other.GetComponent<GridCell>();
            if (cell != null)
            {
                visibleCells.Add(cell);
                cell.SetVisibility(true);
                Debug.Log($"ターゲットセルに到達: {cell.name}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 四角いオブジェクトがマスから離れたときの処理
        if (other.gameObject == detectionBox)
        {
            GridCell cell = other.GetComponent<GridCell>();
            if (cell != null)
            {
                visibleCells.Remove(cell);
                cell.SetVisibility(false);
                Debug.Log($"ターゲットセルから離れた: {cell.name}");
            }
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

    private void UpdateVisibleCells()
    {
        // 全てのマスを非アクティブにする
        GridCell[] allCells = FindObjectsOfType<GridCell>(true);
        foreach (GridCell cell in allCells)
        {
            cell.SetVisibility(false);
        }

        // 現在表示されているマスを再表示
        foreach (GridCell cell in visibleCells)
        {
            cell.SetVisibility(true);
        }
    }
}