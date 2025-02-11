using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName = "鍵"; // 鍵の名前
    public AudioClip pickupSound; // 取得音
    private bool isCollected = false; // 取得済みかどうか

    void OnMouseDown()
    {
        if (!isCollected)
        {
            CollectKey();
        }
    }

    void CollectKey()
    {
        isCollected = true;

        // インベントリに鍵を追加
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddItem(keyName);
        }

        // 鍵の取得音を再生
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        // 鍵を削除
        Destroy(gameObject);
    }
}
