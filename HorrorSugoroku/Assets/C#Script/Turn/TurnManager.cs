using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText;
    private int currentTurn = 0;
    public bool turnStay = false;

    public PlayerSaikoro playerSaikoro;
    public CurseSlider curseSlider; 

    public FlashlightController flashlightController; // �����d���R���g���[���[���Q��

    // ���̃^�[���ɐi�ޏ���
    public void NextTurn()
    {
        if (!turnStay)
        {
            turnStay = true;
            currentTurn++;
            PlayerPrefs.SetInt("Turn", currentTurn);
            UpdateTurnText();
            playerSaikoro.DiceRoll();
            // �����d���̃^�[���i�s�������Ăяo��
            if (flashlightController != null)
            {
                flashlightController.OnTurnAdvanced();
            }
            if (curseSlider != null)
            {
                curseSlider.IncreaseDashPointPerTurn();
                Debug.Log("[TurnManager] IncreaseDashPointPerTurn() called.");
            }
            else
            {
                Debug.LogError("[TurnManager] CurseSlider is not assigned!");
            }
        }
    }

    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = "Turn: " + currentTurn;
        }
    }

    private void Start()
    {
        UpdateTurnText();
        PlayerPrefs.SetInt("Turn", 0);
    }
}
