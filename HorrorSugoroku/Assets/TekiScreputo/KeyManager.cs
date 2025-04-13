using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public KeyRandomizer keyRandomizer;

    void Start()
    {
        if (keyRandomizer == null)
        {
            keyRandomizer = FindObjectOfType<KeyRandomizer>();
        }

        if (keyRandomizer == null)
        {
            Debug.LogError("KeyRandomizer が見つかりません！");
            return;
        }

        // アイテム割り当てリスト取得
        List<string> itemList = keyRandomizer.GetGeneratedItems();

        // シーン内の "Item" タグオブジェクトを取得
        GameObject[] keyObjects = GameObject.FindGameObjectsWithTag("Item");

        if (itemList.Count < keyObjects.Length)
        {
            Debug.LogWarning("アイテムの数よりオブジェクトの方が多いです！");
        }

        // 順番に割り当て
        for (int i = 0; i < keyObjects.Length && i < itemList.Count; i++)
        {
            GameObject keyObject = keyObjects[i];
            string itemName = itemList[i];

            keyObject.name = itemName;

            Debug.Log($"{keyObject.name} に名前を設定しました。");
        }
    }
}