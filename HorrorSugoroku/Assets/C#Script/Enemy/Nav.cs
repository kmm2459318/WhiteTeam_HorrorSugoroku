using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Nav : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;

    //敵の動作の種類
    public enum EnemyType { Chase, Randommove, KeepDistance }
    public EnemyType enemyType;

    public int RandomMin = 4; //ランダム移動の最小値
    public int RandomMax = 10; //ランダム移動の最大値

    private Vector3 lastPlayerPosition;


    private void Start()
    {
        lastPlayerPosition = target.position;
    }

    private void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Chase:
                ChasePlayer();
                break;
            case EnemyType.Randommove:
                Randommove();
                break;
            case EnemyType.KeepDistance:
                KeepDistance();
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

    //プレイヤーと一定の距離を保つ
    void KeepDistance()
    {
        Debug.Log("プレイヤーと一定の距離を保つ");

        if (!agent.enabled || !agent.isOnNavMesh) return;

        float desiredDistance = 5f; //保つ距離
        float minDistance = 2; //最小距離
        float maxDistance = 4; //許容距離

        // プレイヤーとの距離を計算
        float distance = Vector3.Distance(transform.position, target.position);

        //距離が適正なら動かない
        if (distance >= minDistance && distance <= maxDistance)
        {
            if (agent.hasPath)
            {
                agent.ResetPath(); // 目的地をリセット
            }
            return;
        }

        // 近すぎる or 離れすぎているときは常に再設定
        Vector3 direction = (distance < minDistance)
            ? (transform.position - target.position).normalized
            : (target.position - transform.position).normalized;


        Vector3 newPos = target.position + direction * desiredDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(newPos, out hit, 2.5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
