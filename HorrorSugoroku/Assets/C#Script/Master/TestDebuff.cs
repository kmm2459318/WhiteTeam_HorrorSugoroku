using UnityEngine;

public class TestDebuff : MonoBehaviour
{
    [SerializeField] private Master_Debuff DebuffSheet;

    public int n = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("ID:" + DebuffSheet.DebuffSheet[n].ID);
        Debug.Log("�C�x���g��:" + DebuffSheet.DebuffSheet[n].Name);
        Debug.Log("�����d���̍ŏ��Q�[�W������:" + DebuffSheet.DebuffSheet[n].DecreaseMin);
        Debug.Log("�����d���̍ő�Q�[�W������:" + DebuffSheet.DebuffSheet[n].DecreaseMax);
        Debug.Log("�A�C�e����t�^���邩�̔���:" + DebuffSheet.DebuffSheet[n].ItemGive);
        Debug.Log("�A�C�e�����g���Ȃ��Ȃ邩�̔���:" + DebuffSheet.DebuffSheet[n].ItemGive);
        Debug.Log("�A�C�e�����g���Ȃ��^�[����:" + DebuffSheet.DebuffSheet[n].ItemGive);
    }

}
