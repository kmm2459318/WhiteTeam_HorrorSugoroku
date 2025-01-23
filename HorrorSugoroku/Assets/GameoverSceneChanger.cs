using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger3D : MonoBehaviour
{
    [SerializeField] private GameObject enemy; // �G�I�u�W�F�N�g�̖��O
    [SerializeField] private Image cutInImage; // �J�b�g�C���摜
    [SerializeField] private float cutInDuration = 2.0f; // �J�b�g�C���̕\�����ԁi�b�j

    private bool isGameOver = false; // �d�������h�~�p�t���O

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGameOver && collision.gameObject == enemy)
        {
            StartCoroutine(ShowCutInAndGoToGameover()); // �R���[�`�����J�n
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver && other.gameObject == enemy)
        {
            StartCoroutine(ShowCutInAndGoToGameover()); // �R���[�`�����J�n
        }
    }

    // �J�b�g�C���摜��\�����Ă���Q�[���I�[�o�[�V�[���ɑJ�ڂ��鏈��
    private IEnumerator ShowCutInAndGoToGameover()
    {
        isGameOver = true; // �d�������h�~�p�t���O

        // ����UI�v�f�i�e�L�X�g�Ȃǁj���\���ɂ���
        HideAllUI(); // UI��\�����������s

        // �J�b�g�C���摜��\��
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(true); // �摜��\��
        }

        // �w�肳�ꂽ���Ԃ����ҋ@
        yield return new WaitForSeconds(cutInDuration);

        // �J�b�g�C���摜���\���ɂ���
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(false); // �摜���\��
        }

        // �Q�[���I�[�o�[�V�[���֑J��
        SceneManager.LoadScene("Gameover");
    }

    // UI�̑��̗v�f�i�e�L�X�g�₻�̑��̉摜�j���\���ɂ��郁�\�b�h
    private void HideAllUI()
    {
        // ����UI�v�f������Δ�\���ɂ��܂��B�Ⴆ�΁A�e�L�X�g��{�^���ȂǁB
        // �����Ńe�L�X�g��{�^�����\���ɂ��鏈����ǉ����Ă��������B
        // ��:
        // if (someText != null) someText.gameObject.SetActive(false);
        // if (someButton != null) someButton.gameObject.SetActive(false);
    }
}
