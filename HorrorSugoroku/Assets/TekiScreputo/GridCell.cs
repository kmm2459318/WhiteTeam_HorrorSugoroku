using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public string cellEffect = "Normal"; // マス目の効果（例: Normal, Bonus, Penalty）

    public void ExecuteEvent()
    {
        switch (cellEffect)
        {
            case "Event":
                Debug.Log($"{name}: イベント発動！");
                Debug.Log("ドアが開きました！"); // ドアが開いたことを示すデバッグログを追加
                break;
            case "Blockl":
                Debug.Log($"{name}: ペナルティ効果発動！");
                break;
            case "Item":
                Debug.Log($"{name}:アイテムを獲得！");
                break;
            case " Direc":
                Debug.Log($"{name}:演出発動！");
                break;
            case "Debuff":
                Debug.Log($"{name}:デバフ効果発動！");
                break;
            case "Battery":
                Debug.Log($"{name}:バッテリーを獲得！");
                break;
            default:
                Debug.Log($"{name}: 通常マス - 効果なし。");
                break;
        }

        Debug.Log("ExecuteEvent");
    }
}