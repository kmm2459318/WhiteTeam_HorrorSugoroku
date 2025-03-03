using UnityEngine;

public class BloclByDoor : MonoBehaviour
{
    public float detectionDistance = 2f; // ���m���鋗��
    public LayerMask doorLayer; // Door�I�u�W�F�N�g�̃��C���[

    private PlayerSaikoro movementScript;
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
        movementScript = GetComponent<PlayerSaikoro>();
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�̑O����Ray���΂�
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance, doorLayer))
        {
            if (hit.collider.CompareTag("Door"))
            {
                // Door���O���ɂ���ꍇ�A�ړ����~�߂�
                movementScript.enabled = false;
                return;
            }
        }  movementScript.enabled = true;
    }
  
}
