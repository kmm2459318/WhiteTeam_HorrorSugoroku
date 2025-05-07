using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public Transform player; // �v���C���[��Transform
    public NavMeshAgent agent; // NavMeshAgent�R���|�[�l���g
    public enum EnemyType { Chase, Predict, Avoid }
    public EnemyType enemyType;

    public float predictionTime = 1.5f; // �\������
    public float keepDistance = 5f; // �v���C���[�Ƃ̋�����ۂ��߂̋���

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(agent == null) agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Chase:
                ChasePlayer();
                break;
            case EnemyType.Predict:
                PredictPlayer();
                break;
            case EnemyType.Avoid:
                KeepDistance();
                break;
        }
    }

    //�v���C���[��ǂ������铮��
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    //
    void PredictPlayer()
    {

    }

    void KeepDistance()
    {

    }
}
