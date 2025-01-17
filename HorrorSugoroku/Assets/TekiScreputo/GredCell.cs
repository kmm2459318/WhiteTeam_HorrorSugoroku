using UnityEngine;

public class GredCell : MonoBehaviour
{
    public string cellType = "Normal"; // �}�X�ڂ̎�ށi��: Normal, Bonus, Penalty�j

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{name} �Ƀv���C���[�����B���܂����B�}�X�̎��: {cellType}");

            // �}�X�ڂ̎�ނɉ������C�x���g�����s
            ExecuteEvent();
        }
    }

    void ExecuteEvent()
    {
        switch (cellType)
        {
            case "Bonus":
                Debug.Log("�{�[�i�X�}�X: �v���C���[���{�[�i�X���l���I");
                break;
            case "Penalty":
                Debug.Log("�y�i���e�B�}�X: �v���C���[���y�i���e�B���󂯂܂����I");
                break;
            default:
                Debug.Log("�ʏ�}�X: ���ɃC�x���g�Ȃ��B");
                break;
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
