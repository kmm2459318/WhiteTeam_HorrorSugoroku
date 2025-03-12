using System.Collections;
using UnityEngine;

public class EnemyAppearance : MonoBehaviour
{
    public GameObject enemyModel; // �G�l�~�[�̃��f��
    public float displayDuration = 2f; // �G�l�~�[���\������鎞�ԁi�b�j

    void Start()
    {
        if (enemyModel == null)
        {
            Debug.LogError("�G�l�~�[���f�����ݒ肳��Ă��܂���");
        }
        else
        {
            enemyModel.SetActive(true); // �ŏ�����\������
            Debug.Log("�G�l�~�[��������Ԃŕ\������܂���");
        }
    }

    public void HideEnemyAfterDelay()
    {
        Debug.Log("aaa");
        if (enemyModel != null)
        {
            StartCoroutine(HideEnemyCoroutine(displayDuration)); // ��莞�Ԍ�ɔ�\���ɂ���R���[�`�����J�n
        }
    }

    private IEnumerator HideEnemyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肳�ꂽ���ԑ҂�
        if (enemyModel != null)
        {
            enemyModel.SetActive(false); // �G�l�~�[���\���ɂ���
            Debug.Log("�G�l�~�[����\���ɂȂ�܂���");
        }
    }
}
