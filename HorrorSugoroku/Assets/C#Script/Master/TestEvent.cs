using UnityEngine;

public class TestEvent : MonoBehaviour
{
    [SerializeField] private Master_Event EventSheet;

    public int n = 0;
    void Start()
    {
        Debug.Log("ID:" + EventSheet.EventSheet[n].ID);
        Debug.Log("�C�x���g��:" + EventSheet.EventSheet[n].Name);
        Debug.Log("�����K�v��:" + EventSheet.EventSheet[n].NeedKey);
        Debug.Log("�����ɂ����邩:" + EventSheet.EventSheet[n].InNarrowRoom);
        Debug.Log("��������ɂ�鐬�۔��肪�K�v��:" + EventSheet.EventSheet[n].Detemination);
    }
}
