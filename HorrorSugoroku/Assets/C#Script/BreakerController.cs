using UnityEngine;

public class BreakerController : MonoBehaviour
{
    public Light elevatorLight;
    public bool breaker = false;

    void Start()
    {
        // GameObject型の配列cubesに、"box"タグのついたオブジェクトをすべて格納
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("light");
    }

    void Update()
    {
        //ブレーカーON(ifの内容はのちに変えます)
        if (Input.GetKeyDown(KeyCode.B) && !breaker)
        {
            BreakerOn();
        }
    }

    void BreakerOn()
    {
        breaker = true;
        elevatorLight.enabled = true;
    }
}
