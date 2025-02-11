using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemHotbar : MonoBehaviour
{
    public Button slot1Button; // 1�L�[�ɑΉ�����{�^��
    public Button slot2Button; // 2�L�[�ɑΉ�����{�^��
    public TextMeshProUGUI slot1Text; // 1�L�[�̃X���b�g UI �\��
    public TextMeshProUGUI slot2Text; // 2�L�[�̃X���b�g UI �\��

    void Update()
    {
        // `1` �L�[�ŃX���b�g1�̃{�^��������
        if (Input.GetKeyDown(KeyCode.Alpha1) && slot1Button != null)
        {
            slot1Button.onClick.Invoke();
        }

        // `2` �L�[�ŃX���b�g2�̃{�^��������
        if (Input.GetKeyDown(KeyCode.Alpha2) && slot2Button != null)
        {
            slot2Button.onClick.Invoke();
        }
    }

    // �X���b�g�� UI �{�^�����Z�b�g����i���I�ɕς���Ƃ��p�j
    public void SetSlotButton(int slotNumber, Button button)
    {
        if (slotNumber == 1)
        {
            slot1Button = button;
            if (slot1Text != null)
                slot1Text.text = "1: " + button.name;
        }
        else if (slotNumber == 2)
        {
            slot2Button = button;
            if (slot2Text != null)
                slot2Text.text = "2: " + button.name;
        }
    }
}