using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Key
{
    public string keyName; // 鍵の名前
    public string keyID;   // 鍵のユニークなID
    public int count = 2;  // 所持数（初期値1）
}

public class PlayerInventory : MonoBehaviour
{
    public CurseSlider curseSlider;

    // インスペクターで設定可能な鍵リスト
    public List<Key> keys = new List<Key>();

    public TextMeshProUGUI dollText;   // 身代わり人形
    public TextMeshProUGUI potionText; // 一階の鍵
    public TextMeshProUGUI kill2Text;  // 二階の鍵
    public TextMeshProUGUI tikaText;   // 地下の鍵

    private Dictionary<string, List<string>> items = new Dictionary<string, List<string>>();

    private bool isCooldown = false;  // アイテム追加のクールダウンフラグ
    private bool isAddingItem = false;  // アイテム追加中かを管理するフラグ
    private float cooldownTime = 3f;  // クールダウン時間（3秒）

    private HashSet<string> persistentItems = new HashSet<string> { "none" };

    public void AddItem(string itemName, string itemID)
    {
        if (isAddingItem)
        {
            Debug.Log("現在アイテム追加中です。");
            return;
        }

        if (isCooldown)
        {
            Debug.Log($"{itemName} はクールダウン中です。");
            return;
        }

        isAddingItem = true;

        // ★ keyName が一致する場合に count を +1
        foreach (var key in keys)
        {
            if (key.keyName == itemName)
            {
                key.count += 1;
                Debug.Log($"🔑 {itemName} の Key.count を増加：{key.count}");
                break; // 同名キーは一つだけと仮定
            }
        }

        if (!items.ContainsKey(itemName))
        {
            items[itemName] = new List<string>();
        }
        items[itemName].Add(itemID);

        Debug.Log($"{itemName} をインベントリに追加しました。現在の所持数：{items[itemName].Count}");

        if (itemName == "回復薬" && curseSlider != null)
        {
            UseItem("回復薬");
            curseSlider.IncreaseDashPoint(20);
            Debug.Log("🧪 回復薬を使用し、呪いゲージを回復しました！");
        }

        UpdateItemCountUI(itemName);
        StartCoroutine(CooldownCoroutine());
    }


    public bool UseItem(string itemName)
    {
        if (items.ContainsKey(itemName) && items[itemName].Count > 0)
        {
            // 最初のIDを削除
            string removedID = items[itemName][0];
            items[itemName].RemoveAt(0);
            Debug.Log($"{itemName} を使用しました（ID: {removedID}）。残り：{items[itemName].Count}");

            // 🔽 keyName が一致する Key の count を -1
            foreach (var key in keys)
            {
                if (key.keyName == itemName)
                {
                    key.count = Mathf.Max(0, key.count - 1); // 負の数にならないように
                    Debug.Log($"🔑 {itemName} の Key.count を減少：{key.count}");
                    break;
                }
            }

            return true;
        }
        else
        {
            Debug.Log($"{itemName} は所持していません。");
            return false;
        }
    }


    public void UpdateItemCountUI(string itemName)
    {
        if (dollText != null)
            dollText.text = $" {GetItemCount("身代わり人形")}";

        if (potionText != null)
            potionText.text = $" {GetItemCount("一階の鍵")}";

        if (kill2Text != null)
            kill2Text.text = $" {GetItemCount("二階の鍵")}";

        if (tikaText != null)
            tikaText.text = $" {GetItemCount("地下の鍵")}";
        
        if (tikaText != null)
            tikaText.text = $" {GetItemCount("食堂の鍵")}";

        if (tikaText != null)
            tikaText.text = $" {GetItemCount("エンジンルームの鍵")}";
    }

    public int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName].Count : 0;
    }

    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;  // クールダウン中
        yield return new WaitForSeconds(cooldownTime);  // 指定された時間だけ待機
        isCooldown = false;  // クールダウン終了
        isAddingItem = false;  // アイテム追加フラグを解除
    }

    public void ShowInventory()
    {
        Debug.Log("=== プレイヤーインベントリ ===");
        foreach (var item in items)
        {
            Debug.Log(item.Key + ": " + item.Value.Count + "個");
        }
    }
    // アイテム所持状況を確認するメソッド
    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName) && items[itemName].Count > 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    void Start()
    {
        foreach (var key in keys)
        {
            for (int i = 0; i < key.count; i++)
            {
                AddItem(key.keyName, key.keyID + "_" + i); // IDをユニークに（重複防止）
            }
        }
    }
}