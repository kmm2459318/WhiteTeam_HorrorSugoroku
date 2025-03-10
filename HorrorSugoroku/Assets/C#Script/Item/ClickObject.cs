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

    [SerializeField] public TextMeshProUGUI Text;
    [SerializeField] public GameObject Canvas;
    [SerializeField] private Image cutInImage;

    // 名前ごとに「取得済みの時間」を記録する辞書
    private Dictionary<string, float> keyObtainedTime = new Dictionary<string, float>();
    private HashSet<string> obtainedKeys = new HashSet<string>();

    // 鍵取得制限時間（秒）
    [SerializeField] private float keyCooldownTime = 10f;  // 例: 10秒で再取得可能
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
                if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Key") || hit.collider.CompareTag("Map"))
                {
                    if (!playerSaikoro.idoutyu)
                    {
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);
                        if (distance <= 3f)
                        {
                            if (hit.collider.CompareTag("Key"))
                            {
                                ExecuteScriptA(hit.collider.gameObject);
                            }
                            else if (hit.collider.CompareTag("Map"))
                            {
                                ExecuteScriptB();
                            }
                            else if (hit.collider.CompareTag("Item"))
                            {
                                ExecuteScriptC();
                            }

                            Destroy(hit.collider.gameObject);
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

        // 取得済みフラグが存在 & クールダウン時間がまだ経っていないならスキップ
        if (keyObtainedTime.ContainsKey(keyName))
        {
            float elapsedTime = Time.time - keyObtainedTime[keyName];
            if (elapsedTime < keyCooldownTime)
            {
                Debug.Log($"{keyName} はまだクールダウン中です（残り {keyCooldownTime - elapsedTime:F1} 秒）");
                return;
            }
            if (obtainedKeys.Contains(keyName))
            {
                Debug.Log($"{keyName} はすでに取得済み（取得スキップ）");
                return;
            }
        }

        // 取得処理
       
        keyObtainedTime[keyName] = Time.time;  // 今の時間を記録
        obtainedKeys.Add(keyName); // フラグに追加
        playerInventory.AddItem(keyName);
        Debug.Log($"{keyName} を入手しました！");
    }


    void ExecuteScriptB()
    {
        Debug.Log("地図のかけらを獲得！");
        if (gameManager != null)
        {
            gameManager.MpPlus();
        }
        else
        {
            Debug.LogError("GameManager が見つかりません！");
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
  
}