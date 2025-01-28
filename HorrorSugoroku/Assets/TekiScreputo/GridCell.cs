using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public string cellEffect = "Normal"; // マス目の効果（例: Normal, Bonus, Penalty）

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
                break;
            case "Battery":
                Debug.Log($"{name}:バッテリーを獲得！");
                Debug.Log("バッテリーが回復しました");
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
}