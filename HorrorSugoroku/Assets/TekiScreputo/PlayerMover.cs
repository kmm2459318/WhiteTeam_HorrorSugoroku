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
            TriggerCurrentCellEvent();
            if (currentCell != null)
            {
                Debug.Log($"�v���C���[�� {currentCell.name} �ɓ��B���܂����B");
            }
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
        }
    }

    private void TriggerCurrentCellEvent()
    {
        // �C�x���g�𔭉�
        if (currentCell != null)
        {
            Debug.Log($"�C�x���g����: {currentCell.name}");
            currentCell.ExecuteEvent();
        }
        else
        {
            Debug.LogWarning("���݂̃Z�����ݒ肳��Ă��܂���B");
        }
    }
}