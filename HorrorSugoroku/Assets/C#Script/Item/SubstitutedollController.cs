using UnityEngine;
using UnityEngine.UI;

public class SubstitutedollController : MonoBehaviour
{
    // �g����l�`�̏�����
    private static int substituteDollCount = 3; // �f�o�b�O�p��3��������

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
            substituteDollCount--; // �����������炷
            SceneChanger3D.hasSubstituteDoll = true; // �g�p����

            Debug.Log("�g����l�`���g�p�I �c��: " + substituteDollCount);

            if (substituteDollCount <= 0)
            {
                Destroy(gameObject); // 0�ɂȂ�����{�^�����폜
                Debug.Log("�g����l�`���Ȃ��Ȃ����I");
            }
        }
        else
        {
            Debug.Log("�g����l�`������܂���I");
        }
    }
}
