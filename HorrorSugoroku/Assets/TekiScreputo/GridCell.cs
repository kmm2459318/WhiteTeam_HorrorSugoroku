using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    //GameManager gameManager;

    public string cellEffect = "Normal"; // マス目の効果（例: Normal, Bonus, Penalty）
    [SerializeField] private Master_Debuff DebuffSheet;
    //public GameObject eventPanel; // UIのパネル
    //public TextMeshProUGUI eventText; // UIのテキスト
    private GameObject ui;
    private Transform ccursePanel;
    private Transform iitemPanel;
    public GameObject cursePanel; // UIのパネル
    public TextMeshProUGUI curseText; // UIのテキスト
    //public GameObject itemPanel; // UIのパネル
    //public TextMeshProUGUI itemText; // UIのテキスト
                                     //public GameObject debffPanel; // UIのパネル
                                     //public TextMeshProUGUI debffText; // UIのテキスト
                                     //                                 //   public TMP_Text itemLogText;
                                     // public Button closeButton; // UIを閉じるボタン
                                     //public ItemPickup item;
    public string requiredItem = "鍵"; // 必要なアイテム
    private CurseSlider curseSlider;                                // public int gridCellIncreaseAmount = 20; // GridCell 側の呪いゲージ増加量
                                                                    // [SerializeField] private int curseChance = 50;  // 呪いの発生確率（％）
    [SerializeField] private int scareChance = 30;  // 驚かしイベントの発生確率（％）
    [SerializeField] private int nothingChance = 20; // 何も起こらない確率（％）
                                                     // [SerializeField] private int hiruChance = 50;  // 呪いの回復確率（％）

    [SerializeField] private int curseamout = 5;//呪いの増加量の調整
    [SerializeField] private int hirueamout = 10;//呪いの回復量の調整
    public Image cutInImage; // カットイン画像
    private Sprite loadedSprite;
    public AudioSource audioSource; // 音声
                                    // private AudioClip gameOverSound;
    public TextMeshProUGUI actionText; // インスペクターで割り当てるテキストUI


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
    public Outline outlineObject; // インスペクターで指定するアウトラインオブジェクト


    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        curseSlider = FindObjectOfType<CurseSlider>(); // 呪いゲージを取得
        substitutedollController = FindObjectOfType<SubstitutedollController>(); // 追加
        beartrapController = FindObjectOfType<BeartrapController>(); // 追加
        ui = GameObject.Find("UI");
        ccursePanel = ui.transform.Find("CurseCanvasUI");
        cursePanel = ccursePanel.gameObject;
        curseText = GameObject.Find("CurseText").GetComponent<TextMeshProUGUI>();
        //iitemPanel = ui.transform.Find("ItemCanvasUI");
        //itemPanel = iitemPanel.gameObject;
        //itemText = GameObject.Find("Text Item").GetComponent<TextMeshProUGUI>();
        //cutInImage = GameObject.Find("ImageCurse")?.GetComponent<Image>();
        //audioSource = GameObject.Find("Mamono_aaa")?.GetComponent<AudioSource>();
        //GameObject[] allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        Debug.Log($"cursePanel: {cursePanel}");
        Debug.Log($"curseText: {curseText}");
        //foreach (GameObject obj in allGameObjects)
        //{
        //    if (obj.name == "CurseCanvasUI")
        //    {
        //        cursePanel = obj;
        //    }
        //    if (obj.name == "CurseText")
        //    {
        //        curseText = obj.GetComponent<TextMeshProUGUI>();
        //    }
        //}
        //foreach (GameObject obj in allGameObjects)
        //{
        //    if (obj.name == "ItemCanvasUI")
        //    {
        //        itemPanel = obj;
        //    }
        //    if (obj.name == "Text Item")
        //    {
        //        itemText = obj.GetComponent<TextMeshProUGUI>();
        //    }
        //}
        //audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceを追加

        // 非アクティブなオブジェクトも含めて Image を探す
        //Image[] allImages = FindObjectsOfType<Image>(true);

        //foreach (Image img in allImages)
        //{
        //    if (img.gameObject.name == "ImageCurse") // 名前で指定
        //    {
        //        cutInImage = img;
        //        break;
        //    }
        //}

        //if (cutInImage != null)
        //{
        //    Debug.Log("✅ 非アクティブな ImageCurse を取得しました！");
        //}
        //else
        //{
        //    Debug.Log("⚠️ ImageCurse が見つかりません！");
        //}
        //// デバッグ用表示
        // UIを非表示にする
        //場合、警告を出す
        if (cursePanel == null) Debug.LogWarning("CursePanel が見つかりません");
        if (curseText == null) Debug.LogWarning("CurseText が見つかりません");
        //if (itemPanel == null) Debug.LogWarning("ItemCanvasUI が見つかりません");
        //if (itemText == null) Debug.LogWarning("ItemText が見つかりません");
        if (cutInImage == null) Debug.LogWarning("ImageCurse が見つかりません");
        if (audioSource == null) Debug.LogWarning("Mamono_aaa の AudioSource が見つかりません");

        if (cursePanel != null)
        {
            cursePanel.SetActive(false);
        }
        //if (itemPanel == null) Debug.LogError("❌ itemPanel がアタッチされていません！");
        ////  if (itemLogText == null) Debug.LogError("❌ itemLogText がアタッチされていません！");
        //if (itemPanel != null)
        //{
        //    itemPanel.SetActive(false);
        //}
        Debug.Log("ID:" + DebuffSheet.DebuffSheet[n].ID);
        Debug.Log("イベント名:" + DebuffSheet.DebuffSheet[n].Name);
        Debug.Log("懐中電灯の最小ゲージ減少量:" + DebuffSheet.DebuffSheet[n].DecreaseMin);
        Debug.Log("懐中電灯の最大ゲージ減少量:" + DebuffSheet.DebuffSheet[n].DecreaseMax);
        Debug.Log("アイテムを付与するかの判定:" + DebuffSheet.DebuffSheet[n].ItemGive);
        Debug.Log("アイテムが使えなくなるかの判定:" + DebuffSheet.DebuffSheet[n].ItemGive);
        Debug.Log("アイテムが使えないターン数:" + DebuffSheet.DebuffSheet[n].ItemGive);

        SetVisibility(true);
        if (actionText != null)
        {
            actionText.gameObject.SetActive(false); // 初期状態では非表示
        }


    }
    void Update()
    {
        SetVisibility(true);

        if (cursePanel.activeSelf )
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("🔘 スペースまたは H キーで UI を閉じる");
                CloseEventUI();
           }
        //if (actionText != null && actionText.gameObject.activeSelf)
        //{
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        HideActionText(); // Gキーを押したらテキストを非表示
        //        Debug.Log("🔘 Gキーを押して UI を閉じました");
        //    }
        //}
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            switch (cellEffect)
            {
                case "Event":
                    renderer.material.color = Color.red; // 赤
                    break;
                case "Debuff":
                    renderer.material.color = Color.green; // 緑
                    break;
                // 他にも追加可能
                case "Curse":
                    renderer.material.color = Color.magenta;
                    break;
                case "Item":
                    renderer.material.color = Color.cyan;
                    break;
                default:
                    renderer.material.color = Color.white; // 通常は白
                    break;
            }
        }
    }
    public void ExecuteEvent()
    {
        ShowActionText(); // マスに止まったらテキストを表示


        if (outlineObject != null)
        {
            outlineObject.enabled = true; // アウトラインを有効にする
            Debug.Log("アウトラインが有効化されました！");
        }

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
                //GiveRandomItem();
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
            //case "Exit":
            //    Debug.Log($"{name}: 出口マスに到達。");
            //    if (gameManager.isExitDoor)
            //    {
            //        Debug.Log("脱出！ゲームクリア！");
            //        SceneManager.LoadScene("Gameclear");
            //    }
            //    else
            //    {
            //        Debug.Log("鍵がかかってる");
            //    }
            //    break;

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
    //void ShowItemUI(string message, float delay = 2.0f)
    //{
    //    StartCoroutine(DelayedShowItemUI(message, delay));
    //}
    //IEnumerator DelayedShowItemUI(string message, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    if (itemPanel != null && itemText != null)
    //    {
    //        itemText.text = message;
    //        // itemLogText.text = message;
    //        itemPanel.SetActive(true);
    //        Time.timeScale = 0; // **ゲームを一時停止**
    //    }
    //}
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
        //if (itemPanel != null && itemPanel.activeSelf)
        //{
        //    itemPanel.SetActive(false);
        //    wasPaused = true;
        //}

        // UIが開いていた場合のみTime.timeScaleを戻す
        if (wasPaused)
        {
            Debug.Log("ゲーム再開！");
            Time.timeScale = 1;
        }
    }



    //public void OpenDoor()
    //{
    //    Debug.Log("ドアが開くイベントを実行します。");
    //    // ドアが開く処理をここに追加
    //}

    //public void SecretCloset()
    //{
    //    Debug.Log("クローゼットに隠れるイベントを実行します。");
    //    // クローゼットに隠れる処理をここに追加
    //    SceneChanger3D.hasSubstituteDoll = true; // 使用判定をトゥルーに設定
    //}

    //public void SleepEvent()
    //{
    //    Debug.Log("眠気イベントを実行します。");
    //    // 眠気の処理をここに追加
    //}

    //public void LogCellArrival()
    //{
    //    Debug.Log($"プレイヤーが {name} に到達しました。現在の位置: {transform.position}");
    //}
    void DisplayRandomEvent()
    {
        // **呪い発動**
        Debug.Log($"{name}: 呪いが発動！");
        curseSlider.DecreaseDashPoint(curseamout); // 呪いゲージ増加
        ShowCurseUI($"{curseamout}呪いが発動した！");
    }


    void DeBuh()
    {
        // **呪い発動**
        Debug.Log($"{name}: 呪いが浄化された");
        curseSlider.IncreaseDashPoint(hirueamout); // 呪いゲージ減少
        ShowCurseUI($"{hirueamout}呪いが減った！");
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



    public void SetVisibility(bool isVisible)
    {
        // 子オブジェクトの Renderer を有効/無効にする
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = isVisible;
        }
    }
    public void DisableOutline()
    {
        if (outlineObject != null)
        {
            outlineObject.enabled = false; // アウトラインを無効化
            Debug.Log("アウトラインが無効化されました！");
        }
    }

    public void ShowActionText()
    {
        if (actionText != null)
        {
            actionText.text = "[G] Key Click"; // テキストを設定
            actionText.gameObject.SetActive(true); // テキストを表示
        }
    }
    public void HideActionText()
    {
        if (actionText != null)
        {
            actionText.gameObject.SetActive(false); // テキストを非表示
        }
    }


}

