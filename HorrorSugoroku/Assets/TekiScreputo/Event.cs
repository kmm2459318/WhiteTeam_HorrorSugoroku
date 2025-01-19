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
            Debug.Log($"{name} �Ƀv���C���[�����B���܂����i�A�C�e���}�X�j");

          
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
