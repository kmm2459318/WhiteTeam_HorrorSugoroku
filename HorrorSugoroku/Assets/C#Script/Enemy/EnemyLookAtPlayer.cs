using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public LayerMask wallLayer; // 壁のレイヤー
    private bool discovery = false;
    private Vector3 moveDirection;
    private float directionChangeCooldown = 1.0f; // 方向変更のクールダウン時間
    private float lastDirectionChangeTime;
    private Animator animator; // アニメーターの参照

    void Start()
    {
        animator = GetComponent<Animator>(); // アニメーターコンポーネントを取得
    }

    void Update()
    {
        if (discovery)
        {
            // プレイヤーの方向に向く（削除）
        }
        else
        {
            // 移動方向に向く
            if (moveDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                animator.SetBool("isRunning", true); // Runアニメーションを再生
            }
            else
            {
                animator.SetBool("isRunning", false); // Runアニメーションを停止
            }

            // 壁に当たった場合に方向を修正（削除）
        }
    }

    public void SetDiscovery(bool isDiscovered)
    {
        discovery = isDiscovered;
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
        Debug.Log("Move direction set to: " + direction);
    }
}