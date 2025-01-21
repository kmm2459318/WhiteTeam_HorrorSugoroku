using UnityEngine;
using UnityEngine.UI;

public class PlayerSaikoro : MonoBehaviour
{
    private EnemySaikoro targetScript; // コマンドを受け取るEnemySaikoro
    private int sai = 1; // ランダムなサイコロの値
    private bool saikorotyu = false; // サイコロを振っているか
    private bool idoutyu = false;
    private float delta = 0; // 時間の計測
    private int ii = 0; // 繰り返し回数
    private int detame = 0; //出た値（ストッパー）
    private bool PN = false; // プレイヤーの東西南北
    private bool PW = false;
    private bool PE = false;
    private bool PS = false;
    private int[] lastAction = new int[7]; // 前の行動の記録【北：１、西：２、東：３、南：４】
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    public GameObject saikoro;
    public GameObject Player;
    public GameObject PNorth;
    public GameObject PWest;
    public GameObject PEast;
    public GameObject PSouth;
    Vector3 Pos;
    Image image;

    [System.Obsolete]
    void Start()
    {
        // プレイヤーシーンがロードされる際に、EnemySaikoroを探して参照を保持
        targetScript = FindObjectOfType<EnemySaikoro>();

        // サイコロのImageを保持
        image = saikoro.GetComponent<Image>();

        saikoro.SetActive(false);

        // Enemyがシーンに存在しない場合、エラーメッセージを出力
        if (targetScript == null)
        {
            Debug.LogError("EnemySaikoro not found in the scene.");
        }
    }

    void Update()
    {
        Pos = Player.transform.position;
        PN = PNorth.GetComponent<PlayerNSEWCheck>().masuCheck;
        PW = PWest.GetComponent<PlayerNSEWCheck>().masuCheck;
        PE = PEast.GetComponent<PlayerNSEWCheck>().masuCheck;
        PS = PSouth.GetComponent<PlayerNSEWCheck>().masuCheck;

        //サイコロ表示
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

        //サイコロ振る
        if ((Input.GetKeyDown(KeyCode.E) || saikorotyu) && !idoutyu)
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
                    //Debug.Log("Player rolling: " + sai);
                    ii++;
                }
                else
                {
                    sai = Random.Range(1, 7);
                    detame = sai;
                    //Debug.Log("Player rolled: " + sai);

                    // プレイヤーのサイコロの結果に応じてEnemyのサイコロ範囲を決定
                    targetScript.RollEnemyDice(sai);

                    ii = 0;
                    saikorotyu = false;
                    idoutyu = true;
                }
            }
        }

        //移動処理　【北：１、西：２、東：３、南：４】
        if (idoutyu == true)
        {
            if (Input.GetKeyDown(KeyCode.W) && PN)
            {
                if (lastAction[detame - sai] == 4)
                {
                    idou(1, true);
                }
                else
                {
                    idou(1, false);
                }
            }
            if (Input.GetKeyDown(KeyCode.A) && PW)
            {
                if (lastAction[detame - sai] == 3)
                {
                    idou(2, true);
                }
                else
                {
                    idou(2, false);
                }
            }
            if (Input.GetKeyDown(KeyCode.S) && PS)
            {
                if (lastAction[detame - sai] == 1)
                {
                    idou(4, true);
                }
                else
                {
                    idou(4, false);
                }
            }
            if (Input.GetKeyDown(KeyCode.D) && PE)
            {
                if (lastAction[detame - sai] == 2)
                {
                    idou(3, true);
                }
                else
                {
                    idou(3, false);
                }
            }
            if (sai < 1)
            {
                idoutyu = false;
                saikoro.SetActive(false);
            }
        }
    }

    void idou(int n, bool back)
    {
        switch (n)
        {
            case 1: Pos.z += 2.0f; break;
            case 2: Pos.x -= 2.0f; break;
            case 3: Pos.x += 2.0f; break;
            case 4: Pos.z -= 2.0f; break;
        }
        Player.transform.position = Pos; //移動

        if (!back)
        {
            lastAction[detame - sai + 1] = n; //来た方向記憶
            Debug.Log(detame - sai + 1 + ":" + lastAction[detame - sai + 1]);
            sai--;
        }
        else
        {
            sai++;
        }
    }
}