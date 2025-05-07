using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffectUI : MonoBehaviour
{
    public enum GameResult { Clear, Over } // �Q�[�����ʂ�Enum
    [SerializeField] private GameResult gameResult; // �C���X�y�N�^�[�Őݒ�

    [SerializeField] private Text uiText;
    [SerializeField] private Text gameResultText; // GAME CLEAR / GAME OVER �̃e�L�X�g
    [SerializeField] private AudioSource audioSource; // �����Đ��p
    [SerializeField] private AudioClip typingSound; // �^�C�v��
    [SerializeField] private AudioClip gameClearSound; // �Q�[���N���A�p��
    [SerializeField] private AudioClip gameOverSound; // �Q�[���I�[�o�[�p��
    [SerializeField] private float delay = 0.1f;

    private string[] clearMessages = {
       "�C���t���ƕa�@�̒��������B",
       "�p�p�ƃ}�}���ƂĂ��S�z���Ă���Ă���B\n���ꂩ��A3���قǂ����Ă����񂾂��āB",
       "�v���Ԃ�Ƀp�p�ƃ}�}�̊������Ă��ꂵ�������B",
       "���̓��̂��Ƃ��l�ɕ����Ă݂����ǁA\n�o���Ă��Ȃ������݂����B",
       "�������ɋA���āA�l�`��T��������...",
       "������Ȃ�����",
       "", // �󔒕����ł͉��𗬂��Ȃ�
       "����͂��������Ȃ񂾂����́B"
    };

    private string[] overMessages = {
       "�C���t���ƕa�@�̒��������B",
       "�p�p�ƃ}�}���ƂĂ��S�z���Ă���Ă���B\n���ꂩ��A3���قǂ����Ă����񂾂��āB",
       "�v���Ԃ�ɂӂ���ɉ�Ă��ꂵ���B",
       "���̓��̂��Ƃ��ӂ���ɕ����Ă݂�����\n���ڂ��Ă��Ȃ������݂����B",
       "�}�}�H�����Ă���́H�킽���͂������傤�ԁB",
       "�݂��Ȃ����A�������Ȃ����ǐ����Ă邩��...",
       "", // �󔒕����ł͉��𗬂��Ȃ�
       "����%/&?$�͉��������̂��ȁA"
    };

    void Start()
    {
        gameResultText.gameObject.SetActive(false); // �ŏ��͔�\��
        StartCoroutine(DisplayMessages());
    }

    IEnumerator DisplayMessages()
    {
        string[] messages = (gameResult == GameResult.Clear) ? clearMessages : overMessages; // ��Ԃɉ����ă��b�Z�[�W�ύX

        foreach (string message in messages)
        {
            if (!string.IsNullOrEmpty(message)) // �󔒂Ȃ特�Ȃ�
            {
                audioSource.clip = typingSound;
                audioSource.Play();
            }

            yield return StartCoroutine(ShowText(message));

            audioSource.Stop(); // ���b�Z�[�W���I������特���~�߂�
            yield return new WaitForSeconds(1f); // ���b�Z�[�W�Ԃ̑ҋ@����
        }

        yield return new WaitForSeconds(1f);
        uiText.gameObject.SetActive(false); // ���b�Z�[�W���\��
        gameResultText.gameObject.SetActive(true); // �Q�[�����ʂ�\��

        // �Q�[���N���A���Q�[���I�[�o�[���ŉ�����؂�ւ�
        if (gameResult == GameResult.Clear)
        {
            gameResultText.text = "GAME CLEAR";
            audioSource.clip = gameClearSound;
        }
        else
        {
            gameResultText.text = "GAME OVER";
            audioSource.clip = gameOverSound;
        }

        audioSource.Play(); // �I�����ꂽ���ʂ̉����Đ�
    }

    IEnumerator ShowText(string text)
    {
        uiText.text = "";
        for (int i = 0; i <= text.Length; i++)
        {
            uiText.text = text.Substring(0, i);
            audioSource.PlayOneShot(typingSound); // �����\�����̃^�C�v��
            yield return new WaitForSeconds(delay);
        }
    }
}