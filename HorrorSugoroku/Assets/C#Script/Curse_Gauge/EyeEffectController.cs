using UnityEngine;

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

        if (parentCanvas != null)
        {
            parentCanvas.gameObject.SetActive(false); // �eCanvas��������ԂŔ�\���ɂ���
        }
        else
        {
            Debug.LogError("parentCanvas���ݒ肳��Ă��܂���");
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

            // �t�F�[�h�C���Ȃ��Ŋ��S�ɕ\�����ꂽ��Ԃɂ���
            canvasGroup.alpha = 1;
        }
        else
        {
            Debug.LogError("blackOverlay�܂���parentCanvas���ݒ肳��Ă��܂���");
        }
    }
}