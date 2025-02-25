using UnityEngine;
using UnityEngine.AI;

public class GridBasedAgent : MonoBehaviour
{
    /*public Transform target; // ターゲットオブジェクト
    public float waitTime = 1.0f; // 各マスでの待機時間
    public float gridSize = 1.0f; // マス目のサイズ

    private NavMeshAgent agent;
    private Queue<Vector3> pathPoints; // マス目ごとの経路
    private bool isWaiting = false;
    public float arrivalThreshold = 0.2f; // マスへの到達とみなす距離

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CalculateOptimizedGridPath(); // 最短経路を計算
    }

    void Update()
    {
        if (!isWaiting && pathPoints != null && pathPoints.Count > 0)
        {
            Vector3 nextPoint = pathPoints.Peek(); // 次のマスの位置を確認

            // Y軸を無視して距離を計算
            Vector2 currentPos2D = new Vector2(transform.position.x, transform.position.z);
            Vector2 nextPoint2D = new Vector2(nextPoint.x, nextPoint.z);

            // 現在の位置が目的のマスに到達しているか
            if (Vector2.Distance(currentPos2D, nextPoint2D) < arrivalThreshold)
            {
                pathPoints.Dequeue(); // 到達したマスを削除
                StartCoroutine(WaitAndMoveToNextPoint()); // 1マスごとに停止
            }
            else
            {
                agent.destination = nextPoint; // 次のマスへ移動
            }
        }
    }

    // ターゲットまでの最短経路をXとZに分解して計算
    private void CalculateOptimizedGridPath()
    {
        pathPoints = new Queue<Vector3>();

        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.position;

        while (Mathf.Abs(targetPos.x - currentPos.x) > gridSize || Mathf.Abs(targetPos.z - currentPos.z) > gridSize)
        {
            // X方向とZ方向の距離を比較して、短い方を優先
            if (Mathf.Abs(targetPos.x - currentPos.x) > Mathf.Abs(targetPos.z - currentPos.z))
            {
                // X方向に移動
                currentPos = new Vector3(
                    currentPos.x + Mathf.Sign(targetPos.x - currentPos.x) * gridSize,
                    currentPos.y,
                    currentPos.z
                );
            }
            else
            {
                // Z方向に移動
                currentPos = new Vector3(
                    currentPos.x,
                    currentPos.y,
                    currentPos.z + Mathf.Sign(targetPos.z - currentPos.z) * gridSize
                );
            }

            pathPoints.Enqueue(currentPos); // キューに現在位置を追加
        }
    }

    // 各マスで停止して待機するコルーチン
    private IEnumerator WaitAndMoveToNextPoint()
    {
        isWaiting = true;
        agent.isStopped = true; // エージェントを停止
        yield return new WaitForSeconds(waitTime); // 指定の時間待機
        agent.isStopped = false; // エージェントの移動を再開
        isWaiting = false;
    }*/
}
