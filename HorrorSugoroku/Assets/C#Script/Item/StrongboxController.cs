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
                if (OpenNumber <= diceController.strongBoxResult)
                {
                    Debug.Log("————祝福のカギは開かれた。");
                    gameObject.tag = "Untagged";
                    // ✅ アニメーション再生
                    if (boxAnimator != null)
                    {
                        boxAnimator.SetTrigger("OrenTrigger");
                    }
                    // アイテムを付与
                    if (playerInventory != null && !string.IsNullOrEmpty(itemToGiveName))
                    {
                        string uniqueID = itemToGiveName + "_" + Time.time;
                        playerInventory.AddItem(itemToGiveName, uniqueID);
                        Debug.Log($"祝福箱から「{itemToGiveName}」を入手しました！");
                    }
                }
                else
                {
                    Debug.Log("残念無念、また来世ー！");
                    OpenNumber--;
                }

                diceController.strongBoxResult = 0;
                diceController.boxDice = false;
                thisBoxOn = false;
                textCanvas.SetActive(false);
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
}
