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

    [SerializeField] public TextMeshProUGUI Text;
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

    // ClickObject.cs に追加
    private Dictionary<string, float> itemAddCooldowns = new Dictionary<string, float>();
    public float itemCooldownDuration = 5f; // ← クールダウン時間（秒）

    [SerializeField] private float keyCooldownTime = 2f;  // 例: 10秒で再取得可能
    private bool hasClicked = false; // クリック多重防止用フラグ

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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClicked)
        {
            hasClicked = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Key") || hit.collider.CompareTag("Doll"))
                {
                    if (!playerSaikoro.idoutyu)
                    {
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);
                        if (distance <= 3f)
                        {
                            /*if (hit.collider.CompareTag("Key"))
                            {
                                ExecuteScriptA(hit.collider.gameObject);
                            }
                            else if (hit.collider.CompareTag("Map"))
                            {
                                ExecuteScriptB();
                            }
                            else if (hit.collider.CompareTag("Item"))
                            {
                                ExecuteScriptC(hit.collider.gameObject);
                            }*/
                            // 🎲 ランダムでスクリプトA または B を実行
                            // int randomChoice = Random.Range(0, 4);
                            if (Input.GetMouseButtonDown(0))
                            { // 左クリック
                                if (hit.collider.CompareTag("Key"))
                                {
                                    ExecuteScriptA(hit.collider.gameObject); // スクリプトAを実行（アイテム取得）
                                    Destroy(hit.collider.gameObject);
                                }
                                else if (hit.collider.CompareTag("Map"))
                                {
                                    ExecuteScriptB(); // スクリプトBを実行（例：敵を召喚）
                                }
                                else if (hit.collider.CompareTag("Item"))
                                {
                                    if (!curse.curse1_3)
                                    {
                                        ExecuteScriptC(hit.collider.gameObject); // スクリプトBを実行（例：敵を召喚）
                                        Destroy(hit.collider.gameObject);
                                        curse.curse1Turn--;
                                    }
                                }
                                else if (hit.collider.CompareTag("Other"))
                                {

                                }
                                //Destroy(hit.collider.gameObject);
                                
                            }
                            else if (hit.collider.CompareTag("Map"))
                            {
                                ExecuteScriptA(clicked); // スクリプトAを実行（アイテム取得）
                            }
                            else if (clicked.CompareTag("Doll"))
                            {
                                ExecuteScriptB(); // スクリプトBを実行（人形を拾う処理）
                            }
                            else if (clicked.CompareTag("Item"))
                            {
                                ExecuteScriptC();// スクリプトCを実行（例：敵を召喚）
                            }

                            //Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }

            StartCoroutine(ResetClick()); // フラグリセットコルーチン呼び出し
        }

        if (Canvas.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            Canvas.SetActive(false);
        }
       
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
            playerInventory.AddItem(keyName);
            Debug.Log($"{keyName} をインベントリに追加しました。");

            // クールダウン後にフラグを解除
            StartCoroutine(CooldownAfterAddItem());
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

    void ExecuteScriptC()
    {
        int randomChoice = Random.Range(0, 100);
        if (randomChoice % 5 == 0)
        {
            Debug.Log("ジャンプスケア");
            cutInImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("何もなかった。");
            Canvas.SetActive(true);
            Text.text = "何もなかった。";
        }

    }
    // ✅ 鍵取得フラグを全部リセット
    // 🔁 全ての鍵のクールダウンをリセットしたいときに使える関数
    public void ResetAllKeyCooldowns()
    {
        keyObtainedTime.Clear();
        Debug.Log("全鍵のクールダウン解除しました！");
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
