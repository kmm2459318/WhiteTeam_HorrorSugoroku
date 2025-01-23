using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���Ǘ��p
using UnityEngine.UI; // UI���g�p���邽��

public class SceneChanger3D : MonoBehaviour
{
    [SerializeField] private string enemyObjectName = "Enemy"; // �G�I�u�W�F�N�g�̖��O
    [SerializeField] private Image cutInImage; // �J�b�g�C���p��UI�摜
    [SerializeField] private float cutInDuration = 2.0f; // �J�b�g�C���̕\�����ԁi�b�j

    private bool isGameOver = false; // �d�������h�~�p�t���O

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGameOver && collision.gameObject.name == enemyObjectName)
        {
            StartCoroutine(ShowCutInAndGoToGameover()); // �R���[�`�����J�n
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver && other.gameObject.name == enemyObjectName)
        {
            StartCoroutine(ShowCutInAndGoToGameover()); // �R���[�`�����J�n
        }
    }

    // �J�b�g�C����\�����Ă���Q�[���I�[�o�[�V�[���ɑJ�ڂ��鏈��
    private IEnumerator ShowCutInAndGoToGameover()
    {
        isGameOver = true; // �������d�����Ȃ��悤�Ƀt���O�𗧂Ă�

        // �J�b�g�C���摜��\��
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(true); // �J�b�g�C���摜���A�N�e�B�u��
        }

        // �w�肵�����Ԃ����ҋ@
        yield return new WaitForSeconds(cutInDuration);

        // �Q�[���I�[�o�[�V�[���Ɉړ�
        SceneManager.LoadScene("Gameover");
    }
}
