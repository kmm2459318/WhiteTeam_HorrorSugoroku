using UnityEngine;

public class TestItem : MonoBehaviour
{
    [SerializeField] private SheetData Itemsheet;
    void Start()
    {

        Debug.Log("ID:" + Itemsheet.Itemsheet[0].ID);
        Debug.Log("�A�C�e����:" + Itemsheet.Itemsheet[0].Name);
        Debug.Log("�^�C�v:" + Itemsheet.Itemsheet[0].type);
        Debug.Log("�ŏ��w�萔:" + Itemsheet.Itemsheet[0].DiseMin);
        Debug.Log("�ő�w�萔:" + Itemsheet.Itemsheet[0].DiseMax);
        Debug.Log("�{�����[��:" + Itemsheet.Itemsheet[0].UsedTurn);
        Debug.Log("���ʂ�������܂ł̃^�[��:" + Itemsheet.Itemsheet[0].UsedTurn);
    }
}
