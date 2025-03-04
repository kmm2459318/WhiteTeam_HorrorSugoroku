using UnityEngine;

public class RigidbodyAdder : MonoBehaviour
{
    void Start()
    {
        // シーン内のすべてのGridCellオブジェクトを取得
        GridCell[] gridCells = FindObjectsOfType<GridCell>();

        foreach (GridCell cell in gridCells)
        {
            // 親オブジェクトにリジッドボディを追加
            Rigidbody rb = cell.gameObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = cell.gameObject.AddComponent<Rigidbody>();
                rb.isKinematic = true; // Is Kinematicをチェック
                Debug.Log($"✅ リジッドボディを追加しました: {cell.name}");
            }
            else
            {
                Debug.Log($"⚠️ 既にリジッドボディが存在します: {cell.name}");
            }
        }
    }
}