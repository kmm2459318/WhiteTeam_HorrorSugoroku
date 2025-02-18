using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public GameObject player; // プレイヤーのゲームオブジェクト
    public LayerMask wallLayer; // 壁のレイヤー
    private bool discovery = false;
    private Vector3 moveDirection;
    private Animator animator; // アニメーターの参照
    private bool isMoving = false; // エネミーが移動中かどうかを示すフラグ

    void Start()
    {
        animator = GetComponent<Animator>(); // アニメーターコンポーネントを取得
        if (animator == null)
        {
            //Debug.LogError("Animator component not found on the enemy object.");
        }
    }

    void Update()
    {
        if (discovery)
        {
            // プレイヤーの方向に向く
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // 移動方向をプレイヤーの方向に設定
            moveDirection = directionToPlayer;
        }
        else
        {
            // 移動方向に向く
            if (moveDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // 壁に当たった場合に方向を反転
            bool frontHit = Physics.CheckBox(transform.position, transform.localScale / 2, transform.rotation, wallLayer);

            if (frontHit)
            {
                moveDirection = -moveDirection; // 方向を反転
                Debug.Log("Wall detected at front, changing direction to: " + moveDirection);
            }
        }

        // エネミーの移動状態に基づいてアニメーションを制御
        isMoving = moveDirection != Vector3.zero; // 移動方向がゼロでない場合は移動中と判断
        //animator.SetBool("is Running", isMoving); // isRunningパラメータを設定
    }

    public void SetDiscovery(bool isDiscovered)
    {
        discovery = isDiscovered;
        Debug.Log("Discovery state set to: " + isDiscovered);
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
        isMoving = moveDirection != Vector3.zero; // 移動方向がゼロでない場合は移動中と判断
        Debug.Log("Move direction set to: " + direction);
    }

    public void SetIsMoving(bool moving)
    {
        isMoving = moving;
        Debug.Log("IsMoving state set to: " + moving);
    }
}