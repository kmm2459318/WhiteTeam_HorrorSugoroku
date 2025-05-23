﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRandomizer : MonoBehaviour
{
    public int totalItemCount = 7;           // 全体の割り振り数
    public int fixedFirstFloorKeyCount = 7;   // 一階の鍵の数
    public Transform spawnParent;
    public List<Transform> spawnPoints;

   private List<string> otherItems = new List<string> { "二階の鍵" };
    private List<string> generatedItems = new List<string>();

    

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