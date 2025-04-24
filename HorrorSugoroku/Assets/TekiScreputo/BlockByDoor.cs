using UnityEngine;

public class BlockByDoor : MonoBehaviour
{
    public float detectionDistance = 2f; // ���m���鋗��
    public LayerMask obstacleLayer; // Door��Wall�̃��C���[

    public PlayerSaikoro movementScript;
    public Transform cameraTransform; // �J������Transform

    void Update()
    {
        // �J�����̌����Ă������Ray���΂�
        Vector3 direction = cameraTransform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance, obstacleLayer))
        {
            if (hit.collider.CompareTag("Door") || hit.collider.CompareTag("Wall"))
            {
                // Door��Wall���O���ɂ���ꍇ�A�ړ����~�߂�
                movementScript.enabled = false;
                Debug.DrawRay(transform.position, direction * detectionDistance, Color.red);
                return;
            }
        }

        // ��Q�����Ȃ���Έړ��ł���
        movementScript.enabled = true;
        Debug.DrawRay(transform.position, direction * detectionDistance, Color.green);
    }
}
