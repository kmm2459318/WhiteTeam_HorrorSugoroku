using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public Transform player; // プレイヤーのTransform
    public NavMeshAgent agent; // NavMeshAgentコンポーネント
    public enum EnemyType { Chase, Predict, Avoid }
    public EnemyType enemyType;

    public float predictionTime = 1.5f; // 予測時間
    public float keepDistance = 5f; // プレイヤーとの距離を保つための距離

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

    //プレイヤーを追いかける動作
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
