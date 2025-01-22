using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro�p�̃^�[�����\��
    public TMP_Text turnIndicatorText; // �V�����^�[���\���p�̃e�L�X�g
    private bool isPlayerTurn = true; // �v���C���[�̃^�[�����ǂ����������t���O

    public PlayerSaikoro playerSaikoro;
    public EnemySaikoro enemySaikoro;

    private void Start()
    {
        UpdateTurnText(); // �����^�[���\��
        playerSaikoro.StartRolling(); // �v���C���[�̃^�[�����J�n
    }

    public void NextTurn()
    {
        isPlayerTurn = !isPlayerTurn; // �^�[����؂�ւ���
        UpdateTurnText(); // UI�̃e�L�X�g���X�V

        if (isPlayerTurn)
        {
            playerSaikoro.StartRolling();
        }
        else
        {
            StartCoroutine(enemySaikoro.EnemyTurn());
        }
    }

    private void UpdateTurnText()
    {

        if (turnIndicatorText != null)
        {
            if (isPlayerTurn)
            {
                turnIndicatorText.text = "PlayerTurn"; // �v���C���[�̃^�[���\��
            }
            else
            {
                turnIndicatorText.text = "EnemyTurn"; // �G�l�~�[�̃^�[���\��
            }
        }
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}