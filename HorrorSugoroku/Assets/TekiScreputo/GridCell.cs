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
    public string cellEffect = "Normal";
    [SerializeField] private Master_Debuff DebuffSheet;

    private GameObject ui;
    private Transform ccursePanel;

    // static共有するUI部品
    public static GameObject cursePanel;
    public static TextMeshProUGUI curseText;

    public string requiredItem = "鍵";
    private CurseSlider curseSlider;

    [SerializeField] private int scareChance = 30;
    [SerializeField] private int nothingChance = 20;

    [SerializeField] private int curseamout = 5;
    [SerializeField] private int hirueamout = 10;

    [SerializeField] private float cutInDuration = 2.0f;
    [SerializeField] private AudioClip gameOverSound;

    [SerializeField] private float volume = 1.0f;

    private bool isGameOver = false;
    private SubstitutedollController substitutedollController;
    private BeartrapController beartrapController;

    public int n = 0;
    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        curseSlider = FindObjectOfType<CurseSlider>();
        substitutedollController = FindObjectOfType<SubstitutedollController>();
        beartrapController = FindObjectOfType<BeartrapController>();

        ui = GameObject.Find("UI");
        ccursePanel = ui.transform.Find("CurseCanvasUI");

        // static に一度だけ代入する
        if (cursePanel == null)
        {
            cursePanel = ccursePanel.gameObject;
            Debug.Log($"cursePanel 取得成功: {cursePanel}");
        }

        if (curseText == null)
        {
            curseText = GameObject.Find("CurseText").GetComponent<TextMeshProUGUI>();
            Debug.Log($"curseText 取得成功: {curseText}");
        }

        if (cursePanel == null) Debug.LogWarning("CursePanel が見つかりません");
        if (curseText == null) Debug.LogWarning("CurseText が見つかりません");

        if (cursePanel != null)
        {
            cursePanel.SetActive(false);
        }

        Debug.Log("ID:" + DebuffSheet.DebuffSheet[n].ID);
        Debug.Log("イベント名:" + DebuffSheet.DebuffSheet[n].Name);
        Debug.Log("懐中電灯の最小ゲージ減少量:" + DebuffSheet.DebuffSheet[n].DecreaseMin);
        Debug.Log("懐中電灯の最大ゲージ減少量:" + DebuffSheet.DebuffSheet[n].DecreaseMax);
        Debug.Log("アイテムを付与するかの判定:" + DebuffSheet.DebuffSheet[n].ItemGive);
        Debug.Log("アイテムが使えなくなるかの判定:" + DebuffSheet.DebuffSheet[n].ItemGive);
        Debug.Log("アイテムが使えないターン数:" + DebuffSheet.DebuffSheet[n].ItemGive);
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
        //if (curseText != null && curseText.gameObject.activeSelf)
        //{
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        HidecurseText(); // Gキーを押したらテキストを非表示
        //        Debug.Log("🔘 Gキーを押して UI を閉じました");
        //    }
        //}

    }
    public void ExecuteEvent()
    {
        ShowActionText(); // マスに止まったらテキストを表示


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


        // 指定された時間だけ待機
        yield return new WaitForSeconds(cutInDuration);
    }



    public void SetVisibility(bool isVisible)
    {
        // 子オブジェクトの Renderer を有効/無効にする
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = isVisible;
        }
    }

    public void ShowActionText()
    {
        if (curseText != null)
        {
            curseText.text = "[G] Key Click"; // テキストを設定
            curseText.gameObject.SetActive(true); // テキストを表示
        }
    }
    public void HideActionText()
    {
        if (curseText != null)
        {
            curseText.gameObject.SetActive(false); // テキストを非表示
        }
    }


}

