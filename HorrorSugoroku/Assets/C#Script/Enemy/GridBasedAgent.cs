using UnityEngine;
using UnityEngine.AI;

public class GridBasedAgent : MonoBehaviour
{
    /*public Transform target; // �^�[�Q�b�g�I�u�W�F�N�g
    public float waitTime = 1.0f; // �e�}�X�ł̑ҋ@����
    public float gridSize = 1.0f; // �}�X�ڂ̃T�C�Y

    private NavMeshAgent agent;
    private Queue<Vector3> pathPoints; // �}�X�ڂ��Ƃ̌o�H
    private bool isWaiting = false;
    public float arrivalThreshold = 0.2f; // �}�X�ւ̓��B�Ƃ݂Ȃ�����

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CalculateOptimizedGridPath(); // �ŒZ�o�H���v�Z
    }

    void Update()
    {
        if (!isWaiting && pathPoints != null && pathPoints.Count > 0)
        {
            Vector3 nextPoint = pathPoints.Peek(); // ���̃}�X�̈ʒu���m�F

            // Y���𖳎����ċ������v�Z
            Vector2 currentPos2D = new Vector2(transform.position.x, transform.position.z);
            Vector2 nextPoint2D = new Vector2(nextPoint.x, nextPoint.z);

            // ���݂̈ʒu���ړI�̃}�X�ɓ��B���Ă��邩
            if (Vector2.Distance(currentPos2D, nextPoint2D) < arrivalThreshold)
            {
                pathPoints.Dequeue(); // ���B�����}�X���폜
                StartCoroutine(WaitAndMoveToNextPoint()); // 1�}�X���Ƃɒ�~
            }
            else
            {
                agent.destination = nextPoint; // ���̃}�X�ֈړ�
            }
        }
    }

    // �^�[�Q�b�g�܂ł̍ŒZ�o�H��X��Z�ɕ������Čv�Z
    private void CalculateOptimizedGridPath()
    {
        pathPoints = new Queue<Vector3>();

        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.position;

        while (Mathf.Abs(targetPos.x - currentPos.x) > gridSize || Mathf.Abs(targetPos.z - currentPos.z) > gridSize)
        {
            // X������Z�����̋������r���āA�Z������D��
            if (Mathf.Abs(targetPos.x - currentPos.x) > Mathf.Abs(targetPos.z - currentPos.z))
            {
                // X�����Ɉړ�
                currentPos = new Vector3(
                    currentPos.x + Mathf.Sign(targetPos.x - currentPos.x) * gridSize,
                    currentPos.y,
                    currentPos.z
                );
            }
            else
            {
                // Z�����Ɉړ�
                currentPos = new Vector3(
                    currentPos.x,
                    currentPos.y,
                    currentPos.z + Mathf.Sign(targetPos.z - currentPos.z) * gridSize
                );
            }

            pathPoints.Enqueue(currentPos); // �L���[�Ɍ��݈ʒu��ǉ�
        }
    }

    // �e�}�X�Œ�~���đҋ@����R���[�`��
    private IEnumerator WaitAndMoveToNextPoint()
    {
        isWaiting = true;
        agent.isStopped = true; // �G�[�W�F���g���~
        yield return new WaitForSeconds(waitTime); // �w��̎��ԑҋ@
        agent.isStopped = false; // �G�[�W�F���g�̈ړ����ĊJ
        isWaiting = false;
    }*/
}
