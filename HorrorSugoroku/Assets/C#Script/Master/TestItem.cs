using UnityEngine;

public class TestItem : MonoBehaviour
{
    [SerializeField] private Master_Item ItemSheet;

    public int n = 0; 
    void Start()
    {
        Debug.Log("ID:" + ItemSheet.ItemSheet[n].ID);
        Debug.Log("�A�C�e����:" + ItemSheet.ItemSheet[n].Name);
        Debug.Log("�^�C�v:" + ItemSheet.ItemSheet[n].type);
        Debug.Log("�񕜗�:" + ItemSheet.ItemSheet[n].Recovery);
        Debug.Log("�{�����[��:" + ItemSheet.ItemSheet[n].Volume);
        Debug.Log("���ʂ�������܂ł̃^�[��:" + ItemSheet.ItemSheet[n].UsedTurn);
    }
}
