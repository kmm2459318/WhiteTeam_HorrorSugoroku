using UnityEngine;

public class TestEvent : MonoBehaviour
{
    [SerializeField] private Master_Event EventSheet;

    public int n = 0;
    void Start()
    {
        Debug.Log("ID:" + EventSheet.EventSheet[n].ID);
        Debug.Log("�C�x���g��:" + EventSheet.EventSheet[n].Name);
        Debug.Log("�I�����̐�:" + EventSheet.EventSheet[n].Choices);
        Debug.Log("�����d���̏����:" + EventSheet.EventSheet[n].Consumption);
        Debug.Log("�����o�邩:" + EventSheet.EventSheet[n].MakeaSound);
        Debug.Log("���������ǂ������Ă��邩:" + EventSheet.EventSheet[n].DemonChase);
        Debug.Log("�A�C�e�����g���邩:" + EventSheet.EventSheet[n].ItemUse);
        Debug.Log("�A�C�e�������炦�邩:" + EventSheet.EventSheet[n].ItemGet);
        Debug.Log("�A�C�e����������:" + EventSheet.EventSheet[n].Itemlose);
    }
}
