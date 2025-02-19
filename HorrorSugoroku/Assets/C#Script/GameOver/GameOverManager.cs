using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    private bool isGameOverScene = false;
    [SerializeField] private TextMeshProUGUI pressSpaceText; // TextMeshPro�̃e�L�X�g�I�u�W�F�N�g���Q��
    [SerializeField] private float fadeDuration = 1.0f; // �t�F�[�h�C���̎���

    void Start()
    {
        // ���݂̃V�[����GameOver�V�[�����ǂ������m�F
        if (SceneManager.GetActiveScene().name == "Gameover")
        {
            isGameOverScene = true;
            // �e�L�X�g�I�u�W�F�N�g���\���ɂ���
            if (pressSpaceText != null)
            {
                pressSpaceText.gameObject.SetActive(false);
            }
            // 5�b��Ƀt�F�[�h�C�����J�n
            StartCoroutine(ShowTextWithFadeIn());
        }
    }

    void Update()
    {
        // GameOver�V�[���ŃX�y�[�X�L�[�������ꂽ��
        if (isGameOverScene && Input.GetKeyDown(KeyCode.Space))
        {
            // Title�V�[���Ɉړ�
            SceneManager.LoadScene("Title");
        }
    }

    private IEnumerator ShowTextWithFadeIn()
    {
        // 5�b�ҋ@
        yield return new WaitForSeconds(3.0f);

        // �e�L�X�g�I�u�W�F�N�g��\��
        if (pressSpaceText != null)
        {
            pressSpaceText.gameObject.SetActive(true);
            pressSpaceText.alpha = 0.0f; // ���������x��0�ɐݒ�
            StartCoroutine(FadeInText());
        }
    }

    private IEnumerator FadeInText()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            pressSpaceText.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }
}