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
