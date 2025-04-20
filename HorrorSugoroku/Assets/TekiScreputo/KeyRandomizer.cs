using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRandomizer : MonoBehaviour
{
    public int totalItemCount = 10;           // 全体の割り振り数
    public int fixedFirstFloorKeyCount = 3;   // 一階の鍵の数
    public Transform spawnParent;
    public List<Transform> spawnPoints;

    private List<string> otherItems = new List<string> { "身代わり人形", "回復薬" };
    private List<string> generatedItems = new List<string>();

    //[System.Serializable]
    //public class ItemPrefabData
    //{
    //    public string itemName;
    //    public GameObject prefab;
    //}
    //public List<ItemPrefabData> itemPrefabs;  // Inspector でセット！
    //private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    //public KeyRandomizer randomizer;

    //void Start()
    //{
    //    // 辞書に変換（見つけやすくする）
    //    foreach (var item in itemPrefabs)
    //    {
    //        prefabDict[item.itemName] = item.prefab;
    //    }

    //    SpawnItems();
    //}
    //void SpawnItems()
    //{
    //    List<string> generatedItems = randomizer.GetGeneratedItems();

    //    for (int i = 0; i < generatedItems.Count && i < spawnPoints.Count; i++)
    //    {
    //        string itemName = generatedItems[i];

    //        if (prefabDict.ContainsKey(itemName))
    //        {
    //            GameObject prefab = prefabDict[itemName];
    //            Transform spawnPoint = spawnPoints[i];

    //            GameObject item = Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnParent);
    //            item.name = itemName;

    //            //// 名前表示やアイテム情報スクリプトがあれば設定
    //            //var info = item.GetComponent<ItemInfo>();
    //            //if (info != null)
    //            //{
    //            //    info.itemName = itemName;

    //        }
    //        else
    //        {
    //            Debug.LogWarning($"⚠ {itemName} に対応するPrefabがありません！");
    //        }
    //    }
    //}

    void Awake()
    {
        GenerateItemList();
    }

    public void GenerateItemList()
    {
        generatedItems.Clear();

        // 一階の鍵を確実に追加
        for (int i = 0; i < fixedFirstFloorKeyCount; i++)
        {
            generatedItems.Add("一階の鍵");
        }

        // 残り枠に他アイテムをランダムに追加
        int remaining = totalItemCount - fixedFirstFloorKeyCount;
        for (int i = 0; i < remaining; i++)
        {
            int rand = Random.Range(0, otherItems.Count);
            generatedItems.Add(otherItems[rand]);
        }

        Shuffle(generatedItems);
    }

    // 外部用：生成済みのリストを渡す
    public List<string> GetGeneratedItems()
    {
        return new List<string>(generatedItems);
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}