using UnityEngine;
using System.Collections;

public class EyeEffectController : MonoBehaviour
{
    [SerializeField] private GameObject blackOverlay; // �����I�[�o�[���C�̃Q�[���I�u�W�F�N�g
    [SerializeField] private Canvas parentCanvas; // �eCanvas�̎Q��
    private CanvasGroup canvasGroup;

    void Start()
    {
        if (blackOverlay != null)
        {
            canvasGroup = blackOverlay.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = blackOverlay.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0; // ������Ԃł͓���
            blackOverlay.SetActive(false); // ������Ԃł͔�\��
        }
        else
        {
            Debug.LogError("blackOverlay���ݒ肳��Ă��܂���");
        }
    }

    public void ApplyEyeEffect()
    {
        if (blackOverlay != null && parentCanvas != null)
        {
            parentCanvas.gameObject.SetActive(true); // �eCanvas���A�N�e�B�u�ɂ���
            blackOverlay.SetActive(true); // �����I�[�o�[���C��\��

            // canvasGroup���ݒ肳��Ă��Ȃ��ꍇ�͍ēx�擾
            if (canvasGroup == null)
            {
                canvasGroup = blackOverlay.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = blackOverlay.AddComponent<CanvasGroup>();
                }
            }

            StartCoroutine(FadeIn());
        }
        else
        {
            Debug.LogError("blackOverlay�܂���parentCanvas���ݒ肳��Ă��܂���");
        }
    }

    private IEnumerator FadeIn()
    {
        if (canvasGroup == null)
        {
            Debug.LogError("canvasGroup���ݒ肳��Ă��܂���");
            yield break;
        }

        float duration = 1.0f; // �t�F�[�h�C���̎��ԁi�b�j
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }
    }
}