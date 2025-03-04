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
    //public GameObject eventPanel; // UIのパネル
    //public TextMeshProUGUI eventText; // UIのテキスト
    public GameObject cursePanel; // UIのパネル
    public TextMeshProUGUI curseText; // UIのテキスト
    public GameObject itemPanel; // UIのパネル
    public TextMeshProUGUI itemText; // UIのテキスト
                                     //   public TMP_Text itemLogText;
                                     // public Button closeButton; // UIを閉じるボタン
    //public ItemPickup item;
    public string requiredItem = "鍵"; // 必要なアイテム
    private CurseSlider curseSlider;                                // public int gridCellIncreaseAmount = 20; // GridCell 側の呪いゲージ増加量
                                                                    // [SerializeField] private int curseChance = 50;  // 呪いの発生確率（％）
    [SerializeField] private int scareChance = 30;  // 驚かしイベントの発生確率（％）
    [SerializeField] private int nothingChance = 20; // 何も起こらない確率（％）
                                                     // [SerializeField] private int hiruChance = 50;  // 呪いの回復確率（％）

    [SerializeField] private int curseamout = 10;//呪いの増加量の調整
    [SerializeField] private int hirueamout = 10;//呪いの回復量の調整
    public Image cutInImage; // カットイン画像
    private Sprite loadedSprite;
    public AudioSource audioSource; // 音声
                                    // private AudioClip gameOverSound;


    [SerializeField] private float cutInDuration = 2.0f; // カットインの表示時間（秒）
    [SerializeField] private AudioClip gameOverSound; // ゲームオーバー時のサウンド
                                                      //[SerializeField] private string imageObjectName = "CutInImage"; // 画像のオブジェクト名
                                                      //[SerializeField] private string audioObjectName = "GameAudioSource"; // AudioSource のオブジェクト名
                                                      // private AudioSource gameOverSound; // 音声再生用のAudioSource

    [SerializeField] private float volume = 1.0f; // 音量 (デフォルトは最大)

    private bool isGameOver = false;    // 重複処理防止用フラグ
    private SubstitutedollController substitutedollController;
    private BeartrapController beartrapController;

    public int n = 0;
    private PlayerInventory playerInventory;
    
    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        curseSlider = FindObjectOfType<CurseSlider>(); // 呪いゲージを取得
        substitutedollController = FindObjectOfType<SubstitutedollController>(); // 追加
        beartrapController = FindObjectOfType<BeartrapController>(); // 追加
        cursePanel = GameObject.Find("CurseCanvasUI");
        curseText = GameObject.Find("CurseText")?.GetComponent<TextMeshProUGUI>();
        itemPanel = GameObject.Find("ItemCanvasUI");
        itemText = GameObject.Find("Text Item")?.GetComponent<TextMeshProUGUI>();
        cutInImage = GameObject.Find("ImageCurse")?.GetComponent<Image>();
        audioSource = GameObject.Find("Mamono_aaa")?.GetComponent<AudioSource>();
        GameObject[] allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            if (obj.name == "CurseCanvasUI")
            {
                cursePanel = obj;
            }
            if (obj.name == "CurseText")
            {
                curseText = obj.GetComponent<TextMeshProUGUI>();
            }
        }
        foreach (GameObject obj in allGameObjects)
        {
            if (obj.name == "ItemCanvasUI")
            {
                itemPanel = obj;
            }
            if (obj.name == "Text Item")
            {
                itemText = obj.GetComponent<TextMeshProUGUI>();
            }
        }
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceを追加

        // 非アクティブなオブジェクトも含めて Image を探す
        Image[] allImages = FindObjectsOfType<Image>(true);

        foreach (Image img in allImages)
        {
            if (img.gameObject.name == "ImageCurse") // 名前で指定
            {
                cutInImage = img;
                break;
            }
        }

        if (cutInImage != null)
        {
            Debug.Log("✅ 非アクティブな ImageCurse を取得しました！");
        }
        else
        {
            Debug.Log("⚠️ ImageCurse が見つかりません！");
        }
        // デバッグ用表示
        Debug.Log($"cursePanel: {cursePanel}");
        Debug.Log($"curseText: {curseText}");

        // UI が見つからない場合、警告を出す
        if (cursePanel == null) Debug.LogWarning("CursePanel が見つかりません");
        if (curseText == null) Debug.LogWarning("CurseText が見つかりません");
        if (itemPanel == null) Debug.LogWarning("ItemCanvasUI が見つかりません");
        if (itemText == null) Debug.LogWarning("ItemText が見つかりません");
        if (cutInImage == null) Debug.LogWarning("ImageCurse が見つかりません");
        if (audioSource == null) Debug.LogWarning("Mamono_aaa の AudioSource が見つかりません");
        // エラーチェック
        //if (cutInImage == null)
        //    Debug.LogError($"❌ {imageObjectName} が見つかりません！");

        //if (audioSource == null)
        //    Debug.LogError($"❌ {audioObjectName} が見つかりません！");


        if (substitutedollController == null)
        {
            Debug.LogError("❌ SubstitutedollController がシーン内に見つかりません！");
        }

        if (beartrapController == null)
        {
            Debug.LogError("❌ BeartrapController がシーン内に見つかりません！");
        }
        if (cursePanel != null)
        {
            cursePanel.SetActive(false);
        }
        if (itemPanel == null) Debug.LogError("❌ itemPanel がアタッチされていません！");
        //  if (itemLogText == null) Debug.LogError("❌ itemLogText がアタッチされていません！");
        if (itemPanel != null)
        {
            itemPanel.SetActive(false);
        }
        //if (closeButton != null)
        //{
        //    closeButton.onClick.AddListener(CloseEventUI);
        //}
        //playerInventory = FindObjectOfType<PlayerInventory>();
        //if (eventPanel != null)
        //{
        //    eventPanel.SetActive(false);
        //}

        //if (closeButton != null)
        //{
        //    closeButton.onClick.AddListener(CloseEventUI);
        //}

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


        if ((cursePanel.activeSelf || itemPanel.activeSelf)
         && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.H)))
        {
            Debug.Log("🔘 スペースまたは H キーで UI を閉じる");
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
                Debug.Log($"{name}: アイテムマスに止まりました。");
                GiveRandomItem();
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
    void ShowItemUI(string message, float delay = 2.0f)
    {
        StartCoroutine(DelayedShowItemUI(message, delay));
    }
    IEnumerator DelayedShowItemUI(string message, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (itemPanel != null && itemText != null)
        {
            itemText.text = message;
            // itemLogText.text = message;
            itemPanel.SetActive(true);
            Time.timeScale = 0; // **ゲームを一時停止**
        }
    }
    void CloseEventUI()
    {
        bool wasPaused = false;

        //if (eventPanel != null && eventPanel.activeSelf)
        //{
        //    eventPanel.SetActive(false);
        //    wasPaused = true;
        //}
        if (cursePanel != null && cursePanel.activeSelf)
        {
            cursePanel.SetActive(false);
            wasPaused = true;
        }
        if (itemPanel != null && itemPanel.activeSelf)
        {
            itemPanel.SetActive(false);
            wasPaused = true;
        }

        // UIが開いていた場合のみTime.timeScaleを戻す
        if (wasPaused)
        {
            Debug.Log("ゲーム再開！");
            Time.timeScale = 1;
        }
    }
    //private void DisplayRandomEvent()
    //{
    //    string[] eventMessages = {
    //        "ドアが開きました！",
    //        "クローゼットに隠れられる",
    //        "急に眠気がおそってきた。"
    //    };

    //    System.Random random = new System.Random();
    //    int randomIndex = random.Next(eventMessages.Length);

    //    string selectedEvent = eventMessages[randomIndex];
    //    Debug.Log($"{name}: イベント発動！ {selectedEvent}");

    //    //ExecuteSelectedEvent(selectedEvent);
    //}

    //private void ExecuteSelectedEvent(string eventMessage)
    //{
    //    switch (eventMessage)
    //    {
    //        case "ドアが開きました！":
    //            Debug.Log("ドアが開くイベントを実行します。");
    //            ShowEventUI("The door opened"); // UIに表示
    //            OpenDoor();
    //            break;
    //        case "クローゼットに隠れられる":
    //            Debug.Log("クローゼットに隠れるイベントを実行します。");
    //            ShowEventUI("クローゼットに隠れられる"); // UIに表示
    //            SecretCloset();
    //            break;
    //        case "急に眠気がおそってきた。":
    //            Debug.Log("眠気イベントを実行します。");
    //            ShowEventUI("急に眠気がおそってきた。"); // UIに表示
    //            SleepEvent();
    //            break;
    //        default:
    //            Debug.Log("未知のイベントです。");
    //            ShowEventUI("未知のイベント"); // UIに表示
    //            break;
    //    }
    //}



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
    void DisplayRandomEvent()
    {
        // **呪い発動**
        Debug.Log($"{name}: 呪いが発動！");
        curseSlider.DecreaseDashPoint(hirueamout); // 呪いゲージ増加
        ShowCurseUI("呪いが発動した！");
    }


    void DeBuh()
    {
        // **呪い発動**
        Debug.Log($"{name}: 呪いが浄化された");
        curseSlider.IncreaseDashPoint(curseamout); // 呪いゲージ増加
        ShowCurseUI("呪いが減った！");
    }
    private void ExecuteCurseEvent()
    {
        int randomValue = Random.Range(1, 101); // 1〜100の乱数を生成

        if (randomValue <= scareChance)

        {
            // **驚かしイベント発動**
            Debug.Log($"{name}: 驚かしイベントが発生！");
            StartCoroutine(TriggerScareEffect());
        }


        else
        {
            // **何も起こらない**
            Debug.Log($"{name}: 何も起こらなかった…");
            //ShowEventUI("…何も起こらなかった");
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



    private void GiveRandomItem()
    {
        // **アイテムコントローラーが見つからない場合は処理を中断**
        if (substitutedollController == null || beartrapController == null)
        {

            Debug.LogError("❌ アイテムのコントローラーが見つかりません！処理をスキップします。");
            ShowItemUI("❌ アイテムのコントローラーが見つかりません！");
            return;
        }

        string logMessage;

        // 50% の確率でどちらかのアイテムを増やす
        if (Random.value < 0.5f)
        {
            substitutedollController.AddItem();
            logMessage = "🎭 身代わり人形を獲得！";
        }
        else
        {
            beartrapController.AddItem();
            logMessage = "🪤 トラバサミを獲得！";
        }
        Debug.Log(logMessage);
        ShowItemUI(logMessage);

    }
    public void SetVisibility(bool isVisible)
    {
        // アクティブ状態は維持しつつ、レンダラーを有効/無効にする
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }

        // 子オブジェクトのレンダラーも有効/無効にする
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = isVisible;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DetectionBox"))
        {
            SetVisibility(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DetectionBox"))
        {
            SetVisibility(false);
        }
    }
}
    //// ✅ UI にログを表示し、Canvas を有効化する
    //private void ShowItemUI(string message)
    //{
    //    if (itemLogText != null)
    //    {
    //        itemLogText.text = message; // UI の Text を更新
    //    }
    //    else
    //    {
    //        Debug.LogError("❌ itemLogText が設定されていません！");
    //    }

    //    if (itemPanel != null)
    //    {
    //        //ShowItemUI( message,  = 1.0f);
    //        //StartCoroutine(HideItemCanvasAfterDelay()); // ⏳ 3 秒後に非表示
    //    }
    //    else
    //    {
    //        Debug.LogError("❌ itemCanvas が設定されていません！");
    //    }
    //}

    //// ✅ Canvas を 3 秒後に非表示にする
    //private IEnumerator HideItemCanvasAfterDelay()
    //{
    //    yield return new WaitForSeconds(3f);
    //    if (itemPanel != null)
    //    {
    //        itemPanel.SetActive(false);
    //    }
    //}
