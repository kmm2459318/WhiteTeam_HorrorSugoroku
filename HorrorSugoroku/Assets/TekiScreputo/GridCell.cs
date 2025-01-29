using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public string cellEffect = "Normal"; // マス目の効果（例: Normal, Bonus, Penalty）
    public FlashlightController flashlightController;
    [SerializeField] private Master_Debuff DebuffSheet;


      public int n = 0;
    void Start()
    {

        Debug.Log("ID:" + DebuffSheet.DebuffSheet[n].ID);
        Debug.Log("イベント名:" + DebuffSheet.DebuffSheet[n].Name);
        Debug.Log("懐中電灯の最小ゲージ減少量:" + DebuffSheet.DebuffSheet[n].DecreaseMin);
        Debug.Log("懐中電灯の最大ゲージ減少量:" + DebuffSheet.DebuffSheet[n].DecreaseMax);
        Debug.Log("アイテムを付与するかの判定:" + DebuffSheet.DebuffSheet[n].ItemGive);
        Debug.Log("アイテムが使えなくなるかの判定:" + DebuffSheet.DebuffSheet[n].ItemGive);
        Debug.Log("アイテムが使えないターン数:" + DebuffSheet.DebuffSheet[n].ItemGive);
    }
    public void ExecuteEvent()
    {
        // マス目の効果を発動
        switch (cellEffect)
        {
            case "Event":
                DisplayRandomEvent();
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

    private void DisplayRandomEvent()
    {
        string[] eventMessages = {
            "ドアが開きました！",
            "クローゼットに隠れられる",
            "急に眠気がおそってきた。"
        };

        System.Random random = new System.Random();
        int randomIndex = random.Next(eventMessages.Length);

        Debug.Log($"{name}: イベント発動！ {eventMessages[randomIndex]}");
    }

    public void LogCellArrival()
    {
        Debug.Log($"プレイヤーが {name} に到達しました。");
    }
    void DeBuh()
    {
       
        int randomEvent = Random.Range(0, 2);
       
        if (randomEvent == 0)
        {
          
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
