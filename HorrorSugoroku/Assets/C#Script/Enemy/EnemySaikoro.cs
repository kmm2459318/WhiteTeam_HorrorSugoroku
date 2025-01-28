using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmoothigTransform;

public class EnemySaikoro : MonoBehaviour
{
    [SerializeField] SmoothTransform enemySmooth;
    public GameObject enemy;
    public GameObject player;
    public GameObject saikoro; // サイコロのゲームオブジェクト
    public LayerMask wallLayer; // 壁のレイヤー
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    private int steps; // サイコロの目の数
    Image image;

    void Start()
    {
        if (saikoro != null)
        {
            saikoro.SetActive(false);
        }
        else
        {
            Debug.LogError("Saikoro GameObject is not assigned in the Inspector.");
        }

        // サイコロのImageを保持
        image = saikoro.GetComponent<Image>();
    }

    void Update()
    {
        if (FindObjectOfType<GameManager>().IsPlayerTurn())
        {
            return;
        }

        switch (steps)
        {
            case 1:
                image.sprite = s1; break;
            case 2:
                image.sprite = s2; break;
            case 3:
                image.sprite = s3; break;
            case 4:
                image.sprite = s4; break;
            case 5:
                image.sprite = s5; break;
            case 6:
                image.sprite = s6; break;
        }
    }

    public IEnumerator RollEnemyDice()
    {
        saikoro.SetActive(true);
        for (int i = 0; i < 10; i++) // 10回ランダムに目を表示
        {
            steps = Random.Range(1, 7);
            yield return new WaitForSeconds(0.1f); // 0.1秒ごとに目を変更
        }

        if (steps <= 3)
        {
            enemySmooth.PosFact = 0.9f;
        }
        else
        {
            enemySmooth.PosFact = 0.2f;
        }

        Debug.Log("Enemy rolled: " + steps);
        StartCoroutine(MoveTowardsPlayer());
    }

    private IEnumerator MoveTowardsPlayer()
    {
        int initialSteps = steps; // 初期のステップ数を保存
        while (steps > 0)
        {
            Vector3 direction = (player.transform.position - enemy.transform.position).normalized;
            direction = GetValidDirection(direction); // 壁を避ける方向を計算

            enemySmooth.TargetPosition += direction * 1.0f; // 2.0f単位で移動
            Debug.Log(direction);
            steps--;
            Debug.Log("Enemy moved towards player. Steps remaining: " + steps);
            yield return new WaitForSeconds(0.5f); // 移動の間隔を待つ
        }
        saikoro.SetActive(false); // サイコロを非表示にする

        Debug.Log("Enemy moved a total of " + initialSteps + " steps.");
        FindObjectOfType<GameManager>().NextTurn(); // 次のターンに進む
    }

    private Vector3 GetValidDirection(Vector3 targetDirection)
    {
        Vector3[] directions = new Vector3[]
        {
            new Vector3(2.0f, 0, 0),   // 東
            new Vector3(-2.0f, 0, 0),  // 西
            new Vector3(0, 0, 2.0f),   // 北
            new Vector3(0, 0, -2.0f)   // 南
        };

        Vector3 bestDirection = Vector3.zero;
        float closestDistance = float.MaxValue;

        foreach (Vector3 direction in directions)
        {
            Vector3 potentialPosition = enemy.transform.position + direction;
            if (!Physics.CheckSphere(potentialPosition, 0.5f, wallLayer))
            {
                float distanceToPlayer = Vector3.Distance(potentialPosition, player.transform.position);
                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    bestDirection = direction;
                }
            }
        }

        return bestDirection != Vector3.zero ? bestDirection : targetDirection; // 有効な方向があればそれを返す
    }

    public IEnumerator EnemyTurn()
    {
        yield return StartCoroutine(RollEnemyDice());
    }

    void LateUpdate()
    {
        Ray ray = new Ray(enemy.transform.position, enemy.transform.forward);
        RaycastHit hit;

        // センサー機能: Rayが何かに当たった場合にログ出力
        if (Physics.Raycast(ray, out hit, 3f)) // 3mの範囲
        {
            //Debug.Log("発見");
        }
    }

    void OnDrawGizmosSelected()
    {
        // センサーの範囲を赤い線で表示
        Gizmos.color = Color.red;
        Vector3 direction = enemy.transform.position + enemy.transform.forward * 3f;
        Gizmos.DrawLine(enemy.transform.position, direction);
    }
}
