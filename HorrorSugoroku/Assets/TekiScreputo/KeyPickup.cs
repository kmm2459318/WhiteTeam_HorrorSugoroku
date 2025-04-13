
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab; // 鍵のプレハブ（Inspectorで指定）
    [SerializeField] private List<Transform> spawnPoints; // 鍵の出現ポイント（空のオブジェクトを登録）

    // 鍵の名前リスト
    private List<string> keyNames = new List<string>
    {
        "食堂の鍵", "地下の鍵", "ホールの鍵", "ベットルームの鍵", "医務室の鍵"
    };

    void Start()
    {
        SpawnKeys();
    }

    void SpawnKeys()
    {
        if (spawnPoints.Count < keyNames.Count)
        {
            Debug.LogError("出現ポイントが足りません。");
            return;
        }

        // 出現ポイントをコピーしてシャッフル
        List<Transform> availablePoints = new List<Transform>(spawnPoints);
        ShuffleList(availablePoints);

        // 鍵をランダムに出現ポイントへ生成
        for (int i = 0; i < keyNames.Count; i++)
        {
            Vector3 position = availablePoints[i].position;
            GameObject key = Instantiate(keyPrefab, position, Quaternion.identity);
            key.name = keyNames[i];

            // 鍵に名前を渡す（KeyPickupスクリプトなどがある場合）
            //KeyPickup pickup = key.GetComponent<KeyPickup>();
            //if (pickup != null)
            //{
            //    pickup.keyName = keyNames[i];
            //}
        }
    }

    // シャッフル関数
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
