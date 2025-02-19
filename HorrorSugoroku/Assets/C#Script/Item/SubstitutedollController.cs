using UnityEngine;
using UnityEngine.UI;

public class SubstitutedollController : MonoBehaviour
{
    private static int substituteDollCount = 3; // �f�o�b�O�p��3��������
    public CurseSlider curseSlider; // �􂢃Q�[�W�̊Ǘ�
    // �g����l�`�̏�����
    private static int substituteDollCount ; // �f�o�b�O�p��3��������
    public int itemCount = 0; // �A�C�e���̐�
    public Button substituteDollButton; // �{�^�����A�^�b�`

    private void Start()
    {
        if (substituteDollButton == null)
        {
            Debug.LogError("substituteDollButton ���A�^�b�`����Ă��܂���I");
            return;
        }

        // �{�^���̃��X�i�[��ݒ�
        substituteDollButton.onClick.AddListener(OnButtonPressed);

        // ������ԂŃ{�^���̕\��/��\�����X�V
        UpdateButtonVisibility();
    }
    public void AddItem()
    {
        itemCount++;
        substituteDollCount++;
        Debug.Log("�g����l�`��1�����܂����I���݂̐�: " + itemCount);
    }
    private void OnButtonPressed()
    {
        if( itemCount > 0)
        {
            substituteDollCount--;  // �����������炷
            SceneChanger3D.hasSubstituteDoll = true; // �g�p����

            Debug.Log("�g����l�`���g�p�I �c��: " + itemCount);

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
            // �{�^���̕\�����X�V
            UpdateButtonVisibility();
        }
        else
        {
            Debug.Log("�g����l�`������܂���I");
        }
    }

    private void UpdateButtonVisibility()
    {
        if (substituteDollButton != null)
        {
            substituteDollButton.gameObject.SetActive(itemCount > 0);
        }
    }
    //private void Start()
    //{
    //    Button button = GetComponent<Button>();
    //    if (button != null)
    //    {
    //        button.onClick.AddListener(OnButtonPressed);
    //    }
    //    else
    //    {
    //        Debug.LogError("Button�R���|�[�l���g���A�^�b�`����Ă��܂���I");
    //    }
    //}

    //private void OnButtonPressed()
    //{
    //    if (substituteDollCount > 0)
    //    {
    //        substituteDollCount--; // �����������炷
    //        SceneChanger3D.hasSubstituteDoll = true; // �g�p����

    //        Debug.Log("�g����l�`���g�p�I �c��: " + substituteDollCount);
    //        UpdateButtonVisibility();
    //        if (substituteDollCount <= 0)
    //        {
    //            Destroy(gameObject); // 0�ɂȂ�����{�^�����폜
    //            Debug.Log("�g����l�`���Ȃ��Ȃ����I");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("�g����l�`������܂���I");
    //    }

    //}
    //private void UpdateButtonVisibility()
    //{
    //    if (substituteDollButton != null)
    //    {
    //        substituteDollButton.gameObject.SetActive(substituteDollCount > 0);
    //    }
    //}
}
