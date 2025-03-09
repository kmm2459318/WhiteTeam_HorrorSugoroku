using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // サイコロスクリプト
    private GridCell currentCell;       // プレイヤーが移動完了したセル
    private GridCell targetCell;        // プレイヤーが次に到達するセル
    private bool wasMoving = false;     // 前回の移動状態

    public GameObject HanteiBox;    // 四角いオブジェクト（既に存在するものを使用）

    void Start()
    {
        // 必要なスクリプトを自動取得
        if (playerSaikoro == null)
        {
            playerSaikoro = GetComponent<PlayerSaikoro>();
        }

        // 初期状態で全てのセルを非表示にする
        GridCell[] allCells = FindObjectsOfType<GridCell>();
        foreach (GridCell cell in allCells)
        {
            cell.SetVisibility(false);
        }

        // デバッグ用ログ
        if (HanteiBox != null)
        {
            Debug.Log("✅ HanteiBox が見つかりました。");
        }
        else
        {
            Debug.LogError("❌ HanteiBox が見つかりません。");
        }
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
            targetCell = cell; // 次に到達するセルをターゲットセルとして記録
        }
    }

    private void TriggerCurrentCellEvent()
    {
        // イベントを発火
        if (currentCell != null)
        {
            currentCell.ExecuteEvent();
        }
    }
}