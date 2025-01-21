using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmoothigTransform;

public class PlayerSaikoro : MonoBehaviour
{
    public GameManager gameManager; // GameManagerへの参照
    [SerializeField] SmoothTransform player;
    private EnemySaikoro targetScript; // コマンドを受け取るEnemySaikoro
    private int sai = 1; // ランダムなサイコロの値
    public bool saikorotyu = false; // サイコロを振っているか
    public bool idoutyu = false;
    private float delta = 0; // 時間の計測
    private int ii = 0; // 繰り返し回数
    private int detame = 0; //出た値（ストッパー）
    private bool PN = false; // プレイヤーの東西南北
    private bool PW = false;
    private bool PE = false;
    private bool PS = false;
    private int[] lastAction = new int[7]; // 前の行動の記録【北：１、西：２、東：３、南：４】
    private int mesen = 1; //目線【北：１、西：２、東：３、南：４】
    private float Pkakudo = 0; //プレイヤーのＹ軸角度
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    public GameObject saikoro;
    public GameObject Player;
    public GameObject PNorth;
    //public GameObject PWest;
    //public GameObject PEast;
    //public GameObject PSouth;
    Vector3 Pos;
    Vector3 Rot;
    Image image;

    int movesum;
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
            Debug.Log("Enemyが無いよ");
        }
        if (saikoro != null)
        {
            image = saikoro.GetComponent<Image>();
            saikoro.SetActive(false);
        }
        else
        {
            Debug.LogError("Saikoro GameObject is not assigned in the Inspector.");
        }

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager is not assigned and could not be found in the scene.");
            }
        }
    }

    void Update()
    {
        if (!gameManager.IsPlayerTurn())
        Pos = Player.transform.position;
        Rot = Player.transform.eulerAngles;
        PN = PNorth.GetComponent<PlayerNSEWCheck>().masuCheck;
        //PW = PWest.GetComponent<PlayerNSEWCheck>().masuCheck;
        //PE = PEast.GetComponent<PlayerNSEWCheck>().masuCheck;
        //PS = PSouth.GetComponent<PlayerNSEWCheck>().masuCheck;

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
        if (!idoutyu && saikorotyu)
        {
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
                    Debug.Log("Player rolled: " + sai);

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
                FrontBack(mesen);
            }
            if (sai < 1)
            {
                idoutyu = false;
                saikoro.SetActive(false);
                gameManager.NextTurn();
            }
        }

        //プレイヤー角度【北：１、西：２、東：３、南：４】
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.TargetRotation *= Quaternion.Euler(0, -90, 0);
            switch (mesen)
            {
                case 1: mesen = 2; break;
                case 2: mesen = 4; break;
                case 3: mesen = 1; break;
                case 4: mesen = 3; break;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.TargetRotation *= Quaternion.Euler(0, 180, 0);
            switch (mesen)
            {
                case 1: mesen = 4; break;
                case 2: mesen = 3; break;
                case 3: mesen = 2; break;
                case 4: mesen = 1; break;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.TargetRotation *= Quaternion.Euler(0, 90, 0);
            switch (mesen)
            {
                case 1: mesen = 3; break;
                case 2: mesen = 1; break;
                case 3: mesen = 4; break;
                case 4: mesen = 2; break;
            }
        }
    }

    public void DiceRoll()
    {
        saikorotyu = true;
    }

    void FrontBack(int n)
    {
        int m = 0;
        switch(n)
        {
            case 1: m = 4; break;
            case 2: m = 3; break;
            case 3: m = 2; break;
            case 4: m = 1; break;
        }

        if (lastAction[detame - sai] == m)
        {
            idou(mesen, true);
        }
        else
        {
            idou(mesen, false);
        }
    }

    //移動
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

    private IEnumerator RollDice()
    {
        saikoro.SetActive(true);
        for (int i = 0; i < 10; i++) // 10回ランダムに目を表示
        {
            sai = Random.Range(1, 7);
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
            yield return new WaitForSeconds(0.1f); // 0.1秒ごとに目を変更
        }

        detame = sai;
        Debug.Log("Player rolled: " + sai);

        saikorotyu = false;
        idoutyu = true;
    }

    public void StartRolling()
    {
        // このメソッドは空にしておくか、必要に応じて他の処理を追加します
    }
}