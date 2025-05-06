using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInventory : MonoBehaviour
{
   
    public CurseSlider curseSlider;
   // インスペクターで設定可能な初期アイテムリスト
   public List<string> initialItems;

    public TextMeshProUGUI dollText;   // 身代わり人形
    public TextMeshProUGUI potionText; // 一階の鍵
    public TextMeshProUGUI kill2Text; //二階の鍵
    public TextMeshProUGUI tikaText; //地下の鍵
    // 複数所持に対応！Dictionaryで個数を管理
    private Dictionary<string, int> items = new Dictionary<string, int>();

    private bool isCooldown = false;  // アイテム追加のクールダウンフラグ
    private bool isAddingItem = false;  // アイテム追加中かを管理するフラグ
    private float cooldownTime = 3f;  // クールダウン時間（3秒）

//    private Dictionary<string, float> itemAddProbabilities = new Dictionary<string, float>()
//{
//    //{ "呪われた鍵", 0.3f },     // 30% の確率で追加される
//    { "回復薬", 0.7f },     // 50%
//    { "身代わり人形", 0.8f }       // 80%
//};
    // アイテムを追加（クールダウン処理を追加）
    private HashSet<string> persistentItems = new HashSet<string> { "none" };
  
    public void AddItem(string itemName)
    {
        //// 🎲 抽選対象なら確率でスキップ
        //if (itemAddProbabilities.TryGetValue(itemName, out float probability))
        //{
        //    float rand = Random.Range(0f, 1f);
        //    if (rand > probability)
        //    {
        //        Debug.Log($"🚫 {itemName} は確率 {probability * 100}% による抽選失敗（出目：{rand:F2}）");
        //        return;
        //    }
        //    else
        //    {
        //        Debug.Log($"🎯 {itemName} は抽選成功で追加！（出目：{rand:F2}）");
        //    }
        //}

        if (isAddingItem)
        {
            Debug.Log("現在アイテム追加中です。");
            return;  // アイテム追加中は追加をスキップ
        }

        if (isCooldown)
        {
            Debug.Log($"{itemName} はクールダウン中です。");
            return;  // クールダウン中はアイテムを追加できない
        }

        isAddingItem = true;  // アイテム追加中フラグを立てる

        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }

        Debug.Log($"{itemName} をインベントリに追加しました。現在の所持数：{items[itemName]}");

            UpdateItemCountUI(itemName); // ← UI更新を呼ぶ！
        
        // ★ 自動使用処理（回復薬のみ）
        if (itemName == "回復薬")
        {
            bool used = UseItem("回復薬");
            if (used)
            {
                 curseSlider.IncreaseDashPoint(20);
                Debug.Log("20回復した");
            }                
        }
        //アイテム追加後にクールダウン開始
        StartCoroutine(CooldownCoroutine());
    }




    // アイテムを使う（消費）
    // 消えないアイテムのリストを追加

    public bool UseItem(string itemName)
    {
        if (items.ContainsKey(itemName) && items[itemName] > 0)
        {
            // 消えないアイテムかどうかを判定
            if (!persistentItems.Contains(itemName))
            {
                items[itemName]--;
                Debug.Log($"{itemName} を使用しました。残り：{items[itemName]}");

                if (items[itemName] <= 0)
                {
                    items.Remove(itemName);
                }
            }
            else
            {
                Debug.Log($"{itemName} を使用しましたが、インベントリからは削除されません。");
            }

            return true;
        }
        else
        {
            Debug.Log($"{itemName} は所持していません。");
            return false;
        }
    }

    public void UpdateItemCountUI(string itemName)
    {
        if (dollText != null)
            dollText.text = $" {GetItemCount("身代わり人形")}";

        if (potionText != null)
            potionText.text = $" {GetItemCount("一階の鍵")}";

        if (kill2Text != null)
            kill2Text.text = $" {GetItemCount("二階の鍵")}";

        if (tikaText != null)
            tikaText.text = $" {GetItemCount("地下の鍵")}";
        
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

    // クールダウン用コルーチン
    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;  // クールダウン中
        yield return new WaitForSeconds(cooldownTime);  // 指定された時間だけ待機
        isCooldown = false;  // クールダウン終了
        isAddingItem = false;  // アイテム追加フラグを解除
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }
  
    // 初期アイテムを追加するメソッド
    void Start()
    {
       
       
           
        
        foreach (string item in initialItems)
        {
            AddItem(item);
            AddItem("一階の鍵");
        }
    }
}