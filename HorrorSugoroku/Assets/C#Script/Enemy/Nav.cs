using UnityEngine;
using UnityEngine.AI;

public class Nav : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;

    //敵の動作の種類
    public enum EnemyType { Chase, Randommove }
    public EnemyType enemyType;

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
        }

        //プレイヤーの位置の更新
        lastPlayerPosition = target.position;
    }

    //プレイヤーを追いかける動作
    void ChasePlayer()
    {
        if (agent.enabled && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }
    }


    //ランダム移動
    void Randommove()
    {
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

        // ランダムなマス数（3～7マス）
        int steps = Random.Range(3, 8); // 3〜7の範囲

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
