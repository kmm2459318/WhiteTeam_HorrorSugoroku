using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public GameObject player; // �v���C���[�̃Q�[���I�u�W�F�N�g
    public LayerMask wallLayer; // �ǂ̃��C���[
    private bool discovery = false;
    private Vector3 moveDirection;
    private Animator animator; // アニメーターの参照
    private bool isMoving = false; // エネミーが移動中かどうかを示すフラグ
    //public Transform northTransform; // NorthオブジェクトのTransform

    void Start()
    {
        animator = GetComponent<Animator>(); // �A�j���[�^�[�R���|�[�l���g���擾
        if (animator == null)
        {
            //Debug.LogError("Animator component not found on the enemy object.");
        }

        /*if (northTransform == null)
        {
            Debug.LogError("North Transform is not assigned.");
        }*/
    }

    void Update()
    {
        if (discovery)
        {
            // �v���C���[�̕����Ɍ���
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            // �ړ������Ɍ���
            if (moveDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // �ǂɓ��������ꍇ�ɕ����𔽓]
            bool frontHit = Physics.CheckBox(transform.position, transform.localScale / 2, transform.rotation, wallLayer);

            if (frontHit)
            {
                moveDirection = -moveDirection; // �����𔽓]
                Debug.Log("Wall detected at front, changing direction to: " + moveDirection);
            }
        }

        // Northオブジェクトをエネミーの回転に追従させる
        /*if (northTransform != null)
        {
            northTransform.position = transform.position; // Northオブジェクトの位置をエネミーの位置に合わせる
            northTransform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Y軸のみで回転
        }*/

        // �G�l�~�[�̈ړ���ԂɊ�Â��ăA�j���[�V�����𐧌�
        isMoving = moveDirection != Vector3.zero; // �ړ��������[���łȂ��ꍇ�͈ړ����Ɣ��f
        //animator.SetBool("isRunning", isMoving);
        //animator.SetBool("isIdle", !isMoving);
    }

    public void SetDiscovery(bool isDiscovered)
    {
        discovery = isDiscovered;
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
        isMoving = moveDirection != Vector3.zero; // �ړ��������[���łȂ��ꍇ�͈ړ����Ɣ��f
        Debug.Log("Move direction set to: " + direction);
    }

    public void SetIsMoving(bool moving)
    {
        isMoving = moving;
    }
}