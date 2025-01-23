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
            StartCoroutine(TriggerCurrentCellEventWithDelay(1.0f));
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
            Debug.Log($"プレイヤーが {cell.name} に到達しました。");
        }
    }

    private IEnumerator TriggerCurrentCellEventWithDelay(float delay)
    {
        // 遅延を待機
        yield return new WaitForSeconds(delay);

        // イベントを発火
        if (currentCell != null)
        {
            Debug.Log($"1秒後にイベント発動: {currentCell.name}");
            currentCell.ExecuteEvent();
        }
        else
        {
            Debug.LogWarning("現在のセルが設定されていません。");
        }
    }
}
