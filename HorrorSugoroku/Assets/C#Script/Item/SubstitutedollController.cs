using UnityEngine;
using UnityEngine.UI;

public class SubstitutedollController : MonoBehaviour
{
    private static int substituteDollCount = 3; // �f�o�b�O�p��3��������
    public CurseSlider curseSlider; // �􂢃Q�[�W�̊Ǘ�

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("Button�R���|�[�l���g���A�^�b�`����Ă��܂���I");
        }
    }

    private void OnButtonPressed()
    {
        if (substituteDollCount > 0)
        {
            substituteDollCount--;
            SceneChanger3D.hasSubstituteDoll = true;

            Debug.Log("�g����l�`���g�p�I �c��: " + substituteDollCount);

            // �􂢃Q�[�W��10����
            if (curseSlider != null)
            {
                curseSlider.IncreaseDashPoint(10);
            }

            if (substituteDollCount <= 0)
            {
                Destroy(gameObject);
                Debug.Log("�g����l�`���Ȃ��Ȃ����I");
            }
        }
        else
        {
            Debug.Log("�g����l�`������܂���I");
        }
    }
}
