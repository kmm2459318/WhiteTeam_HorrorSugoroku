using UnityEngine;

public class TestItem : MonoBehaviour
{
    [SerializeField] private Master_Item ItemSheet;

    public int n = 0; 
    void Start()
    {
        Debug.Log("ID:" + ItemSheet.ItemSheet[n].ID);
        Debug.Log("アイテム名:" + ItemSheet.ItemSheet[n].Name);
        Debug.Log("タイプ:" + ItemSheet.ItemSheet[n].type);
        Debug.Log("回復量:" + ItemSheet.ItemSheet[n].Recovery);
        Debug.Log("ボリューム:" + ItemSheet.ItemSheet[n].Volume);
        Debug.Log("効果が消えるまでのターン:" + ItemSheet.ItemSheet[n].UsedTurn);
    }
}
