using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName = "";  // 拾うキーの名前
   // public float pickupCooldown = 5f;  // 次に拾えるまでの待機時間（秒）

    private bool canPickup = true;  // キーを拾える状態かどうか

    // PlayerInventory スクリプトを参照
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>(); // PlayerInventory を取得
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーがキーに触れたときにインベントリに追加
        if (other.CompareTag("Player") && canPickup)
        {
            PickupKey();
        }
    }

    // キーを拾う処理
    private void PickupKey()
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(keyName); // インベントリにキーを追加
            Debug.Log($"{keyName} を拾いました！");

           
        }
    }

   
}