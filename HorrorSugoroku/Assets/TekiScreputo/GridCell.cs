using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public string cellEffect = "Normal"; // �}�X�ڂ̌��ʁi��: Normal, Bonus, Penalty�j

    public void ExecuteEvent()
    {
        switch (cellEffect)
        {
            case "Event":
                Debug.Log($"{name}: �C�x���g�����I");
                Debug.Log("�h�A���J���܂����I"); // �h�A���J�������Ƃ������f�o�b�O���O��ǉ�
                break;
            case "Blockl":
                Debug.Log($"{name}: �y�i���e�B���ʔ����I");
                break;
            case "Item":
                Debug.Log($"{name}:�A�C�e�����l���I");
                break;
            case " Direc":
                Debug.Log($"{name}:���o�����I");
                break;
            case "Debuff":
                Debug.Log($"{name}:�f�o�t���ʔ����I");
                break;
            case "Battery":
                Debug.Log($"{name}:�o�b�e���[���l���I");
                break;
            default:
                Debug.Log($"{name}: �ʏ�}�X - ���ʂȂ��B");
                break;
        }

        Debug.Log("ExecuteEvent");
    }
}