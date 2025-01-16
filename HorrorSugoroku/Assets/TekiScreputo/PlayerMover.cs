using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed = 5f;        // �v���C���[�̈ړ����x
    private Vector3 targetPosition;    // ���̈ړ���
    private bool isMoving = false;     // �ړ����t���O

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = transform.position; // �����ʒu
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ����ł���Έړ����������s
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // �ړ��������������~
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                Debug.Log("�ړ�����");
            }
        }

        // �L�[���͂Ńe�X�g�ړ�
        if (!isMoving && Input.GetKeyDown(KeyCode.Space)) // Space�L�[�ňړ�
        {
            MoveToNextCell(Vector3.forward); // ���ɑO����1�}�X�ړ�
        }
    }
    public void MoveToNextCell(Vector3 direction)
    {
        if (!isMoving)
        {
            targetPosition = transform.position + direction; // ���̖ڕW�n�_��ݒ�
            isMoving = true; // �ړ����t���O��ݒ�
        }
    }
}
