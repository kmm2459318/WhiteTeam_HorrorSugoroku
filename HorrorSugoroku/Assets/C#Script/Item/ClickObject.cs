using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickObject : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // PlayerSaikoroクラスへの参照
    public PlayerInventory playerInventory; // PlayerInventory への参照
    public GameManager gameManager;
    [SerializeField] public TextMeshProUGUI Text;
    [SerializeField] public GameObject Canvas;
    [SerializeField] private Image cutInImage; // カットイン画像
    //  public string itemName = "鍵"; // 鍵の名前
    void Start()
    {
        // 自動で `PlayerInventory` を取得
        playerInventory = FindObjectOfType<PlayerInventory>();

        // `PlayerSaikoro` も自動取得
        playerSaikoro = FindObjectOfType<PlayerSaikoro>();

        gameManager = FindObjectOfType<GameManager>(); // GameManager を取得

        // Nullチェック
        if (playerInventory == null)
            Debug.LogError("PlayerInventory が見つかりません！プレイヤーにアタッチされていますか？");

        if (playerSaikoro == null)
            Debug.LogError("PlayerSaikoro が見つかりません！");

        if (gameManager == null)
            Debug.LogError("GameManager が見つかりません！");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {Debug.Log("aaa");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // レイキャストでオブジェクトを判定
            {
                Debug.Log("ii");
                if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Key") || hit.collider.CompareTag("Map")) // タグが "Item" "Key" "Map"の場合
                {
                    Debug.Log("uu");
                    // idoutyuがfalseのときのみクリック可能
                    if (!playerSaikoro.idoutyu)
                    {
                        Debug.Log("ee");
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);

                        if (distance <= 3f) // カメラからの距離が3以下の場合
                        {
                            // 🎲 ランダムでスクリプトA または B を実行
                           // int randomChoice = Random.Range(0, 4);

                            if (hit.collider.CompareTag("Key"))
                            {
                                ExecuteScriptA(hit.collider.gameObject); // スクリプトAを実行（アイテム取得）
                            }
                            else if (hit.collider.CompareTag("Map"))
                            {
                                ExecuteScriptB(); // スクリプトBを実行（例：敵を召喚）
                            }
                            else if (hit.collider.CompareTag("Item"))
                            {
                                ExecuteScriptC(); // スクリプトBを実行（例：敵を召喚）
                            }
                            //else if (randomChoice == 2)
                            //{
                            //    ExecuteScriptC();
                            //}
                            //else if (randomChoice == 3)
                            //{
                            //    ExecuteScriptC();
                            //}


                            ///*  string itemName = hit.collider.gameObject.name;*/ // 取得するアイテム名
                            //  Debug.Log(this.itemName + " を入手しました");


                            //  // インベントリが `null` でなければ追加
                            //  if (playerInventory != null)
                            //  {
                            //      playerInventory.AddItem(itemName);
                            //  }
                            //  else
                            //  {
                            //      Debug.LogError("playerInventory が設定されていません！");
                            //  }
                            // クリックしたオブジェクトを削除
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
        if (Canvas.active == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Canvas.SetActive(false);
            }
        }
    }
    void ExecuteScriptA(GameObject clickedItem)
    {
        string itemName = "食堂の鍵";  // **固定で「鍵」にする**
        Debug.Log(itemName + " を入手しました");

        if (playerInventory != null)
        {
            playerInventory.AddItem(itemName); // 🎯 インベントリに「鍵」を追加
        }
        else
        {
            Debug.LogError("playerInventory が設定されていません！");
        }
    }

    // 🎯 スクリプトB: 何か別の処理（例：敵を召喚）
    void ExecuteScriptB()
    {
        Debug.Log("地図のかけらを獲得！");
        if (gameManager != null)
        {
            gameManager.MpPlus(); // 🎯 `GameManager` の `MpPlus()` を実行
        }
        else
        {
            Debug.LogError("GameManager が見つかりません！");
        }
    }
    // 🎯 スクリプトC: 何か別の処理（例：敵を召喚）
    void ExecuteScriptC()
    {
        int randomChoice = Random.Range(0, 100);
        if (randomChoice % 5 == 0)
        {
            Debug.Log("ジャンプスケア");
            cutInImage.gameObject.SetActive(true); // 画像を表示
        }
        else
        {
            Debug.Log("何もなかった。");
            Canvas.SetActive(true);
            Text.text = ("何もなかった。");
        }
    }
}
