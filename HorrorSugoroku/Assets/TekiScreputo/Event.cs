using UnityEngine;

public class Event : MonoBehaviour
{
    public string cellEffect = "Normal"; // �}�X�ڂ̌��ʁi��: Normal, Bonus, Penalty�j

    public void ActivateEffect()
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
    void OnTriggerEnter(Collider other)
    {
        // �^�O��"Player"�̃I�u�W�F�N�g�̂ݔ���
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{name} �Ƀv���C���[�����B���܂���");

          
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
