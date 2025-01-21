using TMPro; // TextMeshPro���g���ꍇ
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro�p�̃^�[�����\��
    private int currentTurn = 1; // ���݂̃^�[���ԍ�
    public PlayerSaikoro PlayerSaikoro;

    // ���̃^�[���ɐi�ޏ���
    public void NextTurn()
    {
        if (!PlayerSaikoro.idoutyu && !PlayerSaikoro.saikorotyu)
        {
            currentTurn++; // �^�[����i�߂�
            UpdateTurnText(); // UI�̃e�L�X�g���X�V
            PlayerSaikoro.DiceRoll();
        }
    }

    // �^�[���\�����X�V���郁�\�b�h
    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = "" + currentTurn; // �e�L�X�g�Ƀ^�[������\��
        }
    }

    // �Q�[���J�n���ɏ�����
    private void Start()
    {
        UpdateTurnText(); // �����^�[���\��
    }
}