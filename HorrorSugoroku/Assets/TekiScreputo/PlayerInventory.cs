using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> items = new List<string>(); // インベントリ（アイテムリスト）

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"{itemName} をインベントリに追加しました！");
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
            Debug.Log($"{itemName} をインベントリから削除しました！");
        }
        else
        {
            Debug.Log($"{itemName} はインベントリにありません！");
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void ShowInventory()
    {
        Debug.Log("現在のインベントリ: " + string.Join(", ", items));
    }
}
