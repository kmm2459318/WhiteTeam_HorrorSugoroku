using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // �T�C�R���X�N���v�g
    private GridCell currentCell;       // �v���C���[���ړ����������Z��
    private bool wasMoving = false;     // �O��̈ړ����

    void Start()
    {
        // �K�v�ȃX�N���v�g�������擾
        if (playerSaikoro == null)
        {
            playerSaikoro = GetComponent<PlayerSaikoro>();
        }
    }

    void Update()
    {
        // �v���C���[�̈ړ������������^�C�~���O���Ď�
        if (wasMoving && !playerSaikoro.idoutyu)
        {
            StartCoroutine(TriggerCurrentCellEventWithDelay(1.0f));
        }

        // ��Ԃ��X�V
        wasMoving = playerSaikoro.idoutyu;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���ʉ߂����Z�����L�^
        GridCell cell = other.GetComponent<GridCell>();
        if (cell != null)
        {
            currentCell = cell;
            Debug.Log($"�v���C���[�� {cell.name} �ɓ��B���܂����B");
        }
    }

    private IEnumerator TriggerCurrentCellEventWithDelay(float delay)
    {
        // �x����ҋ@
        yield return new WaitForSeconds(delay);

        // �C�x���g�𔭉�
        if (currentCell != null)
        {
            Debug.Log($"1�b��ɃC�x���g����: {currentCell.name}");
            currentCell.ExecuteEvent();
        }
        else
        {
            Debug.LogWarning("���݂̃Z�����ݒ肳��Ă��܂���B");
        }
    }
}
