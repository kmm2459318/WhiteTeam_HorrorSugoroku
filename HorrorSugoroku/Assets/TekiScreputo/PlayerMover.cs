using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // サイコロスクリプト
    private GridCell currentCell;       // プレイヤーが移動完了したセル
    private GridCell targetCell;        // プレイヤーが次に到達するセル
    private bool wasMoving = false;     // 前回の移動状態

    private GameObject detectionBox;    // 四角いオブジェクト

    void Start()
    {
        // 必要なスクリプトを自動取得
        if (playerSaikoro == null)
        {
            playerSaikoro = GetComponent<PlayerSaikoro>();
        }

        // 四角いオブジェクトを作成してプレイヤーにアタッチ
        detectionBox = new GameObject("DetectionBox");
        detectionBox.transform.SetParent(transform);
        detectionBox.transform.localPosition = Vector3.zero;
        detectionBox.transform.localScale = new Vector3(3, 3, 3); // サイズを調整済み
        BoxCollider boxCollider = detectionBox.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        detectionBox.tag = "DetectionBox"; // タグを設定
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
            Debug.Log($"イベント発動: {currentCell.name}");
            currentCell.ExecuteEvent();
        }
        else
        {
            Debug.LogWarning("現在のセルが設定されていません。");
        }
    }
}