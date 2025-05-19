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

    private float uiCloseTimer = 0f;
    public float uiCloseDelay = 2f; // UIを何秒後に自動で閉じるか


    private bool isGameOver = false;
    private SubstitutedollController substitutedollController;
    private BeartrapController beartrapController;

    public int n = 0;
    private PlayerInventory playerInventory;

    [SerializeField] private ParticleSystem debuffEffect; // インスペクターでアタッチするために追加
    [SerializeField] private ParticleSystem normalEffect; // 通常エフェクト


    [SerializeField] private AudioClip debuffSound; // デバフエフェクトの音
    [SerializeField] private AudioClip normalSound; // 通常エフェクトの音

    private AudioSource audioSource;



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

        //if (debuffEffect != null)
        //{
        //    debuffEffect.Stop(); // 初期状態では停止
        //}

        if (cellEffect != "Debuff" && normalEffect != null && normalEffect.isPlaying)
        {
            normalEffect.Stop();
            normalEffect.Clear(); // 履歴をクリア
        }



        audioSource = gameObject.AddComponent<AudioSource>();
    }


    void Update()
    {
        SetVisibility(true);
        if (cursePanel.activeSelf)
        {
            // 自動閉じタイマー加算
            uiCloseTimer += Time.deltaTime;

            // 入力で閉じる
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("🔘 スペースまたは H キーで UI を閉じる");
                CloseEventUI();
            }

            // 一定時間経過で閉じる
            if (uiCloseTimer >= uiCloseDelay)
            {
                Debug.Log("⏳ UI自動閉じ");
                CloseEventUI();
            }
        }
        else
        {
            // 非表示ならタイマーリセット
            uiCloseTimer = 0f;
        }

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

        // デバフ効果のチェック
        if (cellEffect != "Event" && debuffEffect != null && debuffEffect.isPlaying)
        {
            debuffEffect.Stop(); // マスを離れたらエフェクトを停止
            debuffEffect.gameObject.SetActive(false); // エフェクトを非アクティブにする
        }

        // 通常エフェクトのチェック
        if (cellEffect != "Debuff" && normalEffect != null && normalEffect.isPlaying)
        {
            normalEffect.Stop();
            // **オブジェクトを消さずに停止する**
        }

        //if (cellEffect == "Debuff" && normalEffect != null)
        //{
        //    normalEffect.gameObject.SetActive(true); // **強制アクティブ化**
        //}

    }

    public void ExecuteEvent()
    {
        ShowActionText(); // マスに止まったらテキストを表示

        // プレイヤーがマスに止まった時にエフェクトを発現させる
        TriggerEffect();

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
            //    Debug.Log($"{name}: 出口マスに到達。");
            //    if (gameManager.isExitDoor)
            //    {
            //        Debug.Log("脱出！ゲームクリア！");
            //        SceneManager.LoadScene("Gameclear");
            //    }
            //    else
            //    {
            //        Debug.Log("鍵がかかってる");
            //    }
            //    break;

            case "Curse":
                //  Debug.Log($"{name}: 呪いゲージが増えた。");
                Debug.Log($"{name}: 呪いマスに到達。ランダムイベントを発動します。");
                ExecuteCurseEvent();
                break;

            default:
                Debug.Log($"{name}: 通常マス - 効果なし。");
                break;
        }
    }

    void TriggerEffect()
    {
        Debug.Log($"TriggerEffect called with cellEffect: {cellEffect}");

        if (cellEffect == "Event" && debuffEffect != null)
        {
            Debug.Log("🔴 発動するエフェクト: EventCell → DebuffEffect");

            if (!debuffEffect.gameObject.activeSelf)
            {
                debuffEffect.gameObject.SetActive(true); // **オブジェクトを再アクティブ化**
            }

            debuffEffect.Stop(); // **一度停止**
            debuffEffect.Clear(); // **履歴クリア**
            debuffEffect.Play(); // **再生**
            PlaySound(debuffSound);

            Debug.Log($"✅ debuffEffect 状態: Active={debuffEffect.gameObject.activeSelf}, Playing={debuffEffect.isPlaying}");
        }
        else if (cellEffect == "Debuff" && normalEffect != null)
        {
            Debug.Log("🟢 発動するエフェクト: DebuffCell → NormalEffect");

            if (!normalEffect.gameObject.activeSelf)
            {
                normalEffect.gameObject.SetActive(true); // **オブジェクトを再アクティブ化**
            }

            normalEffect.Stop(); // **一度停止**
            normalEffect.Clear(); // **履歴クリア**
            normalEffect.Play(); // **再生**
            PlaySound(normalSound);

            Debug.Log($"✅ normalEffect 状態: Active={normalEffect.gameObject.activeSelf}, Playing={normalEffect.isPlaying}");
        }
        else
        {
            Debug.LogWarning("⚠ 適切なエフェクトが再生されませんでした！");
        }
    }


    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
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
            // Time.timeScale = 0; // **ゲームを一時停止**
        }
    }
    //void ShowItemUI(string message, float delay = 2.0f)
    //{
    // StartCoroutine(DelayedShowItemUI(message, delay));
    //}
    //IEnumerator DelayedShowItemUI(string message, float delay)
    //{
    // yield return new WaitForSeconds(delay);
    // if (itemPanel != null && itemText != null)
    // {
    // itemText.text = message;
    // // itemLogText.text = message;
    // itemPanel.SetActive(true);
    // Time.timeScale = 0; // **ゲームを一時停止**
    // }
    //}
    void CloseEventUI()
    {
        bool wasPaused = false;

        //if (eventPanel != null && eventPanel.activeSelf)
        //{
        // eventPanel.SetActive(false);
        // wasPaused = true;
        //}
        if (cursePanel != null && cursePanel.activeSelf)
        {
            cursePanel.SetActive(false);
            wasPaused = true;
        }
        //if (itemPanel != null && itemPanel.activeSelf)
        //{
        // itemPanel.SetActive(false);
        // wasPaused = true;
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
    // Debug.Log("ドアが開くイベントを実行します。");
    // // ドアが開く処理をここに追加
    //}

    //public void SecretCloset()
    //{
    // Debug.Log("クローゼットに隠れるイベントを実行します。");
    // // クローゼットに隠れる処理をここに追加
    // SceneChanger3D.hasSubstituteDoll = true; // 使用判定をトゥルーに設定
    //}

    //public void SleepEvent()
    //{
    // Debug.Log("眠気イベントを実行します。");
    // // 眠気の処理をここに追加
    //}

    //public void LogCellArrival()
    //{
    // Debug.Log($"プレイヤーが {name} に到達しました。現在の位置: {transform.position}");
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

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"プレイヤーが {cellEffect} マスから離れました");

            if (debuffEffect != null && debuffEffect.gameObject.activeSelf)
            {
                debuffEffect.Stop();
                debuffEffect.gameObject.SetActive(false); // **非アクティブ化**
                Debug.Log("DebuffEffect を非アクティブ化しました");
            }

            if (normalEffect != null && normalEffect.gameObject.activeSelf)
            {
                normalEffect.Stop();
                normalEffect.gameObject.SetActive(false); // **非アクティブ化**
                Debug.Log("❌ normalEffect 停止 & 非アクティブ化");
            }

        }
    }
}
