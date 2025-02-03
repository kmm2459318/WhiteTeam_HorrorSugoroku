using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform front;
    public Transform back;
    public Transform right;
    public Transform left;
    public LayerMask wallLayer; // 壁のレイヤー
    private bool discovery = false;
    private Vector3 moveDirection;
    private float directionChangeCooldown = 1.0f; // 方向変更のクールダウン時間
    private float lastDirectionChangeTime;

    void Update()
    {
        //if (discovery)
        //{
            // プレイヤーの方向に向く
            //Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            //.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        //}
        /*else
        {
            // 移動方向に向く
            if (moveDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // 壁に当たった場合に方向を修正
            bool frontHit = Physics.CheckSphere(front.position, 0.5f, wallLayer);
            bool rightHit = Physics.CheckSphere(right.position, 0.5f, wallLayer);
            bool leftHit = Physics.CheckSphere(left.position, 0.5f, wallLayer);

            if (Time.time > lastDirectionChangeTime + directionChangeCooldown)
            {
                if (frontHit)
                {
                    moveDirection = -moveDirection; // 方向を反転
                    lastDirectionChangeTime = Time.time;
                    Debug.Log("Wall detected at front, changing direction to: " + moveDirection);
                }
                else if (!rightHit || !leftHit)
                {
                    moveDirection = -moveDirection; // 方向を反転
                    lastDirectionChangeTime = Time.time;
                    Debug.Log("Wall detected at right or left, changing direction to: " + moveDirection);
                }
            }
        }*/
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