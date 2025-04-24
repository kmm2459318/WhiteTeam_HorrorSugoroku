using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void OnEnable()
    {
        //Outline.OnOutlineKeyPressed += HandleOutlineKeyPressed;
    }

    void OnDisable()
    {
        //Outline.OnOutlineKeyPressed -= HandleOutlineKeyPressed;
    }

    private void HandleOutlineKeyPressed()
    {
        // プレイヤーインベントリにアイテムを追加
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            // ユニークなIDを生成（例: 名前 + 現在時刻）
            string itemID = "一階のカギ_" + Time.time;

            playerInventory.AddItem("一階のカギ", itemID); // itemID を渡す
            Debug.Log($"一階のカギを手に入れました！ID: {itemID}");
        }
        else
        {
            Debug.LogError("PlayerInventory が見つかりません！");
        }
    }
}