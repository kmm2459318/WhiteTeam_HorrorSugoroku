using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    public string cellEffect = "Normal"; // マス目の効果（例: Normal, Bonus, Penalty）
    [SerializeField] private Master_Debuff DebuffSheet;
    public GameObject eventPanel; // UIのパネル
    public TextMeshProUGUI eventText; // UIのテキスト
    public GameObject cursePanel; // UIのパネル
    public TextMeshProUGUI curseText; // UIのテキスト
    public Button closeButton; // UIを閉じるボタン
    public ItemPickup item;
    public string requiredItem = "鍵"; // 必要なアイテム
    private CurseSlider curseSlider;                                // public int gridCellIncreaseAmount = 20; // GridCell 側の呪いゲージ増加量
    [SerializeField] private int curseChance = 50;  // 呪いの発生確率（％）
    [SerializeField] private int scareChance = 30;  // 驚かしイベントの発生確率（％）
    [SerializeField] private int nothingChance = 20; // 何も起こらない確率（％）

    [SerializeField] private int curseamout = 10;//呪いの増加量の調整

    [SerializeField] private Image cutInImage; // カットイン画像
    [SerializeField] private float cutInDuration = 2.0f; // カットインの表示時間（秒）
    [SerializeField] private AudioClip gameOverSound; // ゲームオーバー時のサウンド
    private AudioSource audioSource; // 音声再生用のAudioSource

    [SerializeField] private float volume = 1.0f; // 音量 (デフォルトは最大)

    private bool isGameOver = false;    // 重複処理防止用フラグ

    public int n = 0;
    private PlayerInventory playerInventory;
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        curseSlider = FindObjectOfType<CurseSlider>(); // 呪いゲージを取得
        if (cursePanel != null)
        {
            cursePanel.SetActive(false);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseEventUI);
        }
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
    //void Update()
    //{
    //    // UI が表示されているときに スペースキーで閉じる
    //    if (eventPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        CloseEventUI();
    //    }
    //}
    void Update()
    {
        // UI が表示されているときに スペースキーまたは G キーで閉じる
        if (( cursePanel.activeSelf) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.H)))
        {
            CloseEventUI();
        }
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
                //if (cellEffect == "Item" && playerInventory != null)
                //{
                //    Debug.Log($"{name}:アイテムを獲得！");
                //    playerInventory.AddItem("鍵"); // 鍵を追加
                //}
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
            case "Curse":
              //  Debug.Log($"{name}: 呪いゲージが増えた。");
                Debug.Log($"{name}: 呪いマスに到達。ランダムイベントを発動します。");
                ExecuteCurseEvent();

                break;

            default:
                Debug.Log($"{name}: 通常マス - 効果なし。");
                break;
        }
    }
    void ShowEventUI(string message, float delay = 1.0f)
    {
        StartCoroutine(DelayedShowEventUI(message, delay));
    }
    IEnumerator DelayedShowEventUI(string message, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (eventPanel != null && eventText != null)
        {

            eventText.text = message;
            eventPanel.SetActive(true);
            Time.timeScale = 0; // **ゲームを停止**
        }
    }
    void ShowCurseUI(string message, float delay = 1.0f)
    {
        StartCoroutine(DelayedShowCurseUI(message, delay));
    }
    IEnumerator DelayedShowCurseUI(string message, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (cursePanel != null && curseText != null)
        {
            curseText.text = message;
            cursePanel.SetActive(true);
            Time.timeScale = 0; // **ゲームを一時停止**
        }
    }
    
    void CloseEventUI()
    {
        if (eventPanel != null)
        {
            eventPanel.SetActive(false);
        }
        if (cursePanel != null)
        {
            cursePanel.SetActive(false);
        }
        Time.timeScale = 1; // **ゲームを再開**
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

    private void ExecuteCurseEvent()
    {
        int randomValue = Random.Range(1, 101); // 1〜100の乱数を生成

        if (randomValue <= curseChance)
        {
            // **呪い発動**
            Debug.Log($"{name}: 呪いが発動！");
            curseSlider.IncreaseDashPoint(curseamout); // 呪いゲージ増加
            ShowCurseUI("呪いが発動した！");
        }
        else if (randomValue <= curseChance + scareChance)
        {
            // **驚かしイベント発動**
            Debug.Log($"{name}: 驚かしイベントが発生！");
            StartCoroutine(TriggerScareEffect());
        }
        else
        {
            // **何も起こらない**
            Debug.Log($"{name}: 何も起こらなかった…");
            ShowEventUI("…何も起こらなかった");
        }
    }
    private IEnumerator TriggerScareEffect()
    {
        isGameOver = true; // 重複処理防止用フラグ

        // 他のUI要素（テキストなど）を非表示にする
       // HideAllUI(); // UI非表示処理を実行

        // カットイン画像を表示
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(true); // 画像を表示
        }

        // ゲームオーバーサウンドを再生
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.clip = gameOverSound; // サウンドを設定
            audioSource.Play(); // 音を鳴らす
        }

        // 指定された時間だけ待機
        yield return new WaitForSeconds(cutInDuration);

        // カットイン画像を非表示にする
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(false); // 画像を非表示
        }
    }
        void DeBuh()
    {
        int randomEvent = Random.Range(0, 2);

        Debug.Log("デバフイベントB：アイテムが使えなくなった");
    }

}