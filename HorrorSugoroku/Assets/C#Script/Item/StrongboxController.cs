using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory が見つかりません！");
        }
    }
    void Update()
    {
        if (thisBoxOn)
        {
            if (diceController.strongBoxResult != 0)
            {
                textCanvas.SetActive(true);

                if (OpenNumber <= diceController.strongBoxResult)
                {
                    Debug.Log("————祝福のカギは開かれた。");
                    messageText.text = "————祝福のカギは開かれた。"; // ← 追加
                    gameObject.tag = "Untagged";

                    if (boxAnimator != null)
                    {
                        boxAnimator.SetTrigger("OrenTrigger");
                    }

                    if (playerInventory != null && !string.IsNullOrEmpty(itemToGiveName))
                    {
                        string uniqueID = itemToGiveName + "_" + Time.time;
                        playerInventory.AddItem(itemToGiveName, uniqueID);
                        Debug.Log($"祝福箱から「{itemToGiveName}」を入手しました！");
                        messageText.text = $"祝福箱から「{itemToGiveName}」を入手しました！"; // ← 追加
                    }
                }
                else
                {
                    Debug.Log("残念無念、また来世ー！");
                    messageText.text = "残念無念、また来世ー！"; // ← 追加
                    OpenNumber--;
                }

                diceController.strongBoxResult = 0;
                diceController.boxDice = false;
                thisBoxOn = false;
                //textCanvas.SetActive(false);
            }
        }
    }

    public void StrongBoxDiceOn()
    {
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
