using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger3D : MonoBehaviour
{
    [SerializeField] private GameObject enemy; // �G�I�u�W�F�N�g�̖��O
    [SerializeField] private Image cutInImage; // �J�b�g�C���摜
    [SerializeField] private float cutInDuration = 2.0f; // �J�b�g�C���̕\�����ԁi�b�j
    [SerializeField] private AudioClip gameOverSound; // �Q�[���I�[�o�[���̃T�E���h
    private AudioSource audioSource; // �����Đ��p��AudioSource

    [SerializeField] private float volume = 1.0f; // ���� (�f�t�H���g�͍ő�)

    private bool isGameOver = false; // �d�������h�~�p�t���O
    public static bool hasSubstituteDoll = false; // �g����l�`�̎g�p�t���O

    private void Start()
    {
        // AudioSource�̏�����
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource���A�^�b�`����Ă��Ȃ��ꍇ�͒ǉ�
        }

        // ���ʂ̐ݒ�
        audioSource.volume = volume;

        // �ŏ��ɉ�����Ȃ��悤�ɁA�����Đ����Ȃ�
        audioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGameOver && collision.gameObject == enemy)
        {
            HandleGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver && other.gameObject == enemy)
        {
            HandleGameOver();
        }
    }

    // �Q�[���I�[�o�[�����𔻒肷�郁�\�b�h
    private void HandleGameOver()
    {
        if (hasSubstituteDoll)
        {
            // �g����l�`������ꍇ�͉��
            hasSubstituteDoll = false; // �g����l�`������
            Debug.Log("�g����l�`�������I�Q�[���I�[�o�[������I");
        }
        else
        {
            StartCoroutine(ShowCutInAndGoToGameover()); // �Q�[���I�[�o�[���������s
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

        // �Q�[���I�[�o�[�T�E���h���Đ�
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.clip = gameOverSound; // �T�E���h��ݒ�
            audioSource.Play(); // ����炷
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
