using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public KeyRandomizer keyRandomizer;

    [System.Serializable]
    public class ItemPrefabData
    {
        public string itemName;
        public GameObject prefab;
    }

    public List<ItemPrefabData> itemPrefabs;
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

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

        // プレハブ辞書を作成
        foreach (var item in itemPrefabs)
        {
            if (!prefabDict.ContainsKey(item.itemName))
            {
                prefabDict[item.itemName] = item.prefab;
            }
        }
        // アイテム割り当てリスト取得
        List<string> itemList = keyRandomizer.GetGeneratedItems();

        // シーン内の "Item" タグオブジェクトを取得
        GameObject[] keyObjects = GameObject.FindGameObjectsWithTag("Items");

        if (itemList.Count < keyObjects.Length)
        {
            Debug.LogWarning("アイテムの数よりオブジェクトの方が多いです！");
        }
        // 既存のオブジェクトをPrefabで置き換える
        for (int i = 0; i < keyObjects.Length && i < itemList.Count; i++)
        {
            string itemName = itemList[i];
            GameObject original = keyObjects[i];

            if (prefabDict.ContainsKey(itemName))
            {
                GameObject prefab = prefabDict[itemName];

                // 同じ位置に置き換え
                GameObject newItem = Instantiate(prefab, original.transform.position, original.transform.rotation);
                newItem.name = itemName;

                Destroy(original); // 元のオブジェクトを削除
                Debug.Log($"{itemName} を配置しました！");
            }
            else
            {
                Debug.LogWarning($"⚠ {itemName} に対応するPrefabが見つかりません！");
            }
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