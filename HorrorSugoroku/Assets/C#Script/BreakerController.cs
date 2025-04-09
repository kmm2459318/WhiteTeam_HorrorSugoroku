using UnityEngine;

public class BreakerController : MonoBehaviour
{
    public GameObject ElevatorMasu;

    void Start()
    {
        // GameObject型の配列cubesに、"box"タグのついたオブジェクトをすべて格納
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("light");
    }

    void Update()
    {
        //ブレーカーON(ifの内容はのちに変えます)
        if (Input.GetKeyDown(KeyCode.B))
        {
            BreakerOn();
        }
    }

    void BreakerOn()
    {
        ElevatorMasu.SetActive(true);
    }
}
