using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();
    private bool isUsingItem = false; // アイテム使用中かどうかのフラグ

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
        if (isUsingItem)
        {
            Debug.Log("現在他のアイテムを使用中です");
            return false;
        }

        Debug.Log("UseItemメソッドが呼び出されました: " + itemName);
        if (items.ContainsKey(itemName) && items[itemName] > 0)
        {
            isUsingItem = true; // アイテム使用中フラグを設定
            items[itemName]--;
            Debug.Log(itemName + " を使用しました。残り：" + items[itemName]);

            if (items[itemName] <= 0)
            {
                items.Remove(itemName);
                Debug.Log(itemName + " がインベントリから削除されました。");
            }

            // アイテム使用後にフラグをリセット
            StartCoroutine(ResetItemUsageFlag());

            return true;
        }
        else
        {
            Debug.Log(itemName + " は所持していません。");
            return false;
        }
    }

    // アイテム使用中フラグをリセットするコルーチン
    private IEnumerator ResetItemUsageFlag()
    {
        yield return new WaitForSeconds(1f); // 1秒後にフラグをリセット
        isUsingItem = false;
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

    // 初期アイテムを追加するメソッド
    void Start()
    {
        AddItem("二階のカギ");
        AddItem("一階のカギ");
        AddItem("食堂のカギ");
        AddItem("ホールのカギ");
        AddItem("医務室のカギ");
        AddItem("ベッドルームのカギ");
        AddItem("地下室のカギ");
        AddItem("地下室のカギ１");
        AddItem("地下室のカギ２");
        AddItem("地下室のカギ３");
        AddItem("エンジンルームのカギ");
    }
}