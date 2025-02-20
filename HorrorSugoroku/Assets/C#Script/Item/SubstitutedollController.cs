using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SubstitutedollController : MonoBehaviour
{
    // �g����l�`�̏�����
    private static int substituteDollCount = 3; // �f�o�b�O�p��3��������
    public int itemCount = 0; // �A�C�e���̐�
    public TMP_Text dollCountText; // �{�^���ɕ\������e�L�X�g

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

        // ����̃e�L�X�g�X�V
        UpdateDollCountText();
    }

    public void AddItem()
    {
        itemCount++;
        substituteDollCount++;
        Debug.Log("�g����l�`��1�����܂����I���݂̐�: " + substituteDollCount);
        UpdateDollCountText(); // �e�L�X�g�X�V
    }

    private void OnButtonPressed()
    {
        if (substituteDollCount > 0)
        {
            substituteDollCount--; // �����������炷
            SceneChanger3D.hasSubstituteDoll = true; // �g�p����

            Debug.Log("�g����l�`���g�p�I �c��: " + substituteDollCount);
            UpdateDollCountText(); // �e�L�X�g�X�V
        }
        else
        {
            Debug.Log("�g����l�`������܂���I");
        }
    }

    private void UpdateDollCountText()
    {
        if (dollCountText != null)
        {
            dollCountText.text = "�g����l�`: " + substituteDollCount;
        }
        else
        {
            Debug.LogError("dollCountText ���ݒ肳��Ă��܂���I");
        }
    }
}
