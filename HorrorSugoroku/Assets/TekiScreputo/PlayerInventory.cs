using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // インスペクターで設定可能な初期アイテムリスト
    public List<string> initialItems;

    // 複数所持に対応！Dictionaryで個数を管理
    private Dictionary<string, int> items = new Dictionary<string, int>();

    private bool isCooldown = false;  // アイテム追加のクールダウンフラグ
    private bool isAddingItem = false;  // アイテム追加中かを管理するフラグ
    private float cooldownTime = 3f;  // クールダウン時間（3秒）

    // アイテムを追加（クールダウン処理を追加）
    public void AddItem(string itemName)
    {
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

        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }

        Debug.Log($"{itemName} をインベントリに追加しました。現在の所持数：{items[itemName]}");

        // アイテム追加後にクールダウン開始
        StartCoroutine(CooldownCoroutine());
    }

    // アイテムを使う（消費）
    public bool UseItem(string itemName)
    {
        if (items.ContainsKey(itemName) && items[itemName] > 0)
        {
            items[itemName]--;
            Debug.Log($"{itemName} を使用しました。残り：{items[itemName]}");

            if (items[itemName] <= 0)
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

    // 所持しているか確認
    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName) && items[itemName] > 0;
    }

    // 所持数を取得
    public int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }

    // クールダウン用コルーチン
    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;  // クールダウン中
        yield return new WaitForSeconds(cooldownTime);  // 指定された時間だけ待機
        isCooldown = false;  // クールダウン終了
        isAddingItem = false;  // アイテム追加フラグを解除
    }

    // 全アイテム表示（デバッグ用）
    public void ShowInventory()
    {
        Debug.Log("=== プレイヤーインベントリ ===");
        foreach (var item in items)
        {
            Debug.Log(item.Key + ": " + item.Value + "個");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    // 初期アイテムを追加するメソッド
    void Start()
    {
        foreach (string item in initialItems)
        {
            AddItem(item);
            AddItem("一階のカギ");
        }
    }
}