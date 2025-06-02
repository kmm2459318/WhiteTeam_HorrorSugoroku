using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrongboxController : MonoBehaviour
{
    public DiceController diceController; // DiceControllerへの参照
    public PlayerSaikoro playerSaikoro;
    [SerializeField] private Camera diceCamera;
    public GameObject textCanvas;
    public GameObject textbox;
    private bool thisBoxOn = false;
    public int OpenNumber = 0;
    public string itemToGiveName = ""; // 開いたときに得られるアイテム名（Inspectorで設定可能）
    private PlayerInventory playerInventory;
    public Animator boxAnimator; // 箱のアニメーター
    public TextMeshProUGUI messageText;
    public GameManager gameManager; // ← GameManager 参照を追加
    [SerializeField] private CurseSlider curseSlider;
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        gameManager = FindObjectOfType<GameManager>(); // ← GameManagerを探す

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory が見つかりません！");
        }
        if (gameManager == null)
        {
            Debug.LogError("GameManager が見つかりません！");
        }
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
                    messageText.text = "————祝福のカギは開かれた。"; // ← 追加

                    if (boxAnimator != null)
                    {
                        boxAnimator.SetBool("Door", true);
                    }

                    if (playerInventory != null && !string.IsNullOrEmpty(itemToGiveName))
                    {
                        string uniqueID = itemToGiveName + "_" + Time.time;
                        playerInventory.AddItem(itemToGiveName, uniqueID);
                        Debug.Log($"祝福箱から「{itemToGiveName}」を入手しました！");
                        messageText.text = $"祝福箱から「{itemToGiveName}」を入手しました！"; // ← 追加
                                                                               // 人形のときだけ GameManager に登録
                        if (itemToGiveName == "人形" && gameManager != null)
                        {
                            gameManager.Doll++; // ← 人形を1つ追加
                            Debug.Log("人形が GameManager に追加されました。現在の数: " + gameManager.Doll);
                        }
                    }

                    diceController.boxDice = false;
                }
                else
                {
                    Debug.Log("残念無念、また来世ー！");
                    messageText.text = "残念無念、また来世ー！"; // ← 追加
                    OpenNumber--;
                    gameObject.tag = "Strongbox";

                    StartCoroutine(DashPoint());
                }

                diceController.strongBoxResult = 0;
                thisBoxOn = false;
                //textCanvas.SetActive(false);
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
            diceCamera.enabled = true; // 🎲 カメラ表示
                                                      // playerSaikoro.ResetDiceState(); ← ここで即リセットは NG
        }

        if (!thisBoxOn)
        {
            diceCamera.enabled = true;
            diceController.ResetDiceState();
            diceController.boxDice = true;
            thisBoxOn = true;
            textCanvas.SetActive(true);
            textbox.GetComponent<TextMeshProUGUI>().text = "サイコロを振り、" + OpenNumber + "以上で開錠。";
        }
    }

    public void FalseCanvas()
    {
        textCanvas.SetActive(false);
    }
}
