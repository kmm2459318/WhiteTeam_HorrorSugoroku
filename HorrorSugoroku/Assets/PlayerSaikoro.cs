using UnityEngine;

public class PlayerSaikoro : MonoBehaviour
{
    int Psaikoro = 0;
    int sai = 1;
    bool saikorotyu = false;
    float delta = 0;
    int ii = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || saikorotyu == true)
        {
            saikorotyu = true;
            this.delta += Time.deltaTime;
            if (this.delta > 0.1f)
            {
                this.delta = 0f;
                if (ii < 7)
                {
                    sai = Random.Range(1, 7);
                    Debug.Log(sai);
                    ii++;
                }
                if (ii == 7)
                {
                    Psaikoro = Random.Range(1, 7);
                    Debug.Log("Player:" + Psaikoro);
                    ii = 0;
                    saikorotyu = false;
                }
            }
        }
    }
}
