using UnityEngine;

public class TestDebuff : MonoBehaviour
{
    [SerializeField] private Master_Debuff DebuffSheet;

    public int n = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("ID:" + DebuffSheet.DebuffSheet[n].ID);
        Debug.Log("イベント名:" + DebuffSheet.DebuffSheet[n].Name);
        Debug.Log("何ターン止まるか:" + DebuffSheet.DebuffSheet[n].StopTurn);
        Debug.Log("最小移動量:" + DebuffSheet.DebuffSheet[n].BackMin);
        Debug.Log("最大移動量:" + DebuffSheet.DebuffSheet[n].BackMax);
        Debug.Log("懐中電灯の最小ゲージ減少量:" + DebuffSheet.DebuffSheet[n].DecreaseMin);
        Debug.Log("懐中電灯の最大ゲージ減少量:" + DebuffSheet.DebuffSheet[n].DecreaseMax);
        Debug.Log("呪いを付与するかの判定:" + DebuffSheet.DebuffSheet[n].Curse);
        Debug.Log("アイテムを付与するかの判定:" + DebuffSheet.DebuffSheet[n].ItemGive);
    }

}
