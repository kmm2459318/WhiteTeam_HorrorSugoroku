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
      public string ItemKey = ""; // 鍵の名前
   // public float interactDistance = 1f; // **インタラクト可能な距離**
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) // **3m 以内のオブジェクトを取得**
        {
            // **オブジェクトにクリック済みフラグを追加**
            Clickebleobject clickedObj = hit.collider.GetComponent<Clickebleobject>();

            Debug.Log("ii");
            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Key") || hit.collider.CompareTag("Map")) // タグが "Item" "Key" "Map"の場合
            {
                Debug.Log("uu");
                // idoutyuがfalseのときのみクリック可能
                if (!playerSaikoro.idoutyu)
                {
                    if (IsLookingAtObject(hit.collider.gameObject)) // **視線の方向にあるか確認**
                    {
                        //if (!itemPickedUp) // **二重処理防止**
                        Debug.Log("ee");
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);

                        if (distance <= 3f) // カメラからの距離が3以下の場合
                        {
                            // 🎲 ランダムでスクリプトA または B を実行
                            // int randomChoice = Random.Range(0, 4);
                            if (Input.GetMouseButtonDown(0))
                            { // 左クリック
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
                                else if (hit.collider.CompareTag("Other"))
                                {

                                }
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
    }
    bool IsLookingAtObject(GameObject obj)
    {
        Vector3 directionToObject = (obj.transform.position - Camera.main.transform.position).normalized;
        float dotProduct = Vector3.Dot(Camera.main.transform.forward, directionToObject);

        return dotProduct > 0.8f; // **0.8以上ならプレイヤーの視線方向にある**
    }
    void ExecuteScriptA(GameObject clickedItem)
    {
       
        if (!string.IsNullOrEmpty(ItemKey)) // **空文字を追加しない**
        {
            //  string ItemKey = "";  // **固定で「鍵」にする**
            Debug.Log(ItemKey + " を入手しました");

            if (playerInventory != null)
            {
                playerInventory.AddItem(ItemKey); // 🎯 インベントリに「鍵」を追加
            }
            else
            {
                Debug.LogError("playerInventory が設定されていません！");
            }
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

    void OtherScript()
    {
        int randomChoice = Random.Range(0,4);
        if(randomChoice == 0 || randomChoice == 1)
        {
            cutInImage.gameObject.SetActive(true); // 画像を表示
        }
    }
}
