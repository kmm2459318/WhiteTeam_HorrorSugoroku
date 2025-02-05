using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public GameObject player; // �v���C���[�̃Q�[���I�u�W�F�N�g
    public GameObject frontCollider; // �G�l�~�[�̐��ʂɔz�u���ꂽBoxCollider�I�u�W�F�N�g
    public LayerMask wallLayer; // �ǂ̃��C���[
    private bool discovery = false;
    private Vector3 moveDirection;
    private Animator animator; // �A�j���[�^�[�̎Q��

    void Start()
    {
        animator = GetComponent<Animator>(); // �A�j���[�^�[�R���|�[�l���g���擾
    }

    void Update()
    {
        if (discovery)
        {
            // �v���C���[�̕����Ɍ���
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // frontCollider���v���C���[�̕����Ɍ���
            frontCollider.transform.rotation = Quaternion.Slerp(frontCollider.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            // �ړ������Ɍ���
            if (moveDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                animator.SetBool("isRunning", true); // Run�A�j���[�V�������Đ�
                animator.SetBool("isIdle", false); // Idle�A�j���[�V�������~
            }
            else
            {
                animator.SetBool("isRunning", false); // Run�A�j���[�V�������~
                animator.SetBool("isIdle", true); // Idle�A�j���[�V�������Đ�
            }

            // frontCollider���ړ������Ɍ���
            frontCollider.transform.rotation = Quaternion.Slerp(frontCollider.transform.rotation, transform.rotation, Time.deltaTime * 5f);

            // �ǂɓ��������ꍇ�ɕ������C��
            bool frontHit = Physics.CheckBox(frontCollider.transform.position, frontCollider.transform.localScale / 2, frontCollider.transform.rotation, wallLayer);

            if (frontHit)
            {
                moveDirection = -moveDirection; // �����𔽓]
                Debug.Log("Wall detected at front, changing direction to: " + moveDirection);
            }
        }
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