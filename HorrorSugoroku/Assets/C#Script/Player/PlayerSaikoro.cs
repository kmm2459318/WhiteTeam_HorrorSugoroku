using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmoothigTransform;

public class PlayerSaikoro : MonoBehaviour
{
    public GameManager gameManager; // GameManagerへの参照
    public TurnManager turnManager; // TurnManagerへの参照
    public CameraChange cameraChange;
    public DiceController diceController;
    [SerializeField] SmoothTransform player;
    private EnemySaikoro targetScript; // コマンドを受け取るEnemySaikoro
    private int sai = 1; // ランダムなサイコロの値
    public bool saikorotyu = false; // サイコロを振っているか
    public bool idoutyu = false;
    private bool magarityu = false;
    private float saikoroTime = 0; // サイコロの時間の計測
    private float magariTime = 0; // 曲がりの時間の計測
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
    public GameObject PWest;
    public GameObject PEast;
    public GameObject PSouth;
    public GameObject Camera;
    //Vector3 Pos;
    Vector3 Rotation;
    Vector3 Rot;
    int i;
    Image image;

    int movesum;

    // AudioSource and AudioClip variables for dice roll sound
    private AudioSource audioSource; // AudioSource to play sound
    public AudioClip diceRollSound; // The sound to play when the dice rolls

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

        PlayerPrefs.SetInt("move", 0);

        // Get the AudioSource component and ensure the dice roll sound is assigned
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the PlayerSaikoro GameObject.");
        }

        if (diceRollSound == null)
        {
            Debug.LogError("DiceRollSound AudioClip is not assigned.");
        }
    }

    void Update()
    {
        //if (!gameManager.IsPlayerTurn())
            //Pos = Player.transform.position;
        Rot = Camera.transform.eulerAngles;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!saikorotyu && !idoutyu && gameManager.isPlayerTurn)
            {
                cameraChange.Change();
                turnManager.NextTurn();
            }
        }

        //サイコロ振る
        if (saikorotyu)
        {
            /*this.saikoroTime += Time.deltaTime;

            if (this.saikoroTime > 0.1f)
            {
                this.saikoroTime = 0f;

                if (ii < 7)
                {
                    sai = Random.Range(1, 7);
                    saikoro.SetActive(true);
                    // Play dice roll sound
                    PlayDiceRollSound();
                    ii++;
                }
                else
                {
                    sai = Random.Range(1, 7);
                    detame = sai;
                    Debug.Log("Player rolled: " + sai);*/

                    
                //}
            //}

        }

        //移動処理　【北：１、西：２、東：３、南：４】
        if (idoutyu)
        {
          //  Debug.Log(Rot.y);
            if (Input.GetKeyDown(KeyCode.W)) {
                if (PN && (Rot.y >= 0f && Rot.y < 45f) || (Rot.y >= 315f && Rot.y < 360f))
                {
                    FrontBack(1);
                    Debug.Log("North");
                }
                else if (PW && Rot.y >= 225f && Rot.y < 315f)
                {
                    FrontBack(2);
                    Debug.Log("East");
                }
                else if (PE && Rot.y >= 45f && Rot.y < 135f)
                {
                    FrontBack(3);
                    Debug.Log("West");
                }
                else if (PS && Rot.y >= 135f && Rot.y < 225f)
                {
                    FrontBack(4);
                    Debug.Log("South");
                }
            }
            if (sai < 1)
            {
                idoutyu = false;
                turnManager.turnStay = false;
                saikoro.SetActive(false);
                gameManager.NextTurn();
            }
        }

        //プレイヤー角度【北：１、西：２、東：３、南：４】
        /*if (Input.GetKeyDown(KeyCode.A) && !magarityu)
        {
            magarityu = true;
            player.TargetRotation *= Quaternion.Euler(0, -90, 0);
            switch (mesen)
            {
                case 1: mesen = 2; break;
                case 2: mesen = 4; break;
                case 3: mesen = 1; break;
                case 4: mesen = 3; break;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && !magarityu)
        {
            magarityu = true;
            player.TargetRotation *= Quaternion.Euler(0, 180, 0);
            switch (mesen)
            {
                case 1: mesen = 4; break;
                case 2: mesen = 3; break;
                case 3: mesen = 2; break;
                case 4: mesen = 1; break;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && !magarityu)
        {
            magarityu = true;
            player.TargetRotation *= Quaternion.Euler(0, 90, 0);
            switch (mesen)
            {
                case 1: mesen = 3; break;
                case 2: mesen = 1; break;
                case 3: mesen = 4; break;
                case 4: mesen = 2; break;
            }
        }

        //曲がり中判定
        if (magarityu)
        {
            PNorth.GetComponent<PlayerNSEWCheck>().masuCheck = false;
            PNorth.SetActive(false);
            //Debug.Log("曲がり中");

            this.magariTime += Time.deltaTime;

            if (this.magariTime > 0.6f)
            {
                this.magariTime = 0f;
                //Debug.Log("曲がり尾張");
                magarityu = false;
                PNorth.SetActive(true);
            }
        }*/
    }

    public void DiceRoll()
    {
        saikorotyu = true;
    }

    public void DiceAfter(int n)
    {
        cameraChange.Change();

        sai = n;
        detame = sai;
        saikoro.SetActive(true);
        // プレイヤーのサイコロの結果に応じてEnemyのサイコロ範囲を決定
        targetScript.RollEnemyDice();

        i += sai;
        PlayerPrefs.SetInt("move", i);
        ii = 0;
        saikorotyu = false;
        idoutyu = true;

        if (sai >= 1 && sai <= 3)
        {
            player.PosFact = 0.9f;
        }
        else
        {
            player.PosFact = 0.2f;
        }
    }

    // 音を再生するメソッド
    private void PlayDiceRollSound()
    {
        if (audioSource != null && diceRollSound != null)
        {
            audioSource.PlayOneShot(diceRollSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or DiceRollSound is not set.");
        }
    }

    void FrontBack(int n)
    {
        int m = 0;
        switch (n)
        {
            case 1: m = 4; break;
            case 2: m = 3; break;
            case 3: m = 2; break;
            case 4: m = 1; break;
        }

        if (lastAction[detame - sai] == m)
        {
            idou(n, true);
        }
        else
        {
            idou(n, false);
        }
    }

    //移動
    void idou(int n, bool back)
    {
        // 現在のPlayerのY軸の値を保持
        //Pos = Player.transform.position;

        

        //Player.transform.position = Pos; // 移動

        if (!back)
        {
            switch (n)
            {
                case 1: player.TargetPosition.z += 2.0f; break; // 北に移動
                case 2: player.TargetPosition.x -= 2.0f; break; // 西に移動
                case 3: player.TargetPosition.x += 2.0f; break; // 東に移動
                case 4: player.TargetPosition.z -= 2.0f; break; // 南に移動
            }

            lastAction[detame - sai + 1] = n; // 来た方向を記憶
            Debug.Log(detame - sai + 1 + ":" + lastAction[detame - sai + 1]);
            sai--;
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