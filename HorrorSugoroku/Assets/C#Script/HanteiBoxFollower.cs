using UnityEngine;

public class HanteiBoxFollower : MonoBehaviour
{
    public GameObject player;      // �v���C���[�i�ʒu�̊�j
    public GameObject hanteiBox;   // ����{�b�N�X
    public float forwardDistance = 2.0f;  // ���_�̑O���ɏo������
    public float heightOffset = -1.0f;    // ��������

    void Update()
    {
        if (player != null && hanteiBox != null && Camera.main != null)
        {
            // ���_�����̑O�ɏo��
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0; // �������������Ɍ���i�K�v�Ȃ�j

            Vector3 basePosition = player.transform.position;
            Vector3 targetPosition = basePosition + cameraForward.normalized * forwardDistance;
            targetPosition.y += heightOffset;

            hanteiBox.transform.position = targetPosition;

            // �J�����Ɠ��������ɉ�]���������ꍇ�͂�����ǉ�
            hanteiBox.transform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }
    //    public GameObject player; // Player�I�u�W�F�N�g
    //    public GameObject hanteiBox; // HanteiBox�I�u�W�F�N�g
    //    public float heightOffset = -1.0f; // �����̃I�t�Z�b�g
    //    public float forwardOffset = 1.0f; // �O���̃I�t�Z�b�g

    //    void Update()
    //    {
    //        if (player != null && hanteiBox != null)
    //        {
    //            // Player�̈ʒu��HanteiBox��Ǐ]������
    //            Vector3 newPosition = player.transform.position;
    //            newPosition.y += heightOffset; // �����𒲐�
    //            newPosition += player.transform.forward * forwardOffset; // �O���ɃI�t�Z�b�g
    //            hanteiBox.transform.position = newPosition;
    //        }
    //    }
}