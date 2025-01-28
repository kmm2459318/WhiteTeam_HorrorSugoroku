using UnityEngine;
using UnityEngine.UI;

public class BeartrapController : MonoBehaviour
{
    private void Start()
    {
        // Button�R���|�[�l���g���擾
        Button button = GetComponent<Button>();

        // Button�R���|�[�l���g�����݂��邩�m�F
        if (button != null)
        {
            // �{�^���������ꂽ�Ƃ���OnButtonPressed���\�b�h���Ăяo��
            button.onClick.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("Button�R���|�[�l���g���A�^�b�`����Ă��܂���I");
        }
    }

    // �{�^���������ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    private void OnButtonPressed()
    {
        Debug.Log(gameObject.name + " ���N���b�N����܂����I");
    }
}
