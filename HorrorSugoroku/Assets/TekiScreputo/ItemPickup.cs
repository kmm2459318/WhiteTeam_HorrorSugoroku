using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "鍵"; // 取得できるアイテム名

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.AddItem(itemName); // アイテムを取得
            inventory.ShowInventory(); // インベントリをログに表示
             Destroy(gameObject); // アイテムを消す
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
