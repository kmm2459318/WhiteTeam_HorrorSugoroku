using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> inventory = new List<string>(); // インベントリ（アイテムリスト）
    private bool itemAdded = false; // アイテム追加が一度だけ発動するフラグ
    private float itemAddCooldown = 2f; // 再度アイテムを追加できるまでの待機時間（秒）
    public void AddItem(string itemName)
    {
        // すでにアイテムが追加されている場合は追加しない
        if (itemAdded)
        {
            Debug.LogWarning("アイテムは既に追加されています。一度だけ追加できます。");
            return;
        }

        // アイテムがインベントリに存在しない場合のみ追加
        if (!inventory.Contains(itemName))
        {
            inventory.Add(itemName); // アイテムを追加
            Debug.Log($"{itemName} をインベントリに追加しました。現在のアイテム数: {inventory.Count}");
            itemAdded = true; // アイテム追加フラグを立てる

            // アイテム追加後にコルーチンで数秒後にフラグをリセット
            StartCoroutine(ResetItemAddCooldown());
        }
        else
        {
            Debug.LogWarning($"{itemName} は既にインベントリにあります！");
        }
    }

    // アイテムをインベントリから削除する
    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log($"{itemName} をインベントリから削除しました！");
            itemAdded = false; // アイテム削除時にフラグをリセット（再度アイテム追加可能）
        }
        else
        {
            Debug.Log($"{itemName} はインベントリにありません！");
        }
    }

    // アイテムを追加した後、一定時間後にフラグをリセットするコルーチン
    private IEnumerator ResetItemAddCooldown()
    {
        // 一定時間待機
        yield return new WaitForSeconds(itemAddCooldown);

        // フラグをリセット
        itemAdded = false;
        Debug.Log("アイテム追加のクールダウンが終了しました。再度アイテムを追加できます。");
    }

    // インベントリに指定したアイテムが含まれているかを確認する
    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    // 現在のインベントリの内容を表示する
    public void ShowInventory()
    {
        Debug.Log("現在のインベントリ: " + string.Join(", ", inventory));
    }
}