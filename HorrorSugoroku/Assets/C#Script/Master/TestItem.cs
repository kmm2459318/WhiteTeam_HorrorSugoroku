using UnityEngine;

public class TestItem : MonoBehaviour
{
    [SerializeField] private SheetData Itemsheet;
    void Start()
    {

        Debug.Log("ID:" + Itemsheet.Itemsheet[0].ID);
        Debug.Log("アイテム名:" + Itemsheet.Itemsheet[0].Name);
        Debug.Log("タイプ:" + Itemsheet.Itemsheet[0].type);
        Debug.Log("最小指定数:" + Itemsheet.Itemsheet[0].DiseMin);
        Debug.Log("最大指定数:" + Itemsheet.Itemsheet[0].DiseMax);
        Debug.Log("ボリューム:" + Itemsheet.Itemsheet[0].UsedTurn);
        Debug.Log("効果が消えるまでのターン:" + Itemsheet.Itemsheet[0].UsedTurn);
    }
}
