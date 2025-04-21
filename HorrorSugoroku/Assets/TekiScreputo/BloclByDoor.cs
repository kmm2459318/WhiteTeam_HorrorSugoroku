using UnityEngine;

public class BlockByDoor : MonoBehaviour
{
    public float detectionDistance = 2f; // ���m���鋗��
    public LayerMask doorLayer; // Door�I�u�W�F�N�g�̃��C���[

    public PlayerSaikoro movementScript;
    public Transform cameraTransform; // �J������Transform

    void Update()
    {
        // �J�����̌����Ă������Ray���΂�
        Vector3 direction = cameraTransform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance, doorLayer))
        {
            if (hit.collider.CompareTag("Door"))
            {
                // Door���O���ɂ���ꍇ�A�ړ����~�߂�
                movementScript.enabled = false;
                Debug.DrawRay(transform.position, direction * detectionDistance, Color.red);
                return;
            }
        }

        // Door���Ȃ���Έړ��ł���
        movementScript.enabled = true;
        Debug.DrawRay(transform.position, direction * detectionDistance, Color.green);
    }
}
