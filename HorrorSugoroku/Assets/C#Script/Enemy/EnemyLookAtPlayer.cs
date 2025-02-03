using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public LayerMask wallLayer; // �ǂ̃��C���[
    private bool discovery = false;
    private Vector3 moveDirection;
    private float directionChangeCooldown = 1.0f; // �����ύX�̃N�[���_�E������
    private float lastDirectionChangeTime;
    private Animator animator; // �A�j���[�^�[�̎Q��

    void Start()
    {
        animator = GetComponent<Animator>(); // �A�j���[�^�[�R���|�[�l���g���擾
    }

    void Update()
    {
        if (discovery)
        {
            // �v���C���[�̕����Ɍ����i�폜�j
        }
        else
        {
            // �ړ������Ɍ���
            if (moveDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                animator.SetBool("isRunning", true); // Run�A�j���[�V�������Đ�
            }
            else
            {
                animator.SetBool("isRunning", false); // Run�A�j���[�V�������~
            }

            // �ǂɓ��������ꍇ�ɕ������C���i�폜�j
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