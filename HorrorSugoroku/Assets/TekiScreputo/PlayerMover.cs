using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // �T�C�R���X�N���v�g
    public GridCell currentCell;        // �v���C���[���ړ����������Z���i�C���X�y�N�^�[����ݒ�\�j
    public GameObject detectionBox;     // �l�p���I�u�W�F�N�g
    private GridCell targetCell;        // �v���C���[�����ɓ��B����Z��
    private bool wasMoving = false;     // �O��̈ړ����
    private HashSet<GridCell> visibleCells = new HashSet<GridCell>(); // �\������Ă���}�X�̃Z�b�g

    void Start()
    {
        // �K�v�ȃX�N���v�g�������擾
        if (playerSaikoro == null)
        {
            playerSaikoro = GetComponent<PlayerSaikoro>();
        }

        // ������ԂŌ��݂̃}�X��\��
        UpdateVisibleCells();
    }

    void Update()
    {
        // �v���C���[�̈ړ������������^�C�~���O���Ď�
        if (wasMoving && !playerSaikoro.idoutyu)
        {
            // �v���C���[�����S�Ɏ~�܂����}�X�ŃC�x���g�𔭉�
            if (targetCell != null)
            {
                currentCell = targetCell;
                TriggerCurrentCellEvent();
                targetCell = null; // �C�x���g���Ό�Ƀ^�[�Q�b�g�Z�������Z�b�g

                // ���݂̃}�X���X�V
                UpdateVisibleCells();
            }
        }

        // ��Ԃ��X�V
        wasMoving = playerSaikoro.idoutyu;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �l�p���I�u�W�F�N�g���}�X�ɐG�ꂽ�Ƃ��̏���
        if (other.gameObject == detectionBox)
        {
            GridCell cell = other.GetComponent<GridCell>();
            if (cell != null)
            {
                visibleCells.Add(cell);
                cell.SetVisibility(true);
                Debug.Log($"�^�[�Q�b�g�Z���ɓ��B: {cell.name}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �l�p���I�u�W�F�N�g���}�X���痣�ꂽ�Ƃ��̏���
        if (other.gameObject == detectionBox)
        {
            GridCell cell = other.GetComponent<GridCell>();
            if (cell != null)
            {
                visibleCells.Remove(cell);
                cell.SetVisibility(false);
                Debug.Log($"�^�[�Q�b�g�Z�����痣�ꂽ: {cell.name}");
            }
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

    private void UpdateVisibleCells()
    {
        // �S�Ẵ}�X���A�N�e�B�u�ɂ���
        GridCell[] allCells = FindObjectsOfType<GridCell>(true);
        foreach (GridCell cell in allCells)
        {
            cell.SetVisibility(false);
        }

        // ���ݕ\������Ă���}�X���ĕ\��
        foreach (GridCell cell in visibleCells)
        {
            cell.SetVisibility(true);
        }
    }
}