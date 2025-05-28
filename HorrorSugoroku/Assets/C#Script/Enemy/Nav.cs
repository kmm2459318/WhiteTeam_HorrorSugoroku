using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Nav : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;

    public PlayerSaikoro playerSaikoro;

    //敵の動作の種類
    public enum EnemyType { Chase, Randommove}
    public EnemyType enemyType;

    public int RandomMin = 4; //ランダム移動の最小移動数
    public int RandomMax = 10; //ランダム移動の最大移動数

    private Vector3 lastPlayerPosition;


    private void Start()
    {
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        agent.avoidancePriority = 50;
        lastPlayerPosition = target.position;

        // NavMeshAgentの自動移動・回転制御を無効にする
        agent.updatePosition = false;

        agent.avoidancePriority = 0;
        agent.radius = 0.1f;
    }

    private void Update()
    {
        // 自分で位置を更新（他エージェントを完全に無視してすり抜ける）
        transform.position = Vector3.MoveTowards(transform.position, agent.nextPosition, agent.speed * Time.deltaTime);

        switch (enemyType)
        {
            case EnemyType.Chase:
                ChasePlayer();
                break;
            case EnemyType.Randommove:
                Randommove();
                break;
        }

        //プレイヤーの位置の更新
        lastPlayerPosition = target.position;
    }

    //プレイヤーを追いかける動作
    void ChasePlayer()
    {
        if (agent.enabled && agent.isOnNavMesh)
        {
            Debug.Log("プレイヤーを追いかける");
            agent.SetDestination(target.position);
        }
    }


    //ランダム移動
    void Randommove()
    {
        Debug.Log("ランダム移動");

        // すでに移動中なら何もしない
        if (agent.pathPending || agent.remainingDistance > 0.1f) return;

        // 進行可能な方角の候補（上下左右）
        Vector3[] directions = new Vector3[]
        {
        Vector3.forward,   // +Z（前）
        Vector3.back,      // -Z（後）
        Vector3.left,      // -X（左）
        Vector3.right      // +X（右）
        };

        // ランダムな方向を選ぶ
        Vector3 chosenDir = directions[Random.Range(0, directions.Length)];

        // ランダムな移動数
        int steps = Random.Range(RandomMin, RandomMax);

        // 進む方向と距離を計算
        Vector3 targetPos = transform.position + chosenDir * steps;

        // NavMesh 上の位置をサンプリングして目的地を設定
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPos, out hit, 1.0f, NavMesh.AllAreas))
        {
            if (agent.enabled && agent.isOnNavMesh)
            {
                agent.SetDestination(hit.position);
            }
        }

    }

   
}
