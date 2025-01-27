using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �v���C���[�Ƃ̋������v�Z
        float distance = Vector3.Distance(transform.position, player.position);

        // �v���C���[��ǂ�������
        if (distance > 1f)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // �ړ�����run1�A�j���[�V�������Đ�
            animator.SetBool("isRunning", true);
        }
        else
        {
            // ��~���͑ҋ@���[�V�����ɖ߂�
            animator.SetBool("isRunning", false);
        }
    }
}