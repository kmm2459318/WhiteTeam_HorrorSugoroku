using UnityEngine;
using TMPro;  // TextMeshPro �p

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText;  // TextMeshPro �p�̃^�[�����\��
    private int currentTurn = 1;  // ���݂̃^�[���ԍ�

    public PlayerSaikoro playerSaikoro;  // �v���C���[�̃T�C�R���Ǘ��i���̃^�[���ɐi�ޏ����j

    // ���̃^�[���ɐi�ޏ���
    public void NextTurn()
    {
        currentTurn++;  // �^�[����i�߂�
        PlayerPrefs.SetInt("Turn", currentTurn);
        UpdateTurnText();  // UI�̃e�L�X�g���X�V����
    }


    // �^�[���\�����X�V���郁�\�b�h
    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = "Turn: " + currentTurn;  // �e�L�X�g�Ƀ^�[���ԍ���\��
        }
    }

    // �Q�[���J�n���ɏ�����
    private void Start()
    {
        UpdateTurnText();  // �����^�[���\��
    }
}
