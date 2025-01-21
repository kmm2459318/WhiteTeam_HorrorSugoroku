using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySaikoro : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public GameObject saikoro; // サイコロのゲームオブジェクト
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    private Image image;
    private int steps; // サイコロの目の数

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (saikoro != null)
        {
            image = saikoro.GetComponent<Image>();
            saikoro.SetActive(false);
        }
        else
        {
            Debug.LogError("Saikoro GameObject is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (FindObjectOfType<GameManager>().IsPlayerTurn())
        {
            return;
        }
    }

    public IEnumerator RollEnemyDice()
    {
        saikoro.SetActive(true);
        for (int i = 0; i < 10; i++) // 10回ランダムに目を表示
        {
            steps = Random.Range(1, 7);
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
            yield return new WaitForSeconds(0.1f); // 0.1秒ごとに目を変更
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
            enemy.transform.position += direction * 2.0f; // 2.0f単位で移動
            steps--;
            Debug.Log("Enemy moved towards player. Steps remaining: " + steps);
            yield return new WaitForSeconds(0.5f); // 移動の間隔を待つ
        }
        saikoro.SetActive(false); // サイコロを非表示にする

        // エネミーの進んだ数をデバッグログに表示
        Debug.Log("Enemy moved a total of " + initialSteps + " steps.");

        FindObjectOfType<GameManager>().NextTurn(); // 次のターンに進む
    }

    public IEnumerator EnemyTurn()
    {
        yield return StartCoroutine(RollEnemyDice());
    }
}