using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
   // public PlayerMover playerMover;
    public string cellEffect = "Normal"; // �}�X�ڂ̌��ʁi��: Normal, Bonus, Penalty�j

    //void OnTriggerEnter(Collider other)
    //{

    //    //�^�O��"Player"�̃I�u�W�F�N�g�̂ݔ���
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log($"{name} �Ƀv���C���[�����B���܂����B�}�X�̎�ށF{cellEffect}");

    //        ExecuteEvent();
          
    //    } 
    //}
    public void ExecuteEvent()
    {
       
        // �}�X�ڂ̌��ʂ𔭓�
        switch (cellEffect)
        {
            case "Event":
                Debug.Log($"{name}: �C�x���g�����I");
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

         
    }
   
}
