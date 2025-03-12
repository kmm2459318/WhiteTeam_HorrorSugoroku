using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void OnEnable()
    {
        Outline.OnOutlineKeyPressed += HandleOutlineKeyPressed;
    }

    void OnDisable()
    {
        Outline.OnOutlineKeyPressed -= HandleOutlineKeyPressed;
    }

    private void HandleOutlineKeyPressed()
    {
        // プレイヤーインベントリにアイテムを追加
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.AddItem("一階のカギ");
            Debug.Log("一階のカギを手に入れました！");
        }
        else
        {
            Debug.LogError("PlayerInventory が見つかりません！");
        }
    }
}