using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Y軸の変化を無視する
        direction.Normalize();

        // プレイヤーの方向を向く
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

        // プレイヤーに向かって移動
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}