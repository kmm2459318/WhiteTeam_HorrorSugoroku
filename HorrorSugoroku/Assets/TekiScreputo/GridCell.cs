using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
   // public PlayerMover playerMover;
    public string cellEffect = "Normal"; // マス目の効果（例: Normal, Bonus, Penalty）
    public FlashlightController flashlightController;

    //void OnTriggerEnter(Collider other)
    //{

    //    //タグが"Player"のオブジェクトのみ反応
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log($"{name} にプレイヤーが到達しました。マスの種類：{cellEffect}");

    //        ExecuteEvent();
          
    //    } 
    //}
    public void ExecuteEvent()
    {
       
        // マス目の効果を発動
        switch (cellEffect)
        {
            case "Event":
                Debug.Log($"{name}: イベント発動！");
                break;
            case "Blockl":
                Debug.Log($"{name}: ペナルティ効果発動！");
                break;
            case "Item":
                Debug.Log($"{name}:アイテムを獲得！");
                break;
            case "Dires":
                Debug.Log($"{name}:演出発動！");
                break;
            case "Debuff":
                Debug.Log($"{name}:デバフ効果発動！");
                DeBuh();
                break;
            case "Battery":
                Debug.Log($"{name}:バッテリーを獲得！");
                Batre();
                break;
            default:
                Debug.Log($"{name}: 通常マス - 効果なし。");

                break;
          
        }

         
    }
    void DeBuh()
    {
        int randomEvent = Random.Range(0, 2);

        if (randomEvent == 0)
        {
            Debug.Log("デバフイベントA：　電池のゲージが減った！");

            flashlightController.OnTurnAdvanced();
        }
        else
        {
            Debug.Log("デバフイベントB：アイテムが使えなくなった");
        }
    }
    void Batre()
    {
       
            Debug.Log("バッテリー回復：バッテリーが回復した");

            flashlightController.AddBattery(20f);
        
    }

}
