using TMPro;
using UnityEngine;

public class CurseTextManager : MonoBehaviour
{
    public TextMeshProUGUI curseText;
    public GameObject conditionObject; // �e�L�X�g�\���̏����ƂȂ�I�u�W�F�N�g

    private void Start()
    {
        // �V�[���̊J�n���Ɏ􂢔����e�L�X�g���\���ɂ���
        if (curseText != null)
        {
            curseText.gameObject.SetActive(false);
            Debug.Log("�V�[���̊J�n���Ɏ􂢔����e�L�X�g���\���ɂ��܂�");
        }
        else
        {
            Debug.LogError("curseText���ݒ肳��Ă��܂���");
        }
    }

    private void Update()
    {
        // �����I�u�W�F�N�g���A�N�e�B�u���ǂ������Ď�
        if (conditionObject != null && conditionObject.activeSelf)
        {
            ShowCurseText();
        }
        else
        {
            HideCurseText();
        }
    }

    public void ShowCurseText()
    {
        if (curseText != null && !curseText.gameObject.activeSelf)
        {
            curseText.gameObject.SetActive(true);
            Debug.Log("�􂢔����e�L�X�g��\�����܂�");
        }
    }

    public void HideCurseText()
    {
        if (curseText != null && curseText.gameObject.activeSelf)
        {
            curseText.gameObject.SetActive(false);
            Debug.Log("�􂢔����e�L�X�g���\���ɂ��܂�");
        }
    }
}
