using UnityEngine;

public class BeartrapTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�G���g���΂��݂Ɉ����|�������I�I");

            EnemySaikoro enemy = other.GetComponent<EnemySaikoro>();
            if (enemy != null)
            {
                enemy.isTrapped = true; // �����œG�̓������~�߂�
                Debug.Log("�G�������Ȃ��Ȃ����I");
            }

            // �g���o�T�~���폜�i���̃^�[���ŏ����Ȃ�ʂ̏�����ǉ��j
            Destroy(gameObject);
        }
    }
}
