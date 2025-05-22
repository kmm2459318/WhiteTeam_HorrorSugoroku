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
    public enum EnemyType { Chase, Randommove, KeepDistance }
    public EnemyType enemyType;

    public int RandomMin = 4; //ランダム移動の最小値
    public int RandomMax = 10; //ランダム移動の最大値

    public int minDistance = 3;     // 離れる距離
    public int desiredDistance = 5; // 理想の距離
    public int maxDistance = 7;     // 離れすぎる距離

    private bool isMovingForKeepDistance = false;// KeepDistanceの実行中フラグ

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

        // 目的地に到着したかチェックしてターン終了
        if (enemyType == EnemyType.KeepDistance && isMovingForKeepDistance)
        {
            if (!agent.pathPending && agent.remainingDistance <= 0.1f)
            {
                if (playerSaikoro != null)
                {
                    Debug.Log("KeepDistance の移動完了 → FinishTurn()");
                    playerSaikoro.FinishTurn();
                }
                isMovingForKeepDistance = false; // 移動完了フラグをリセット
            }
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

        float currentDistance = Vector3.Distance(transform.position, target.position);

        if (currentDistance >= minDistance && currentDistance <= maxDistance)
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
            }
            return;
        }

        Vector3 rawDirection = transform.position - target.position;

        Vector3 direction;
        if (Mathf.Abs(rawDirection.x) > Mathf.Abs(rawDirection.z))
        {
            direction = new Vector3(Mathf.Sign(rawDirection.x), 0, 0);
        }
        else
        {
            direction = new Vector3(0, 0, Mathf.Sign(rawDirection.z));
        }

        Vector3 snappedTargetPos = new Vector3(
            Mathf.Round(target.position.x),
            target.position.y,
            Mathf.Round(target.position.z)
        );

        Vector3 newPos;

        if (currentDistance < minDistance)
        {
            newPos = transform.position + direction * desiredDistance;
        }
        else
        {
            newPos = snappedTargetPos + (-direction * desiredDistance);
        }

        NavMeshHit hit;
        if (NavMesh.SamplePosition(newPos, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            isMovingForKeepDistance = true; // 移動中フラグをON
        }
    }
}
