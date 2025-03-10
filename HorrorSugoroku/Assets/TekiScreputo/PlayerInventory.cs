using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // ← 複数所持に対応！Dictionaryで個数を管理
    private Dictionary<string, int> items = new Dictionary<string, int>();

    // アイテムを追加
    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }

        Debug.Log(itemName + " を入手しました。現在の所持数：" + items[itemName]);
    }

    // アイテムを使う（消費）
    public bool UseItem(string itemName)
    {
        if (items.ContainsKey(itemName) && items[itemName] > 0)
        {
            items[itemName]--;
            Debug.Log(itemName + " を使用しました。残り：" + items[itemName]);

            if (items[itemName] <= 0)
            {
                items.Remove(itemName);
            }
            return true;
        }
        else
        {
            Debug.Log(itemName + " は所持していません。");
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

    // 全アイテム表示（デバッグ用）
    public void ShowInventory()
    {
        Debug.Log("=== プレイヤーインベントリ ===");
        foreach (var item in items)
        {
            Debug.Log(item.Key + ": " + item.Value + "個");
        }
    }
   
}