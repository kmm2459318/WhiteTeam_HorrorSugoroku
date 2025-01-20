using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Y���̕ω��𖳎�����
        direction.Normalize();

        // �v���C���[�̕���������
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

        // �v���C���[�Ɍ������Ĉړ�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}