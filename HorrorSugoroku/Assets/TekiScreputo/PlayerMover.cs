using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // �T�C�R���X�N���v�g
    private GridCell currentCell;       // �v���C���[���ړ����������Z��
    private GridCell targetCell;        // �v���C���[�����ɓ��B����Z��
    private bool wasMoving = false;     // �O��̈ړ����

    private GameObject detectionBox;    // �l�p���I�u�W�F�N�g

    void Start()
    {
        // �K�v�ȃX�N���v�g�������擾
        if (playerSaikoro == null)
        {
            playerSaikoro = GetComponent<PlayerSaikoro>();
        }

        // �l�p���I�u�W�F�N�g���쐬���ăv���C���[�ɃA�^�b�`
        detectionBox = new GameObject("DetectionBox");
        detectionBox.transform.SetParent(transform);
        detectionBox.transform.localPosition = Vector3.zero;
        detectionBox.transform.localScale = new Vector3(3, 3, 3); // �T�C�Y�𒲐��ς�
        BoxCollider boxCollider = detectionBox.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        detectionBox.tag = "DetectionBox"; // �^�O��ݒ�
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
            targetCell = cell; // ���ɓ��B����Z�����^�[�Q�b�g�Z���Ƃ��ċL�^
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