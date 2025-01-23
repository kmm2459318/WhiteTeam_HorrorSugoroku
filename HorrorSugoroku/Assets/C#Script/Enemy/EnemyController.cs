using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �u�����h�c���[�̃p�����[�^�������_���ɐݒ�
        animator.SetFloat("IdleIndex", Random.Range(0, 3));

        // �ړ����̃A�j���[�V��������
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}