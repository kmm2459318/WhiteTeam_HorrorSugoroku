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
    [SerializeField] GameObject CardCanvas3;
    //小さい呪い
    [SerializeField] Button extraButton;
    [SerializeField] Button hideButton;
    [SerializeField] Button cursegiveButton;

    // 大きな呪い
    [SerializeField] Button ArmButton;
    [SerializeField] Button LegButton;
    [SerializeField] Button EyeButton;

    [SerializeField] private Master_Curse master_Curse;
    [SerializeField] private Image[] ImageGages; // 画像ゲージ（下から上に増える）

    private float maxDashPoint = 100;
    private float dashIncreasePerTurn = 5;

    public int CountGauge = 0; // ゲームオーバーカウント
    public float dashPoint = 0;
    public GameManager gameManager;
    public TurnManager turnManager;
    private bool saikorotyu;
    private bool CardSelect1 = false;
    private bool CardSelect2 = false;
    private bool CardSelect3 = false;
    private bool CardSelect4 = false;

    [SerializeField] private EyeEffectController eyeEffectController; // EyeEffectControllerの参照
    [SerializeField] private FlashlightManager flashlightManager;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject uiCanvas; // UI全体を管理するオブジェクト
    

    void Start()
    {
        DashGage.maxValue = maxDashPoint;
        DashGage.value = dashPoint;
        ResetGaugeImages();

        if (extraButton != null)
        {
            extraButton.onClick.RemoveAllListeners();
            extraButton.onClick.AddListener(() => { ExtraButtonAction(); HideCardCanvas1(); });
            HideCardCanvases(); // CardCanvas1とCardCanvas2を非アクティブにする
        }
        if (hideButton != null)
        {
            hideButton.onClick.RemoveAllListeners();
            hideButton.onClick.AddListener(() => { HideCardCanvasAndModifyDashIncrease(); HideCardCanvas1(); });
            HideCardCanvases(); // CardCanvas1とCardCanvas2を非アクティブにする
        }
        if (cursegiveButton != null)
        {
            cursegiveButton.onClick.RemoveAllListeners();
            cursegiveButton.onClick.AddListener(() => { CursegiveButtonAction(); HideCardCanvas1(); });
            HideCardCanvases(); // CardCanvas1とCardCanvas2を非アクティブにする
        }

        if (ArmButton != null)
        {
            ArmButton.onClick.RemoveAllListeners();
            ArmButton.onClick.AddListener(() => { ExtraButtonAction(); HideCardCanvases(); LimitDiceRoll(); });
            HideCardCanvases(); // CardCanvas1とCardCanvas2を非アクティブにする
        }
        if (LegButton != null)
        {
            LegButton.onClick.RemoveAllListeners();
            LegButton.onClick.AddListener(() =>
            {
                HideCardCanvasAndModifyDashIncrease();
                HideCardCanvas1();
                flashlightManager.DeactivateFlashlight(); // 懐中電灯を非アクティブにする
                flashlightManager.PlaceFlashlightUnderPlayer(playerTransform); // 懐中電灯をプレイヤーの真下に配置
                HideCardCanvases(); // CardCanvas1とCardCanvas2を非アクティブにする
            });
        }
        if (EyeButton != null)
        {
            EyeButton.onClick.RemoveAllListeners();
            EyeButton.onClick.AddListener(() => { CursegiveButtonAction(); HideCardCanvas1(); ApplyEyeEffect(); });
            HideCardCanvases();// CardCanvas1とCardCanvas2を非アクティブにする
        }

        HideCardCanvas1();
        HideCardCanvas2();
    }

    void Update()
    {
        if (80 <= dashPoint && dashPoint >= 100 && CardSelect1 == false)
        {
            CardSelect1 = true;
            StartCoroutine(ShowCardCanvas2());
        }
        else if (60 <= dashPoint && dashPoint < 80 && CardSelect2 == false)
        {
            CardSelect2 = true;
            StartCoroutine(ShowCardCanvas1());
        }
        else if (40 <= dashPoint && dashPoint < 60 && CardSelect3 == false)
        {
            CardSelect3 = true;
            StartCoroutine(ShowCardCanvas1());
        }
        else if (20 <= dashPoint && dashPoint < 40 && CardSelect4 == false)
        {
            CardSelect4 = true;
            StartCoroutine(ShowCardCanvas1());
        }

        DashGage.value = dashPoint;

        // 100を超えた場合、ゲージリセット
        if (dashPoint >= maxDashPoint)
        {
            CountGauge++;
            dashPoint = 0;
            CardSelect1 = false;
            CardSelect2 = false;
            CardSelect3 = false;
            CardSelect4 = false;
            ResetGaugeImages();
            UpdateCountText();
        }

        // 300を超えた場合、ゲームオーバー画面へ遷移
        if (CountGauge == 3)
        {
            GameOver();
        }

        UpdateImageGauges();

        if (gameManager.isPlayerTurn && !saikorotyu)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EndTurnWithCardDisplay();
            }
        }
    }
    private void LimitDiceRoll()
    {
        // PlayerSaikoroスクリプトのインスタンスを取得
        PlayerSaikoro playerSaikoro = FindObjectOfType<PlayerSaikoro>();
        if (playerSaikoro != null)
        {
            playerSaikoro.SetDiceRollRange(1, 3);
        }
    }
    private void HideCardCanvases()
    {
        if (CardCanvas1 != null)
        {
            CardCanvas1.SetActive(false);
        }
        if (CardCanvas2 != null)
        {
            CardCanvas2.SetActive(false);
        }
        Time.timeScale = 1;
    }
    private void EndTurnWithCardDisplay()
    {
        if (!gameManager.isPlayerTurn) return;
    }

    public void IncreaseDashPointPerTurn()
    {
        dashPoint = Mathf.Min(dashPoint + dashIncreasePerTurn, maxDashPoint);
        DashGage.value = dashPoint;
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
            yield return new WaitForSeconds(1.0f);
            Time.timeScale = 0;
        }
    }

    public IEnumerator ShowCardCanvas1()
    {
        if (CardCanvas1 != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CardCanvas1.SetActive(true);
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

    public void ExtraButtonAction()
    {
    }

    public void HideCardCanvasAndModifyDashIncrease()
    {
        dashIncreasePerTurn += master_Curse.CurseSheet[1].TurnIncrease;
        Time.timeScale = 1;
    }

    public void CursegiveButtonAction()
    {
        dashPoint = Mathf.Min(dashPoint + 5, maxDashPoint);
        DashGage.value = dashPoint;
        Time.timeScale = 1;
    }

    private void UpdateCountText()
    {
        if (countText != null)
        {
            if (CountGauge == 2)
            {
                countText.text = "呪";
            }
            else
            {
                countText.text = (3 - CountGauge).ToString();
            }
        }
    }

    private void GameOver()
    {
    }
    private void ApplyEyeEffect()
    {
        if (eyeEffectController != null)
        {
            eyeEffectController.ApplyEyeEffect();
        }
    }
}
