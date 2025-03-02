using UnityEngine;
using UnityEngine.UI;

public class HeelCurseGage : MonoBehaviour
{
    [SerializeField] Image[] ImageGages; // �Q�[�W�摜
    [SerializeField] Button resetButton; // �Q�[�W�����Z�b�g����{�^��
    [SerializeField] Slider DashGage;
    public CurseSlider curseslider;

    void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.RemoveAllListeners();
            resetButton.onClick.AddListener(ResetGages); // �{�^���������ꂽ�Ƃ��Ƀ��Z�b�g
        }
        curseslider.dashPoint = 0;
    }


    // �Q�[�W�̃��Z�b�g
    public void ResetGages()
    {
        curseslider.dashPoint = 0;
        foreach (Image img in ImageGages)
        {
            curseslider.dashPoint = 0;
            img.fillAmount = 0; // �Q�[�W�����Z�b�g
            Debug.Log("�Q�[�W�����Z�b�g����܂���");
        }
        
    }
}
