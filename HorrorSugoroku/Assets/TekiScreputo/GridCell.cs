using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    public string cellEffect = "Normal"; // マス目の効果（例: Normal, Bonus, Penalty）
    public FlashlightController flashlightController;
    [SerializeField] private Master_Debuff DebuffSheet;
    public GameObject eventPanel; // UIのパネル
    public TextMeshProUGUI eventText; // UIのテキスト
    public Button closeButton; // UIを閉じるボタン
    public ItemPickup item;
    public string requiredItem = "鍵"; // 必要なアイテム
 


    public int n = 0;
  private PlayerInventory playerInventory;
    void Start()
    {  
        playerInventory = FindObjectOfType<PlayerInventory>();
        if (eventPanel != null)
        {
            eventPanel.SetActive(false);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseEventUI);
        }
     
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
        switch (cellEffect)
        {
            case "Event":
                
                DisplayRandomEvent();
                break;
            case "Blockl":
                Debug.Log($"{name}: ペナルティ効果発動！");
                break;
            case "Item":
                if (cellEffect == "Item" && playerInventory != null)
                {
                    Debug.Log($"{name}:アイテムを獲得！");
                    playerInventory.AddItem("鍵"); // 鍵を追加
                }
                break;
            case "Dires":
                Debug.Log($"{name}:演出発動！");
                break;
            case "Debuff":
                Debug.Log($"{name}:デバフ効果発動！");
                DeBuh();
                break;
            case "Door":
               
                break;
            case "Battery":
                Debug.Log($"{name}:バッテリーを獲得！");
                break;
            default:
                Debug.Log($"{name}: 通常マス - 効果なし。");
                break;
        }
    }
    void ShowEventUI(string message, float   delay = 1.0f)
    {
        StartCoroutine(DelayedShowEventUI(message,  delay));
    }
    IEnumerator DelayedShowEventUI(string message,float delay)
    {
        yield return new WaitForSeconds(delay);
        if (eventPanel != null && eventText != null)
        {
            eventText.text = message;
            eventPanel.SetActive(true);
            Time.timeScale = 0; // **ゲームを停止**
        }
    }

    void CloseEventUI()
    {
        if (eventPanel != null)
        {
            eventPanel.SetActive(false);
            Time.timeScale = 1; // **ゲームを再開**
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

        string selectedEvent = eventMessages[randomIndex];
        Debug.Log($"{name}: イベント発動！ {selectedEvent}");

        ExecuteSelectedEvent(selectedEvent);
    }

    private void ExecuteSelectedEvent(string eventMessage)
    {
        switch (eventMessage)
        {
            case "ドアが開きました！":
                Debug.Log("ドアが開くイベントを実行します。");
                ShowEventUI("The door opened"); // UIに表示
                OpenDoor();
                break;
            case "クローゼットに隠れられる":
                Debug.Log("クローゼットに隠れるイベントを実行します。");
                ShowEventUI("クローゼットに隠れられる"); // UIに表示
                SecretCloset();
                break;
            case "急に眠気がおそってきた。":
                Debug.Log("眠気イベントを実行します。");
                ShowEventUI("急に眠気がおそってきた。"); // UIに表示
                SleepEvent();
                break;
            default:
                Debug.Log("未知のイベントです。");
                ShowEventUI("未知のイベント"); // UIに表示
                break;
        }
    }

 

    public void OpenDoor()
    {
        Debug.Log("ドアが開くイベントを実行します。");
        // ドアが開く処理をここに追加
    }

    public void SecretCloset()
    {
        Debug.Log("クローゼットに隠れるイベントを実行します。");
        // クローゼットに隠れる処理をここに追加
        SceneChanger3D.hasSubstituteDoll = true; // 使用判定をトゥルーに設定
    }

    public void SleepEvent()
    {
        Debug.Log("眠気イベントを実行します。");
        // 眠気の処理をここに追加
    }

    public void LogCellArrival()
    {
        Debug.Log($"プレイヤーが {name} に到達しました。現在の位置: {transform.position}");
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

}