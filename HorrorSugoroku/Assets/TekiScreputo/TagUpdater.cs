using UnityEngine;

public class TagUpdater : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 全ての Outline コンポーネントを持つオブジェクトを取得
        Outline[] outlineObjects = FindObjectsOfType<Outline>();

        int updatedCount = 0;

        foreach (Outline outline in outlineObjects)
        {
            GameObject obj = outline.gameObject;

            // タグが "Untagged" の場合のみ "Item" に変更
            if (obj.tag == "Untagged")
            {
                obj.tag = "Item";
                updatedCount++;
            }
        }

        Debug.Log($"タグを 'Item' に変更したオブジェクト数: {updatedCount}");
    }
}
