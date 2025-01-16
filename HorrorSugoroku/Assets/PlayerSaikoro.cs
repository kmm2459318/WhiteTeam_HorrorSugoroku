using UnityEngine;
using UnityEngine.UI;

public class PlayerSaikoro : MonoBehaviour
{
    int Psaikoro = 0;
    int sai = 1;
    bool saikorotyu = false;
    bool idoutyu = false;
    float delta = 0;
    int ii = 0;
    int nn = 0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    public GameObject saikoro;
    public GameObject Player;
    Image image;

    void Start()
    {
        image = saikoro.GetComponent<Image>();
        saikoro.SetActive(false);
    }

    void Update()
    {
        Vector3 Pos = Player.transform.position;
        //サイコロ表示
        switch (Psaikoro)
        {
            case 1:
                image.sprite = s1; break;
            case 2:
                image.sprite = s2; break;
            case 3:
                image.sprite = s3; break;
            case 4:
                image.sprite = s4; break;
            case 5:
                image.sprite = s5; break;
            case 6:
                image.sprite = s6; break;
        }

        //ダイスロール1d6
        if ((Input.GetKeyDown(KeyCode.E) || saikorotyu == true) && idoutyu == false)
        {
            saikorotyu = true;
            this.delta += Time.deltaTime;
            if (this.delta > 0.1f)
            {
                this.delta = 0f;
                if (ii < 7)
                {
                    sai = Random.Range(1, 7);
                    saikoro.SetActive(true);
                    //Debug.Log(sai);
                    ii++;
                    switch (sai)
                    {
                        case 1:
                            image.sprite = s1; break;
                        case 2:
                            image.sprite = s2; break;
                        case 3:
                            image.sprite = s3; break;
                        case 4:
                            image.sprite = s4; break;
                        case 5:
                            image.sprite = s5; break;
                        case 6:
                            image.sprite = s6; break;
                    }
                }
                if (ii == 7)
                {
                    Psaikoro = Random.Range(1, 7);
                    nn = Psaikoro;
                    Debug.Log("Player:" + Psaikoro);
                    ii = 0;
                    saikorotyu = false;
                    idoutyu = true;
                }
            }
        }

        //移動処理
        if (idoutyu == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Pos.z += 2.0f;
                Player.transform.position = Pos;
                Psaikoro--;
            }
            if (Input.GetKeyDown(KeyCode.S) && nn > Psaikoro)
            {
                Pos.z -= 2.0f;
                Player.transform.position = Pos;
                Psaikoro++;
            }
            if (Psaikoro < 1)
            {
                idoutyu = false;
                saikoro.SetActive(false);
            }
        }
    }
}
