using UnityEngine;
using System.Collections; // �ǉ�

public class EnemyController : MonoBehaviour
{
    private Animator animator; // �A�j���[�^�[�R���|�[�l���g
    private bool isMoving; // �ړ������ǂ����������t���O
    public GameManager gameManager; // �Q�[���}�l�[�W���[�ւ̎Q��
    public EnemySaikoro enemySaikoro; // �G�L�����N�^�[�̃T�C�R���ւ̎Q��
    int mp = 0; // �}�b�v�s�[�X�̐�
    float mo = 3f; // �ړ����x
    int id = 1; // �G�L�����N�^�[��ID

    void Start()
    {
        animator = GetComponent<Animator>(); // �A�j���[�^�[�R���|�[�l���g�̎擾

        if (enemySaikoro == null)
        {
            Debug.LogError("enemySaikoro is not assigned.");
        }

        // AttackRoutine�R���[�`�����J�n
        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        if (isMoving)
        {
            animator.SetBool("isRunning", true); // �ړ����̏ꍇ�A�A�j���[�V�����𑖂��Ԃɐݒ�
        }
        else
        {
            animator.SetBool("isRunning", false); // �ړ����Ă��Ȃ��ꍇ�A�A�j���[�V�������~��Ԃɐݒ�
        }

        mp = gameManager.mapPiece; // �Q�[���}�l�[�W���[����}�b�v�s�[�X�̐����擾

        if (mp < 3) // �P�i�K
        {
            mo = 6f;
            id = 1;
        }
        else if (mp < 6) // �Q�i�K
        {
            mo = 8f;
            id = 2;
            enemySaikoro.skill1 = true; // �X�L��1��L���ɂ���
        }
        else if (mp < 9) // �R�i�K
        {
            mo = 10f;
            id = 3;
            enemySaikoro.skill2 = true; // �X�L��2��L���ɂ���
        }
        else // �ڂ��i��
        {
            mo = 12f;
            id = 4;
        }

        enemySaikoro.mokushi = mo; // �T�C�R���̈ړ����x��ݒ�
        enemySaikoro.idoukagen = id; // �T�C�R����ID��ݒ�
    }

    public void SetMovement(bool moving)
    {
        isMoving = moving; // �ړ������ǂ�����ݒ�
        if (animator != null)
        {
            animator.SetBool("is Running", moving); // �ړ����̏ꍇ�A�A�j���[�V�����𑖂��Ԃɐݒ�
        }
        Debug.Log("SetMovement called with: " + moving); // �f�o�b�O���O�Ɉړ���Ԃ��o��
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(120f); // 2���ҋ@
            if (animator != null && !animator.GetBool("is Running")) // is Running��false�̏ꍇ�̂�Attack�𔭓�
            {
                animator.SetBool("Attack", true); // Attack�A�j���[�V�������J�n
                yield return new WaitForSeconds(1f); // �A�^�b�N���[�V�����̍Đ����Ԃ�ҋ@�i�K�X�����j
                animator.SetBool("Attack", false); // Attack�A�j���[�V�������I��
                animator.SetBool("is Running", false); // Idle��Ԃɖ߂�
            }
        }
    }
}