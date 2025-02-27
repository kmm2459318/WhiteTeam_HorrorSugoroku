using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurseSlider : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;  //呪いゲージTEXT
    [SerializeField] Slider DashGage;
    [SerializeField] SceneChanger3D sceneChanger;
    [SerializeField] GameObject CardCanvas1;
    [SerializeField] GameObject CardCanvas2;
    //小さい呪い
    [SerializeField] Button extraButton;
    [SerializeField] Button hideButton;
    [SerializeField] Button cursegiveButton;
    //大きな呪い
    [SerializeField] Button ArmButton;
    [SerializeField] Button LegButton;
    [SerializeField] Button EyeButton;

    [SerializeField] private Master_Curse master_Curse;
    [SerializeField] private Image[] ImageGages; // 画像ゲージ（下から上に増える）
    [SerializeField] private TextMeshProUGUI eyeButtonText;

    private float maxDashPoint = 100;
    private float dashIncreasePerTurn = 5;

    public int CountGauge = 0;              //ゲームオーバーカウント
    public float dashPoint = 0;
    public GameManager gameManager;
    public TurnManager turnManager;
    private bool saikorotyu;
    private bool CardSelect1 = false;
    private bool CardSelect2 = false;
    private bool CardSelect3 = false;
    private bool CardSelect4 = false;
    private bool CardSelect5 = false;

    // ボタンの使用状態を管理
    private bool isArmButtonUsed = false;
    private bool isLegButtonUsed = false;
    private bool isEyeButtonUsed = false;
    public int sai = 1; // ランダムなサイコロの値（publicに変更）

    [SerializeField] private EyeEffectController eyeEffectController; // EyeEffectControllerの参照
    [SerializeField] private FlashlightManager flashlightManager; // FlashlightManagerの参照
    [SerializeField] private Transform playerTransform; // プレイヤーのTransform
    private PlayerSaikoro playerSaikoro; // PlayerSaikoroクラスのインスタンスを保持するフィールド
    [SerializeField] private DiceRangeManager diceRangeManager; // DiceRangeManagerへの参照
    public DiceController diceController; // DiceControllerへの参照
    [SerializeField] private GameObject eyeButtonCanvas;
    private bool isDisplayingText = false;

    private int nextShowCardThreshold = 20;
    // カード表示の閾値（20,40,60,80,100）

    //小さい呪い、大きい呪いどちらを表示しているかの判定
    private bool isCardCanvas1 = false;
    private bool isCardCanvas2 = false;

    [SerializeField] private HeelCurseGage heelCurseGage; // HeelCurseGageの参照を追加

    void Start()
    {
        DashGage.maxValue = maxDashPoint;
        DashGage.value = dashPoint;
        ResetGaugeImages();
        // PlayerSaikoroクラスのインスタンスを取得
        playerSaikoro = FindObjectOfType<PlayerSaikoro>();

        if (extraButton != null)
        {
            extraButton.onClick.RemoveAllListeners();
            extraButton.onClick.AddListener(() => { ExtraButtonAction(); HideCardCanvas1(); });
        }
        if (hideButton != null)
        {
            hideButton.onClick.RemoveAllListeners();
            hideButton.onClick.AddListener(() => { HideCardCanvasAndModifyDashIncrease(); HideCardCanvas1(); });
        }
        if (cursegiveButton != null)
        {
            cursegiveButton.onClick.RemoveAllListeners();
            cursegiveButton.onClick.AddListener(() => { CursegiveButtonAction(); HideCardCanvas1(); });
        }

        if (ArmButton != null)
        {
            ArmButton.onClick.RemoveAllListeners();
            ArmButton.onClick.AddListener(() => { Arm_ButtonAction(); HideCardCanvas2(); });
            ArmButton.onClick.AddListener(() => { flashlightManager.DeactivateFlashlight(); }); // 懐中電灯を非アクティブにする
        }

        if (LegButton != null)
        {
            LegButton.onClick.RemoveAllListeners();
            LegButton.onClick.AddListener(Leg_ButtonAction);
        }
        if (EyeButton != null)
        {
            EyeButton.onClick.RemoveAllListeners();
            EyeButton.onClick.AddListener(() => { Eye_ButtonAction(); HideCardCanvas2(); });
            EyeButton.onClick.AddListener(() => { eyeEffectController.ApplyEyeEffect(); }); // 追加
        }

        HideCardCanvas1();
        HideCardCanvas2();
    }

    private void ExtraButtonAction()
    {
        throw new NotImplementedException();
    }

    public void HideCardCanvasAndModifyDashIncrease()
    {
        dashIncreasePerTurn += master_Curse.CurseSheet[1].TurnIncrease;
        Debug.Log("[CurseSlider] Dash Increase Per Turn set to: " + dashIncreasePerTurn);
        Time.timeScale = 1;
    }

    void Update()
    {
        //小さい呪い画面表示でASDキーで押せるようにする
        if (isCardCanvas1)
        {
            if (Input.GetKeyDown(KeyCode.A) && extraButton != null)
            {
                extraButton.onClick.Invoke();
                isCardCanvas1 = false;
            }

            if (Input.GetKeyDown(KeyCode.S) && hideButton != null)
            {
                hideButton.onClick.Invoke();
                isCardCanvas1 = false;
            }

            if (Input.GetKeyDown(KeyCode.D) && cursegiveButton != null)
            {
                cursegiveButton.onClick.Invoke();
                isCardCanvas1 = false;
            }
        }

        //大きい呪い画面表示でASDキーで押せるようにする
        if (isCardCanvas2)
        {
            if (Input.GetKeyDown(KeyCode.A) && ArmButton != null)
            {
                ArmButton.onClick.Invoke();
                isCardCanvas2 = false;
            }

            if (Input.GetKeyDown(KeyCode.S) && LegButton != null)
            {
                LegButton.onClick.Invoke();
                isCardCanvas2 = false;
            }

            if (Input.GetKeyDown(KeyCode.D) && EyeButton != null)
            {
                EyeButton.onClick.Invoke();
                isCardCanvas2 = false;
            }
        }


        if (80 <= dashPoint && dashPoint >= 100 && CardSelect1 == false)
        {
            CardSelect1 = true;
            StartCoroutine(ShowCardCanvas2());
        }
        else if (80 <= dashPoint && dashPoint < 100 && CardSelect2 == false)
        {
            CardSelect2 = true;
            StartCoroutine(ShowCardCanvas1());
        }
        else if (60 <= dashPoint && dashPoint < 80 && CardSelect3 == false)
        {
            CardSelect3 = true;
            StartCoroutine(ShowCardCanvas1());
        }
        else if (40 <= dashPoint && dashPoint < 60 && CardSelect4 == false)
        {
            CardSelect4 = true;
            StartCoroutine(ShowCardCanvas1());
        }
        else if (20 <= dashPoint && dashPoint < 40 && CardSelect5 == false)
        {
            CardSelect5 = true;
            StartCoroutine(ShowCardCanvas1());
        }

        DashGage.value = dashPoint;

        if (dashPoint >= maxDashPoint)
        {

            CountGauge++;
            dashPoint = 0;
            CardSelect1 = false;
            CardSelect2 = false;
            CardSelect3 = false;
            CardSelect4 = false;
            CardSelect5 = false;
            ResetGaugeImages();
            UpdateCountText();
        }

        

        UpdateImageGauges();
    }

    
    public void IncreaseDashPointPerTurn()
    {
        dashPoint = Mathf.Min(dashPoint + dashIncreasePerTurn, maxDashPoint);
        DashGage.value = dashPoint;

        Debug.Log("今の呪いゲージ量: " + dashPoint);
    }



    private void UpdateImageGauges()
    {
        for (int i = 0; i < ImageGages.Length; i++)
        {
            float min = i * 20;
            float max = (i + 1) * 20;
            float fill = Mathf.Clamp01((dashPoint - min) / (max - min));
            ImageGages[i].fillAmount = fill;
        }
    }

    private void ResetGaugeImages()
    {
        foreach (Image img in ImageGages)
        {
            img.fillAmount = 0;
        }
    }

    public IEnumerator ShowCardCanvas2()
    {
        if (CardCanvas2 != null)
        {
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            CardCanvas2.SetActive(true);
            ArmButton.interactable = !isArmButtonUsed;
            LegButton.interactable = !isLegButtonUsed;
            EyeButton.interactable = !isEyeButtonUsed;
            yield return new WaitForSeconds(1.0f);
            Time.timeScale = 0; // **ゲームを停止**
            isCardCanvas2 = true;
        }
    }

    public IEnumerator ShowCardCanvas1()
    {
        if (CardCanvas1 != null)
        {
            CardCanvas1.SetActive(true);
            ArmButton.interactable = !isArmButtonUsed;
            LegButton.interactable = !isLegButtonUsed;
            EyeButton.interactable = !isEyeButtonUsed;
            yield return new WaitForSeconds(1.0f);
            Time.timeScale = 0; // **ゲームを停止**
            isCardCanvas1 = true;
        }
    }

    public void HideCardCanvas2()
    {
        if (CardCanvas2 != null)
        {
            CardCanvas2.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    public void HideCardCanvas1()
    {
        if (CardCanvas1 != null)
        {
            CardCanvas1.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    private bool isArmButtonClicked = false;

    public void Arm_ButtonAction()
    {
        if (isArmButtonClicked) return;
        isArmButtonClicked = true;

        Debug.Log("Arm_Buttonが呼び出されました。");
        Debug.Log("腕がなくなった");
        ArmButton.interactable = false;
        ArmButton.gameObject.SetActive(false); // ボタンを非表示にする
        isArmButtonUsed = true;

        // 懐中電灯を非アクティブにする
        flashlightManager.DeactivateFlashlight();

        // Canvasをアクティブにしてテキストを一文字ずつ表示
        if (eyeButtonText != null && eyeButtonCanvas != null && !isDisplayingText)
        {
            StartCoroutine(ActivateCanvasForDuration(eyeButtonCanvas, 5f));
            StartCoroutine(DisplayTextOneByOne("片手ヲ失っタ。\n懐中電灯が使えナイ。", eyeButtonText, 0.1f));
        }
    }

    private bool isLegButtonClicked = false;

    public void Leg_ButtonAction()
    {
        Debug.Log("足がなくなった");
        Debug.Log("Leg_Buttonが呼び出されました");
        LegButton.interactable = false;
        LegButton.gameObject.SetActive(false); // ボタンを非表示にする
        isLegButtonUsed = true;

        // サイコロの出目を1から3に設定
        diceRangeManager.SetDiceRollRange(1, 3);
        diceController.SetDiceRollRange(1, 3); // DiceControllerにも範囲を設定
        playerSaikoro.SetLegButtonEffect(true); // LegButtonの効果を有効にする
        HideCardCanvas2();

        // Canvasをアクティブにしてテキストを一文字ずつ表示
        if (eyeButtonText != null && eyeButtonCanvas != null && !isDisplayingText)
        {
            StartCoroutine(ActivateCanvasForDuration(eyeButtonCanvas, 5f));
            StartCoroutine(DisplayTextOneByOne("片足ヲ失っタ。\nサイコロが1,2,3しか出ナイ。", eyeButtonText, 0.1f));
        }
    }

    public void Eye_ButtonAction()
    {
        Debug.Log("目が落ちた");
        EyeButton.interactable = false;
        EyeButton.gameObject.SetActive(false); // ボタンを非表示にする
        isEyeButtonUsed = true;

        // Canvasをアクティブにしてテキストを一文字ずつ表示
        if (eyeButtonText != null && eyeButtonCanvas != null && !isDisplayingText)
        {
            StartCoroutine(ActivateCanvasForDuration(eyeButtonCanvas, 5f));
            StartCoroutine(DisplayTextOneByOne("片目ヲ失っタ。", eyeButtonText, 0.1f));
        }
    }


    private IEnumerator DisplayTextOneByOne(string message, TextMeshProUGUI textComponent, float delay)
    {
        isDisplayingText = true;
        textComponent.text = "";
        foreach (char letter in message)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(delay);
        }
        isDisplayingText = false;
    }

    private IEnumerator ActivateCanvasForDuration(GameObject canvas, float duration)
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(duration);
        canvas.SetActive(false);
    }

    public void CursegiveButtonAction()
    {
        dashPoint = Mathf.Min(dashPoint + 5, maxDashPoint);
        DashGage.value = dashPoint;
        Debug.Log("[CursegiveButton] After: DashPoint = " + dashPoint);
        Time.timeScale = 1;
    }


    private void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = (CountGauge >= 2) ? "呪" : (3 - CountGauge).ToString();
        }
    }

    public void IncreaseDashPoint(int amount)
    {
        dashPoint = Mathf.Min(dashPoint + amount, maxDashPoint);
        DashGage.value = dashPoint;
        Debug.Log("[CurseSlider] 呪いゲージ増加: " + amount + " 現在の値: " + dashPoint);
    }
    private void DivideDiceRoll()
    {
        if (playerSaikoro != null)
        {
            // サイコロの出目を2で割って切り捨て
            playerSaikoro.sai = Mathf.FloorToInt(playerSaikoro.sai / 2.0f);
            Debug.Log("サイコロの出目を2で割って切り捨てた結果: " + playerSaikoro.sai);
        }
        else
        {
            Debug.LogError("PlayerSaikoroのインスタンスが見つかりませんでした。");
        }
    }
}