using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CurseSlider : MonoBehaviour
{
    [SerializeField] Slider DashGage;
    [SerializeField] SceneChanger3D sceneChanger;
    [SerializeField] GameObject CardCanvas;
    [SerializeField] Button extraButton;
    [SerializeField] Button hideButton;
    [SerializeField] Button cursegiveButton;

    [SerializeField] private Master_Curse master_Curse;
    [SerializeField] private Image[] ImageGages; // 画像ゲージ (2〜6)

    public float maxDashPoint = 100;
    public float dashIncreasePerTurn = 5;

    int CountGauge = 0;
    float dashPoint = 0;
    public GameManager gameManager;
    public TurnManager turnManager;
    private bool saikorotyu;

    private int nextShowCardThreshold = 20; // カード表示の閾値（20,40,60,80,100）

    void Start()
    {
        DashGage.maxValue = maxDashPoint;
        DashGage.value = dashPoint;
        ResetGaugeImages();

        if (extraButton != null)
        {
            extraButton.onClick.RemoveAllListeners();
            extraButton.onClick.AddListener(() => { ExtraButtonAction(); HideCardCanvas(); });
        }
        if (hideButton != null)
        {
            hideButton.onClick.RemoveAllListeners();
            hideButton.onClick.AddListener(() => { HideCardCanvasAndModifyDashIncrease(); HideCardCanvas(); });
        }
        if (cursegiveButton != null)
        {
            cursegiveButton.onClick.RemoveAllListeners();
            cursegiveButton.onClick.AddListener(() => { CursegiveButtonAction(); HideCardCanvas(); });
        }

        HideCardCanvas();
    }

    void Update()
    {
        DashGage.value = dashPoint;

        if (dashPoint >= maxDashPoint)
        {
            CountGauge++;
            dashPoint = 0;
            nextShowCardThreshold = 20; // リセット時に閾値もリセット
            ResetGaugeImages();
        }

        UpdateImageGauges();

        if (dashPoint >= nextShowCardThreshold)
        {
            StartCoroutine(ShowCardCanvas());
            nextShowCardThreshold += 20;
        }

        if (gameManager.isPlayerTurn && !saikorotyu)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EndTurnWithCardDisplay();
            }
        }
    }

    private void EndTurnWithCardDisplay()
    {
        // すでにターンが終了している場合は何もしない
        if (!gameManager.isPlayerTurn) return;
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
            float alpha = Mathf.Clamp01((dashPoint - min) / (max - min));

            Color color = ImageGages[i].color;
            color.a = alpha;
            ImageGages[i].color = color;
        }
    }

    private void ResetGaugeImages()
    {
        foreach (Image img in ImageGages)
        {
            Color color = img.color;
            color.a = 0;
            img.color = color;
        }
    }

    public IEnumerator ShowCardCanvas()
    {
        if (CardCanvas != null)
        {
            CardCanvas.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            Time.timeScale = 0; // **ゲームを停止**
        }
    }

    public void ExtraButtonAction()
    {
        Debug.Log("Extra Button Clicked!");
    }

    public void HideCardCanvas()
    {
        if (CardCanvas != null)
        {
            CardCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void HideCardCanvasAndModifyDashIncrease()
    {
        dashIncreasePerTurn += master_Curse.CurseSheet[1].TurnIncrease;
        Debug.Log("[CurseSlider] Dash Increase Per Turn set to: " + dashIncreasePerTurn);
        Time.timeScale = 1;
    }

    public void CursegiveButtonAction()
    {
        dashPoint = Mathf.Min(dashPoint + 15, maxDashPoint);
        DashGage.value = dashPoint;
        Debug.Log("[CursegiveButton] After: DashPoint = " + dashPoint);
        Time.timeScale = 1;
    }
}
