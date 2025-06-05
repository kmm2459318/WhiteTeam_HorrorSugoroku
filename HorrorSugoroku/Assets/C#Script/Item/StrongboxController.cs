using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrongboxController : MonoBehaviour
{
    public DiceController diceController;
    public PlayerSaikoro playerSaikoro;
    [SerializeField] private Camera diceCamera;
    public GameObject textCanvas;
    public GameObject textbox;
    private bool thisBoxOn = false;
    public int OpenNumber = 0;
    public string itemToGiveName = "";
    private PlayerInventory playerInventory;
    public Animator boxAnimator;
    public TextMeshProUGUI messageText;
    public GameManager gameManager;
    [SerializeField] private CurseSlider curseSlider;

    private float messageDisplayDuration = 3f; // ← Inspectorで調整可能な表示時間

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        gameManager = FindObjectOfType<GameManager>();

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory が見つかりません！");
        }
        if (gameManager == null)
        {
            Debug.LogError("GameManager が見つかりません！");
        }

        textCanvas.SetActive(false);
    }

    void Update()
    {
        if (thisBoxOn)
        {
            gameObject.tag = "Untagged";

            if (diceController.strongBoxResult != 0)
            {
                textCanvas.SetActive(true);

                if (OpenNumber <= diceController.strongBoxResult)
                {
                    Debug.Log("————祝福のカギは開かれた。");
                    messageText.text = "————祝福のカギは開かれた。";
                    StartCoroutine(HideTextCanvasAfterDelay());

                    if (boxAnimator != null)
                    {
                        boxAnimator.SetBool("Door", true);
                    }

                    if (playerInventory != null && !string.IsNullOrEmpty(itemToGiveName))
                    {
                        string uniqueID = itemToGiveName + "_" + Time.time;
                        playerInventory.AddItem(itemToGiveName, uniqueID);
                        Debug.Log($"祝福箱から「{itemToGiveName}」を入手しました！");
                        messageText.text = $"宝箱から「{itemToGiveName}」を入手した";
                        StartCoroutine(HideTextCanvasAfterDelay());
                        textCanvas.SetActive(true);

                        if (itemToGiveName == "人形" && gameManager != null)
                        {
                            gameManager.Doll++;
                            Debug.Log("人形が GameManager に追加されました。現在の数: " + gameManager.Doll);
                        }
                    }

                    diceController.boxDice = false;
                }
                else
                {
                    Debug.Log("残念無念、また来世ー！");
                    messageText.text = "開錠失敗、呪いが増えた";
                    StartCoroutine(HideTextCanvasAfterDelay());
                    textCanvas.SetActive(true);

                    OpenNumber--;
                    gameObject.tag = "Strongbox";
                    StartCoroutine(DashPoint());
                }

                diceController.strongBoxResult = 0;
                thisBoxOn = false;
            }
        }
    }

    private IEnumerator DashPoint()
    {
        if ((curseSlider.dashPoint + 10) % 20 == 0)
        {
            yield return new WaitForSeconds(0.51f);
        }

        curseSlider.DecreaseDashPoint(10);
        diceController.boxDice = false;
    }

    public void StrongBoxDiceOn()
    {
        if (!diceController.boxDice)
        {
            diceController.boxDice = true;
            diceCamera.enabled = true;
        }

        if (!thisBoxOn)
        {
            diceCamera.enabled = true;
            diceController.ResetDiceState();
            diceController.boxDice = true;
            thisBoxOn = true;
            textCanvas.SetActive(true);
            Debug.Log("textCanvas表示");
            textbox.GetComponent<TextMeshProUGUI>().text = "サイコロを振り、" + OpenNumber + "以上で開錠。";
        }
    }

    public void FalseCanvas()
    {
        textCanvas.SetActive(false);
    }

    private IEnumerator HideTextCanvasAfterDelay()
    {
        yield return new WaitForSeconds(messageDisplayDuration);
        textCanvas.SetActive(false);
    }
}
