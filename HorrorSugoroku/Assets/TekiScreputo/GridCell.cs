using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
   // public PlayerMover playerMover;
    public string cellEffect = "Normal"; // �}�X�ڂ̌��ʁi��: Normal, Bonus, Penalty�j
    public FlashlightController flashlightController;

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
            case "Dires":
                Debug.Log($"{name}:���o�����I");
                break;
            case "Debuff":
                Debug.Log($"{name}:�f�o�t���ʔ����I");
                DeBuh();
                break;
            case "Battery":
                Debug.Log($"{name}:�o�b�e���[���l���I");
                Batre();
                break;
            default:
                Debug.Log($"{name}: �ʏ�}�X - ���ʂȂ��B");

                break;
          
        }

         
    }
    void DeBuh()
    {
        int randomEvent = Random.Range(0, 2);

        if (randomEvent == 0)
        {
            Debug.Log("�f�o�t�C�x���gA�F�@�d�r�̃Q�[�W���������I");

            flashlightController.OnTurnAdvanced();
        }
        else
        {
            Debug.Log("�f�o�t�C�x���gB�F�A�C�e�����g���Ȃ��Ȃ���");
        }
    }
    void Batre()
    {
       
            Debug.Log("�o�b�e���[�񕜁F�o�b�e���[���񕜂���");

            flashlightController.AddBattery(20f);
        
    }

}
