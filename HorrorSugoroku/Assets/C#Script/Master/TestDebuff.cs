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
        Debug.Log("���^�[���~�܂邩:" + DebuffSheet.DebuffSheet[n].StopTurn);
        Debug.Log("�ŏ��ړ���:" + DebuffSheet.DebuffSheet[n].BackMin);
        Debug.Log("�ő�ړ���:" + DebuffSheet.DebuffSheet[n].BackMax);
        Debug.Log("�����d���̍ŏ��Q�[�W������:" + DebuffSheet.DebuffSheet[n].DecreaseMin);
        Debug.Log("�����d���̍ő�Q�[�W������:" + DebuffSheet.DebuffSheet[n].DecreaseMax);
        Debug.Log("�􂢂�t�^���邩�̔���:" + DebuffSheet.DebuffSheet[n].Curse);
        Debug.Log("�A�C�e����t�^���邩�̔���:" + DebuffSheet.DebuffSheet[n].ItemGive);
    }

}
