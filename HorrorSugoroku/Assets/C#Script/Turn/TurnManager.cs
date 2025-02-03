using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText;
    private int currentTurn = 0;
    public bool turnStay = false;

    public PlayerSaikoro playerSaikoro;
    public CurseSlider curseSlider;
    public FlashlightController flashlightController;

    public GameObject currentEnemy; // 現在のエネミーオブジェクト
    public GameObject newEnemyPrefab; // 新しいエネミーのプレハブ

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

            // ターンが6に達したらエネミーのモデルを変更
            if (currentTurn == 6)
            {
                ChangeEnemyModel();
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

    private void ChangeEnemyModel()
    {
        if (currentEnemy != null && newEnemyPrefab != null)
        {
            Vector3 enemyPosition = currentEnemy.transform.position;
            Quaternion enemyRotation = currentEnemy.transform.rotation;
            Destroy(currentEnemy);
            currentEnemy = Instantiate(newEnemyPrefab, enemyPosition, enemyRotation);
            Debug.Log("[TurnManager] Enemy model changed to new model.");
        }
        else
        {
            Debug.LogError("[TurnManager] Current enemy or new enemy prefab is not assigned!");
        }
    }
}