using UnityEngine;

public class TestEvent : MonoBehaviour
{
    [SerializeField] private Master_Event EventSheet;

    public int n = 0;
    void Start()
    {
        Debug.Log("ID:" + EventSheet.EventSheet[n].ID);
        Debug.Log("イベント名:" + EventSheet.EventSheet[n].Name);
        Debug.Log("選択肢の数:" + EventSheet.EventSheet[n].Choices);
        Debug.Log("懐中電灯の消費量:" + EventSheet.EventSheet[n].Consumption);
        Debug.Log("音が出るか:" + EventSheet.EventSheet[n].MakeaSound);
        Debug.Log("お化けが追いかけてくるか:" + EventSheet.EventSheet[n].DemonChase);
        Debug.Log("アイテムが使えるか:" + EventSheet.EventSheet[n].ItemUse);
        Debug.Log("アイテムがもらえるか:" + EventSheet.EventSheet[n].ItemGet);
        Debug.Log("アイテムを失うか:" + EventSheet.EventSheet[n].Itemlose);
    }
}
