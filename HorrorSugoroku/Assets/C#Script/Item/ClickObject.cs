using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // PlayerSaikoroクラスへの参照
    public PlayerInventory playerInventory; // PlayerInventory への参照
    public string itemName = "鍵"; // 鍵の名前
    void Start()
    {
        // 自動で `PlayerInventory` を取得
        playerInventory = FindObjectOfType<PlayerInventory>();

        // `PlayerSaikoro` も自動取得
        playerSaikoro = FindObjectOfType<PlayerSaikoro>();

        // Nullチェック
        if (playerInventory == null)
            Debug.LogError("PlayerInventory が見つかりません！プレイヤーにアタッチされていますか？");

        if (playerSaikoro == null)
            Debug.LogError("PlayerSaikoro が見つかりません！");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // レイキャストでオブジェクトを判定
            {
                if (hit.collider.CompareTag("Object")) // タグが "Object" の場合
                {
                    // idoutyuがfalseのときのみクリック可能
                    if (!playerSaikoro.idoutyu)
                    {
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);

                        if (distance <= 3f) // カメラからの距離が3以下の場合
                        {
                            string itemName = hit.collider.gameObject.name; // 取得するアイテム名
                            Debug.Log(itemName + " を入手しました");

                          
                            // インベントリが `null` でなければ追加
                            if (playerInventory != null)
                            {
                                playerInventory.AddItem(itemName);
                            }
                            else
                            {
                                Debug.LogError("playerInventory が設定されていません！");
                            }
                            // クリックしたオブジェクトを削除
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }
}
