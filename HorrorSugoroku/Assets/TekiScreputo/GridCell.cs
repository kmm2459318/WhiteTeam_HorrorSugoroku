using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public string cellEffect = "Normal"; // �}�X�ڂ̌��ʁi��: Normal, Bonus, Penalty�j

    public void ExecuteEvent()
    {
        // �}�X�ڂ̌��ʂ𔭓�
        switch (cellEffect)
        {
            case "Event":
                DisplayRandomEvent();
                break;
            case "Blockl":
                Debug.Log($"{name}: �y�i���e�B���ʔ����I");
                break;
            case "Item":
                Debug.Log($"{name}:�A�C�e�����l���I");
                break;
            case "Dires":
                Debug.Log($"{name}:���o�����I");
                break;
            case "Debuff":
                Debug.Log($"{name}:�f�o�t���ʔ����I");
                break;
            case "Battery":
                Debug.Log($"{name}:�o�b�e���[���l���I");
                Debug.Log("�o�b�e���[���񕜂��܂���");
                break;
            default:
                Debug.Log($"{name}: �ʏ�}�X - ���ʂȂ��B");
                break;
        }
    }

    private void DisplayRandomEvent()
    {
        string[] eventMessages = {
            "�h�A���J���܂����I",
            "�N���[�[�b�g�ɉB�����",
            "�}�ɖ��C���������Ă����B"
        };

        System.Random random = new System.Random();
        int randomIndex = random.Next(eventMessages.Length);

        Debug.Log($"{name}: �C�x���g�����I {eventMessages[randomIndex]}");
    }

    public void LogCellArrival()
    {
        Debug.Log($"�v���C���[�� {name} �ɓ��B���܂����B");
    }
}