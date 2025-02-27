using UnityEngine;
using UnityEngine.UI;

public class HeelCurseGage : MonoBehaviour
{
    [SerializeField] private Image[] ImageGages; // �Q�[�W�摜
    [SerializeField] private Button resetButton; // �Q�[�W�����Z�b�g����{�^��

    void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.RemoveAllListeners();
            resetButton.onClick.AddListener(ResetGages); // �{�^���������ꂽ�Ƃ��Ƀ��Z�b�g
        }
    }

   
    // �Q�[�W�̃��Z�b�g
    public void ResetGages()
    {
        foreach (Image img in ImageGages)
        {
            img.fillAmount = 0; // �Q�[�W�����Z�b�g
        }
        Debug.Log("�Q�[�W�����Z�b�g����܂���");
    }
}
