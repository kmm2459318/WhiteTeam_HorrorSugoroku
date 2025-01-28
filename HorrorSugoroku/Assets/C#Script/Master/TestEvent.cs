using UnityEngine;

public class TestEvent : MonoBehaviour
{
    [SerializeField] private Master_Event EventSheet;

    public int n = 0;
    void Start()
    {
        Debug.Log("ID:" + EventSheet.EventSheet[n].ID);
        Debug.Log("イベント名:" + EventSheet.EventSheet[n].Name);
        Debug.Log("鍵が必要か:" + EventSheet.EventSheet[n].NeedKey);
        Debug.Log("部屋にこもるか:" + EventSheet.EventSheet[n].InNarrowRoom);
        Debug.Log("さいころによる成否判定が必要か:" + EventSheet.EventSheet[n].Detemination);
    }
}
