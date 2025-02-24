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

    private int nextShowCardThreshold = 20;
    // カード表示の閾値（20,40,60,80,100）

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
            ArmButton.onClick.AddListener(() => { flashlightManager.DeactivateFlashlight(); }); // 追加
        }

        if (LegButton != null)
        {
            LegButton.onClick.RemoveAllListeners();
            LegButton.onClick.AddListener(() => { Leg_ButtonAction(); HideCardCanvas2(); });
            LegButton.onClick.AddListener(() => { DivideDiceRoll(); }); // 追加
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
        if (100 <= dashPoint && CardSelect1 == false)
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CardCanvas2.SetActive(true);
            ArmButton.interactable = !isArmButtonUsed;
            LegButton.interactable = !isLegButtonUsed;
            EyeButton.interactable = !isEyeButtonUsed;
            yield return new WaitForSeconds(1.0f);
            Time.timeScale = 0;
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
            Time.timeScale = 0;
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

   public void Arm_ButtonAction()
    {
        Debug.Log("腕がなくなった");
        ArmButton.interactable = false;
        ArmButton.gameObject.SetActive(false); // ボタンを非表示にする
        isArmButtonUsed = true;
    }

    public void Leg_ButtonAction()
    {
        Debug.Log("足がなくなった");
        LegButton.interactable = false;
        LegButton.gameObject.SetActive(false); // ボタンを非表示にする
        isLegButtonUsed = true;
    }

    public void Eye_ButtonAction()
    {
        Debug.Log("目が落ちた");
        EyeButton.interactable = false;
        EyeButton.gameObject.SetActive(false); // ボタンを非表示にする
        isEyeButtonUsed = true;
    }


    public void HideCardCanvasAndModifyDashIncrease()
    {
        dashIncreasePerTurn += master_Curse.CurseSheet[1].TurnIncrease;
        Debug.Log("[CurseSlider] Dash Increase Per Turn set to: " + dashIncreasePerTurn);
        Time.timeScale = 1;
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

    public void CursegiveButtonAction()
    {
        dashPoint = Mathf.Min(dashPoint + 5, maxDashPoint);
        DashGage.value = dashPoint;
        Debug.Log("[CursegiveButton] After: DashPoint = " + dashPoint);
        Time.timeScale = 1;
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