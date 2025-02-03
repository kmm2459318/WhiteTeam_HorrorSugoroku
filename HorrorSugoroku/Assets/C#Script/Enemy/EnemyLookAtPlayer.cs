using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform front;
    public Transform back;
    public Transform right;
    public Transform left;
    public LayerMask wallLayer; // �ǂ̃��C���[
    private bool discovery = false;
    private Vector3 moveDirection;
    private float directionChangeCooldown = 1.0f; // �����ύX�̃N�[���_�E������
    private float lastDirectionChangeTime;

    void Update()
    {
        //if (discovery)
        //{
            // �v���C���[�̕����Ɍ���
            //Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            //.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        //}
        /*else
        {
            // �ړ������Ɍ���
            if (moveDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // �ǂɓ��������ꍇ�ɕ������C��
            bool frontHit = Physics.CheckSphere(front.position, 0.5f, wallLayer);
            bool rightHit = Physics.CheckSphere(right.position, 0.5f, wallLayer);
            bool leftHit = Physics.CheckSphere(left.position, 0.5f, wallLayer);

            if (Time.time > lastDirectionChangeTime + directionChangeCooldown)
            {
                if (frontHit)
                {
                    moveDirection = -moveDirection; // �����𔽓]
                    lastDirectionChangeTime = Time.time;
                    Debug.Log("Wall detected at front, changing direction to: " + moveDirection);
                }
                else if (!rightHit || !leftHit)
                {
                    moveDirection = -moveDirection; // �����𔽓]
                    lastDirectionChangeTime = Time.time;
                    Debug.Log("Wall detected at right or left, changing direction to: " + moveDirection);
                }
            }
        }*/
    }

    public void SetDiscovery(bool isDiscovered)
    {
        discovery = isDiscovered;
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
        Debug.Log("Move direction set to: " + direction);
    }
}