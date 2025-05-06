using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Key
{
    public string keyName; // 鍵の名前
    public string keyID;   // 鍵のユニークなID
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
        //// 🎲 抽選対象なら確率でスキップ
        //if (itemAddProbabilities.TryGetValue(itemName, out float probability))
        //{
        //    float rand = Random.Range(0f, 1f);
        //    if (rand > probability)
        //    {
        //        Debug.Log($"🚫 {itemName} は確率 {probability * 100}% による抽選失敗（出目：{rand:F2}）");
        //        return;
        //    }
        //    else
        //    {
        //        Debug.Log($"🎯 {itemName} は抽選成功で追加！（出目：{rand:F2}）");
        //    }
        //}

        if (isAddingItem)
        {
            Debug.Log("現在アイテム追加中です。");
            return;  // アイテム追加中は追加をスキップ
        }

        if (isCooldown)
        {
            Debug.Log($"{itemName} はクールダウン中です。");
            return;  // クールダウン中はアイテムを追加できない
        }

        isAddingItem = true;  // アイテム追加中フラグを立てる

        if (!items.ContainsKey(itemName))
        {
            items[itemName] = new List<string>();
        }
        items[itemName].Add(itemID);

        Debug.Log($"{itemName} をインベントリに追加しました。現在の所持数：{items[itemName].Count}");

        UpdateItemCountUI(itemName); // ← UI更新を呼ぶ！

        // アイテム追加後にクールダウン開始
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

            // リストが空になったらエントリを削除
            if (items[itemName].Count == 0)
            {
                items.Remove(itemName);
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
        // インスペクターで設定された鍵をインベントリに追加
        foreach (var key in keys)
        {
            AddItem(key.keyName, key.keyID);
        }
    }
}