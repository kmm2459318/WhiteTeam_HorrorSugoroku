using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // プレイヤーとの距離を計算
        float distance = Vector3.Distance(transform.position, player.position);

        // プレイヤーを追いかける
        if (distance > 1f)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // 移動中はrun1アニメーションを再生
            animator.SetBool("isRunning", true);
        }
        else
        {
            // 停止時は待機モーションに戻る
            animator.SetBool("isRunning", false);
        }
    }
}