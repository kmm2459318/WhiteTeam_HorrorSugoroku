using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClickObject : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro;
    public PlayerInventory playerInventory;
    public GameManager gameManager;
    public KeyRandomizer keyRandomizer; // ←追加！
    public CurseSlider curse;
    public Camera raycastCamera;

    [SerializeField] public GameObject Canvas;
    [SerializeField] private Image cutInImage;
    private HashSet<string> cooldownItems = new HashSet<string>();
    // 名前ごとに「取得済みの時間」を記録する辞書
    private Dictionary<string, float> keyObtainedTime = new Dictionary<string, float>();
    //private HashSet<string> obtainedKeys = new HashSet<string>();
    private bool keyCooldownActive = false;
    // 鍵取得制限時間（秒）
    private bool canAddItem = true;  // アイテム追加の制限フラグ
    private bool isCooldown = false; // クールダウン中かどうかのフラグ
    private bool waitingForDice = false;

    // ClickObject.cs に追加
    private Dictionary<string, float> itemAddCooldowns = new Dictionary<string, float>();
    public float itemCooldownDuration = 5f; // ← クールダウン時間（秒）

    private bool isRandomItemCooldown = false;
    [SerializeField] private float randomItemCooldownTime = 5f; // 次の抽選まで5秒待機

    [SerializeField] private float keyCooldownTime = 2f;  // 例: 10秒で再取得可能
    private bool hasClicked = false; // クリック多重防止用フラグ

    public BreakerController breakerController;
    public ElevatorIdou elevatorIdou;
    public bool LookElevatorDoor = false;


    [System.Serializable]
    public class ItemIconEntry
    {
        public string itemName;
        public Sprite icon;
    }


    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private List<ItemIconEntry> itemIcons = new List<ItemIconEntry>();
    [SerializeField] private Image uiIconImage; // ← UI上に表示するImageコンポーネント（Canvas内）
    [SerializeField] private GameObject itemCanvas;


    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerSaikoro = FindObjectOfType<PlayerSaikoro>();
        gameManager = FindObjectOfType<GameManager>();
        keyRandomizer = FindObjectOfType<KeyRandomizer>(); // ←追加！

        if (playerInventory == null) Debug.LogError("PlayerInventory が見つかりません！");
        if (playerSaikoro == null) Debug.LogError("PlayerSaikoro が見つかりません！");
        if (gameManager == null) Debug.LogError("GameManager が見つかりません！");
        if (keyRandomizer == null) Debug.LogError("KeyRandomizer が見つかりません！");

        if (itemCanvas != null) itemCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClicked)
        {
            hasClicked = true;
            Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Key") || hit.collider.CompareTag("Doll") || hit.collider.CompareTag("Strongbox") || hit.collider.CompareTag("ElevatorDoor") || hit.collider.CompareTag("Breaker"))
                {
                    float distance = Vector3.Distance(raycastCamera.transform.position, hit.collider.transform.position);
                    if (distance <= 3f)
                    {
                        if (playerSaikoro.exploring)
                        {
                            //if (!playerSaikoro.idoutyu)
                            //{
                            if (hit.collider.CompareTag("Key"))
                            {
                                ExecuteScriptA(hit.collider.gameObject);
                            }
                            else if (hit.collider.CompareTag("Doll"))
                            {
                                ExecuteScriptB();
                            }
                            else if (hit.collider.CompareTag("Item"))
                            {
                                ExecuteScriptC(hit.collider.gameObject);
                            }
                            else if (hit.collider.CompareTag("Strongbox"))
                            {
                                hit.collider.gameObject.GetComponent<StrongboxController>().StrongBoxDiceOn();// 
                            }
                            else if (hit.collider.CompareTag("Breaker"))
                            {
                                breakerController.BreakerHantei();
                            }
                            // 🎲 ランダムでスクリプトA または B を実行
                            // int randomChoice = Random.Range(0, 4);
                            if (Input.GetMouseButtonDown(0))
                            { // 左クリック
                                if (hit.collider.CompareTag("Key"))
                                {
                                    ExecuteScriptA(hit.collider.gameObject); // スクリプトAを実行（アイテム取得）
                                                                             //Destroy(hit.collider.gameObject);
                                }
                                else if (hit.collider.CompareTag("Doll"))
                                {
                                    ExecuteScriptB(); // スクリプトBを実行（例：敵を召喚）
                                }
                                else if (hit.collider.CompareTag("Item"))
                                {
                                    if (!curse.curse1_3)
                                    {
                                        ExecuteScriptC(hit.collider.gameObject); // スクリプトBを実行（例：敵を召喚）
                                                                                 // クリック後にオブジェクトのタグを「Untagged」に変更
                                        hit.collider.gameObject.tag = "Untagged";
                                        //  Destroy(hit.collider.gameObject);
                                        //curse.curse1Turn--;
                                    }
                                }
                                else if (hit.collider.CompareTag("Strongbox"))
                                {
                                    hit.collider.gameObject.GetComponent<StrongboxController>().StrongBoxDiceOn();// 
                                }
                                //else if (hit.collider.CompareTag("Other"))
                                //{

                                //}
                                // Destroy(hit.collider.gameObject);

                            }
                            // Destroy(hit.collider.gameObject);
                            //}
                        }
                        else
                        {
                            if (hit.collider.CompareTag("ElevatorDoor"))
                            {
                                elevatorIdou.IdouHantei();
                            }
                        }
                    }
                }
            }

            StartCoroutine(ResetClick()); // フラグリセットコルーチン呼び出し
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("クリック検知");

        //    Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        GameObject hitObj = hit.collider.gameObject;
        //        Debug.Log("ヒット！オブジェクト: " + hitObj.name +
        //                  ", タグ: " + hitObj.tag +
        //                  ", 親: " + hitObj.transform.parent?.name);

        //        // 親のタグもチェックする
        //        string tagToCheck = hitObj.tag;
        //        if (tagToCheck == "Untagged" && hitObj.transform.parent != null)
        //        {
        //            tagToCheck = hitObj.transform.parent.tag;
        //        }

        //        if (tagToCheck == "Item" || tagToCheck == "Key" || tagToCheck == "Doll" || tagToCheck == "Strongbox" || tagToCheck == "ElevatorDoor")
        //        {
        //            Debug.Log("指定タグに一致しました: " + tagToCheck);
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("Raycastヒットなし");
        //    }

        //    hasClicked = true;
        //    StartCoroutine(ResetClick());
        //}

        //if (Canvas.activeSelf && Input.GetKeyDown(KeyCode.Space))
        //{
        //    Canvas.SetActive(false);
        //}

    }

    IEnumerator ResetClick()
    {
        yield return new WaitForSeconds(0.1f); // 多重クリック防止時間
        hasClicked = false;
    }

    void ExecuteScriptA(GameObject clickedItem)
    {
        string keyName = clickedItem.name;

        if (string.IsNullOrEmpty(keyName))
        {
            Debug.LogWarning("KeyNameが設定されていません！");
            return;
        }

        // ユニークなIDを生成（例: 名前 + 現在時刻）
        string itemID = keyName + "_" + Time.time;

        // クールダウン中で、かつすでに所持しているアイテムの場合は追加しない
        if (isCooldown && playerInventory.HasItem(keyName))
        {
            Debug.Log($"{keyName} はすでにインベントリにあり、クールダウン中のため追加しません。");
            return;
        }

        // アイテムがインベントリにまだない、またはクールダウンが終わった場合
        if (!playerInventory.HasItem(keyName) || !isCooldown)
        {
            // アイテムをインベントリに追加
            playerInventory.AddItem(keyName, itemID); // itemIDを渡す
            Debug.Log($"{keyName} をインベントリに追加しました。（ID: {itemID}）");

            // ✅ タグを Untagged に変更
            clickedItem.tag = "Untagged";


            ShowItemUIAndPrefab(keyName);
            // クールダウン後にフラグを解除
            StartCoroutine(CooldownAfterAddItem());
        }
    }

    void ExecuteScriptC(GameObject clickedItem)
    {

        if (isRandomItemCooldown)
        {
            Debug.Log("🎲 クールダウン中です。抽選はもう少し待ってください。");
            return;
        }

        // タグが "Item" でなければ無視
        if (clickedItem.tag != "Item")
        {
            Debug.Log($"⛔ タグが 'Item' ではないため無視します（{clickedItem.name}）");
            return;
        }

        // 抽選候補
        string[] randomItems = { "身代わり人形", "回復薬", "何もない" };
        string selected = randomItems[Random.Range(0, randomItems.Length)];

        if (selected == "何もない")
        {
            Debug.Log("🎲 ランダム結果：何もない → インベントリには追加されません。");
            return;
        }

        else
        {
            // クールダウン中で既に所持していたらスキップ
            if (isCooldown && playerInventory.HasItem(selected))
            {
                Debug.Log($"{selected} はすでに所持中＆クールダウン中 → スキップ");
                return;
            }
            string itemID = selected + "_" + Time.time;
            if (!isCooldown || !playerInventory.HasItem(selected))
            {
                playerInventory.AddItem(selected, itemID);
                Debug.Log($"🎁 ランダムで {selected} をインベントリに追加しました！（ID: {itemID}）");

                // ✅ タグを Untagged に変更
                clickedItem.tag = "Untagged";

                ShowItemUIAndPrefab(selected);
                // ランダム抽選のクールダウン開始
                StartCoroutine(RandomItemCooldown());
            }
        }
    }
    void ShowItemUIAndPrefab(string itemName)
    {

        if (itemCanvas != null)
        {
            itemCanvas.SetActive(true);
            StartCoroutine(HideCanvasAfterSeconds(itemCanvas, 3f));
        }
        // テキスト表示
        itemNameText.text = $"獲得アイテム: {itemName}";
        itemNameText.gameObject.SetActive(true);

        // 🎯アイコン画像の表示（SpriteをImageに割り当て）
        foreach (var entry in itemIcons)
        {
            if (entry.itemName == itemName)
            {
                uiIconImage.sprite = entry.icon;
                uiIconImage.gameObject.SetActive(true); // 表示ON
                break;
            }
        }

        StartCoroutine(HideItemUITextAfterSeconds(3f));
    }
    IEnumerator RandomItemCooldown()
    {
        isRandomItemCooldown = true;
        yield return new WaitForSeconds(randomItemCooldownTime);
        isRandomItemCooldown = false;
        Debug.Log("🎲 ランダムアイテム抽選が再び可能になりました。");
    }

    IEnumerator HideItemUITextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (itemNameText != null)
        {
            itemNameText.gameObject.SetActive(false); // アイテム名テキストを非表示
        }

        if (uiIconImage != null)
        {
            uiIconImage.gameObject.SetActive(false); // アイコン画像を非表示
        }


    }
    IEnumerator HideCanvasAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (Canvas != null)
        {
            Canvas.SetActive(false);
        }
    }
    IEnumerator HideCanvasAfterSeconds(GameObject canvas, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }
    // クールダウン後にフラグを解除するコルーチン
    IEnumerator CooldownAfterAddItem()
    {
        // クールダウン開始前にフラグをセット
        isCooldown = true;
        yield return new WaitForSeconds(3f);  // 3秒のクールダウン
        isCooldown = false;  // クールダウン終了
    }

    //人形を拾うと人形の所持カウントを増やす
    void ExecuteScriptB()
    {
        if (gameManager.Doll <= 5)
        {
            Debug.Log("人形を拾った。");
            gameManager.Doll++;
        }
        else
        {

            Debug.Log("人形はもう持てません。");
        }
    }

    //void ExecuteScriptC()
    //{
    //    int randomChoice = Random.Range(0, 100);
    //    if (randomChoice % 5 == 0)
    //    {
    //        Debug.Log("ジャンプスケア");
    //        cutInImage.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.Log("何もなかった。");
    //        Canvas.SetActive(true);
    //        Text.text = "何もなかった。";
    //    }

    //}
    // ✅ 鍵取得フラグを全部リセット
    // 🔁 全ての鍵のクールダウンをリセットしたいときに使える関数
    public void ResetAllKeyCooldowns()
    {
        keyObtainedTime.Clear();
        Debug.Log("全鍵のクールダウン解除しました！");
    }
    void OtherScript()
    {
        int randomChoice = Random.Range(0, 4);
        if (randomChoice == 0 || randomChoice == 1)
        {
            cutInImage.gameObject.SetActive(true); // 画像を表示
        }
    }
}