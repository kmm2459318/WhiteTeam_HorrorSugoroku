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
        List<string> itemList = keyRandomizer.GetGeneratedItems();

        GameObject[] firstFloorObjects = GameObject.FindGameObjectsWithTag("Items");
        GameObject[] secondFloorObjects = GameObject.FindGameObjectsWithTag("Items2");

        int firstIndex = 0;
        int secondIndex = 0;

        foreach (string itemName in itemList)
        {
            if (itemName.Contains("一階の鍵") && firstIndex < firstFloorObjects.Length)
            {
                GameObject obj = firstFloorObjects[firstIndex++];
                obj.name = itemName;
                obj.tag = "Key";
                Debug.Log($"一階の鍵「{itemName}」を {obj.name} に設定");
            }
            else if (itemName.Contains("二階の鍵") && secondIndex < secondFloorObjects.Length)
            {
                GameObject obj = secondFloorObjects[secondIndex++];
                obj.name = itemName;
                obj.tag = "Key";
                Debug.Log($"二階の鍵「{itemName}」を {obj.name} に設定");
            }
            else
            {
                Debug.LogWarning($"割り当て対象が見つからない、または枠が足りない: {itemName}");
            }
        }
    }
}