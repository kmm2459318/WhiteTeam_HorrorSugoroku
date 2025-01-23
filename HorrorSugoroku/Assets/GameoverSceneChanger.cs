using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger3 : MonoBehaviour
{
    // �G�I�u�W�F�N�g�𒼐ڐݒ�
    [SerializeField] private GameObject enemyObject;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�Ԃ�������");
        // �G�I�u�W�F�N�g�Q�ƂŔ���
        if (collision.gameObject == enemyObject)
        {
            GoToGameover();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �G�I�u�W�F�N�g�Q�ƂŔ���
        if (other.gameObject == enemyObject)
        {
            GoToGameover();
        }
    }

    // Gameover �V�[���֑J�ڂ��郁�\�b�h
    private void GoToGameover()
    {
        SceneManager.LoadScene("Gameover");
    }
}
