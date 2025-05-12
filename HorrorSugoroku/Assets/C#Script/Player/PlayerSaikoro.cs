using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmoothigTransform;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class PlayerSaikoro : MonoBehaviour
{
    public DiceRotation diceRotation;
    public GameManager gameManager; // GameManagerへの参照
    public TurnManager turnManager; // TurnManagerへの参照
    public DiceController diceController;
    [SerializeField] SmoothTransform player;
    private EnemySaikoro targetScript; // コマンドを受け取るEnemySaikoro
    public CurseSlider curseGauge;
    public ElevatorIdou elevatorIdou;
    public EnemyStop enemyStop;
    public EnemyStop enemyStop1;
    public EnemyStop enemyStop2;
    public EnemyStop enemyStop3;
    public EnemyStop enemyStop4;
    public EnemyStop enemyStop5;
    public int sai = 1; // ランダムなサイコロの値
    private int walkCount = 0; // 進んだ回数
    public bool saikorotyu = true; // サイコロを振っているか
    public bool idoutyu = false;
    public bool exploring = false; // 探索中の判定（追加）
    private bool magarityu = false;
    private bool idouspan = false;
    private bool idouspanIkidomari = false;
    public bool enemyEnd = false;
    private float posFactZ = 0.6f;
    private float saikoroTime = 0; // サイコロの時間の計測
    private float magariTime = 0; // 曲がりの時間の計測
    private float idouspanTime = 0;
    private float idouspanIkidomariTime = 0;
    private float exploringCoolTime = 0;
    private float enemyendTime = 0;
    private int ii = 0; // 繰り返し回数
    private int detame = 0; //出た値（ストッパー）
    private bool PN = false; // プレイヤーの東西南北
    private bool PW = false;
    private bool PE = false;
    private bool PS = false;
    private int[] lastAction = new int[7]; // 前の行動の記録【北：１、西：２、東：３、南：４】
    private int lastaction;
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
    public GameObject Enemy;
    public GameObject PNorth;
    public GameObject PWest;
    public GameObject PEast;
    private Transform Smasu;
    private Transform Nmasu;
    private Transform Wmasu;
    private Transform Emasu;
    public Transform startMasu;
    private List<Transform> parentTransform = new List<Transform>();
    private List<Material> parentTransformlast = new List<Material>();
    //private Transform parentTransform;
    public Transform nextDarkMasu;
    private Transform lastMasu;
    public Material darkMaterial;
    public Material neonMaterial;
    public GameObject PSouth;
    public GameObject Camera;
    Vector3 lastPos = new Vector3(0, 0, 0);
    private int minDiceValue = 1;
    private int maxDiceValue = 6;
    Vector3 nnn = new Vector3(0, 0, 0);
    //Vector3 Pos;
    Vector3 Rotation;
    Vector3 Rot;
    int i;
    Image image;

    [SerializeField] private RawImage diceUI; // UI参照
    [SerializeField] private Camera diceCamera; // カメラ参照
    private float diceCameraHideDelay = 0.5f; // 🎯 非表示にするまでの遅延時間（秒）

    int movesum;

    int number;
    
    public bool gaugeCircle;
    public bool diceLight;

    // AudioSource and AudioClip variables for dice roll sound
    private AudioSource audioSource; // AudioSource to play sound

    public DiceRangeManager diceRangeManager; // DiceRangeManagerへの参照
    private bool legButtonEffect = false; // LegButtonの効果を管理するフラグ

    [System.Obsolete]
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        player.PosFact = 0.3f;

        // プレイヤーシーンがロードされる際に、EnemySaikoroを探して参照を保持
        targetScript = FindObjectOfType<EnemySaikoro>();
        // DiceRangeManagerのインスタンスを取得
        // 他の初期化コード...
        diceRangeManager = FindObjectOfType<DiceRangeManager>();
        diceController = FindObjectOfType<DiceController>(); // DiceControllerの参照を取得

        if (diceController == null)
        {
            Debug.LogError("DiceController is not assigned and could not be found in the scene.");
        }

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

        nextDarkMasu = startMasu;

        // スプライトのサイズを変更
        ChangeSpriteSize(s1, new Vector2(200, 200));
        ChangeSpriteSize(s2, new Vector2(200, 200));
        ChangeSpriteSize(s3, new Vector2(200, 200));
        ChangeSpriteSize(s4, new Vector2(200, 200));
        ChangeSpriteSize(s5, new Vector2(200, 200));
        ChangeSpriteSize(s6, new Vector2(200, 200));

        if (diceUI != null) diceUI.gameObject.SetActive(false);
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
        Nmasu = PNorth.GetComponent<PlayerCloseMass>().GetClosestObject();
        Wmasu = PWest.GetComponent<PlayerCloseMass>().GetClosestObject();
        Emasu = PEast.GetComponent<PlayerCloseMass>().GetClosestObject();
        Smasu = PSouth.GetComponent<PlayerCloseMass>().GetClosestObject();

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


        if (!saikorotyu && !idoutyu && gameManager.isPlayerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
            // サイコロの出目を制限
            sai = UnityEngine.Random.Range(minDiceValue, maxDiceValue + 1);

        }

        // サイコロ振る
        if (saikorotyu)
        {
            sai = RollDiceWithLegEffect();
            //Debug.Log("最終的なサイコロの出目: " + sai);

            // サイコロの出目に応じてスプライトを更新
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

        //移動処理　【北：１、西：２、東：３、南：４】
        if (idoutyu)
        {
            if (((!PW && !PN && !PE && lastaction == 1) ||
                (!PW && !PS && !PN && lastaction == 2) ||
                (!PN && !PS && !PE && lastaction == 3) ||
                (!PW && !PS && !PE && lastaction == 4)) &&
                !idouspanIkidomari && !elevatorIdou.idou)
            {
                Debug.Log("行き止まり");
                sai = 0;
            }

            if (Input.GetKeyDown(KeyCode.W) && !idouspan && sai > 0)
            {
                idouspan = true;
                idouspanIkidomari = true;
                idouspanIkidomariTime = 0f;
                if (PN && (Rot.y >= 0f && Rot.y < 45f) || (Rot.y >= 315f && Rot.y < 360f))
                {
                    FrontBack(1);
                    Debug.Log("North");
                }
                else if (PW && Rot.y >= 225f && Rot.y < 315f)
                {
                    FrontBack(2);
                    Debug.Log("West");
                }
                else if (PE && Rot.y >= 45f && Rot.y < 135f)
                {
                    FrontBack(3);
                    Debug.Log("East");
                }
                else if (PS && Rot.y >= 135f && Rot.y < 225f)
                {
                    FrontBack(4);
                    Debug.Log("South");
                }
            }

            this.idouspanTime += Time.deltaTime;
            if (idouspanTime > player.PosFact)
            {
                idouspanTime = 0f;
                idouspan = false;
            }

            this.idouspanIkidomariTime += Time.deltaTime;
            if (idouspanIkidomariTime > player.PosFact + posFactZ)
            {
                idouspanIkidomariTime = 0f;
                idouspanIkidomari = false;
            }

            if (sai < 1)
            {
                //MasuColorChange(neonMaterial);
                for (int i = 0; i < walkCount; i++)
                {
                    MasuColorChange(parentTransformlast[i], parentTransform[i]);
                }
                parentTransform = new List<Transform>();
                parentTransformlast = new List<Material>();

                turnManager.turnStay = false;
                saikoro.SetActive(false);
                walkCount = 0;
                curseGauge.Update();

                // プレイヤーの動き終了
                //呪い溜まってないか判定(ここまでには呪いの判定を済ませとく)
                StartCoroutine(idoutyuHantei());
            }
        }

        if (exploring)
        {
            this.exploringCoolTime += Time.deltaTime;
        }

        // Fキーを押したら探索を終了し、次のターンへ
        if ((exploring && Input.GetKeyDown(KeyCode.F)) || enemyEnd)
        {
            enemyEnd = true;
            if (enemyStop.stopMasu)
            {
                FinishTurn();
            }
        }
        // 探索中の判定をtrueにする
        // ボタン、スペースキーを押したときに探索の判定をfalseにする
        // ボタン、スペースを押したときにNextTurnを動かす

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
        diceRotation.GetDiceNumber(sai);

        if (saikoro.active == true)
        {
            if (diceUI != null) diceUI.gameObject.SetActive(true);
            diceLight = true;
        }
        else if (saikoro.active == false)
        {
            //if (diceUI != null) diceUI.gameObject.SetActive(false);
        }
    }

    void FinishTurn()
    {
        enemyEnd = false;
        exploring = false;
        Debug.Log("探索モード終了、次のターンへ");
        lastaction = 0;
        lastMasu = nextDarkMasu;
        gameManager.NextTurn();
        if (diceUI != null) diceUI.gameObject.SetActive(false);
    }

    IEnumerator idoutyuHantei()
    {
        yield return new WaitForSeconds(0.6f);
        yield return null;

        if (!curseGauge.isCardCanvas1 && !curseGauge.isCardCanvas2)
        {
            idoutyu = false;
        }

        // プレイヤーの移動が終了したら探索モードに入る
        if (!idoutyu && !saikorotyu && !exploring)
        {
            exploring = true;
            Debug.Log("探索モードに入りました:Fを押して次に");
            gameManager.NextTurn();
        }
    }

    void ChangeSpriteSize(Sprite sprite, Vector2 newSize)
    {
        RectTransform rectTransform = saikoro.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = newSize;
        }
        else
        {
            Debug.LogError("RectTransform component is missing on the saikoro GameObject.");
        }
    }
    public void DiceRoll()
    {
        saikorotyu = true;
        if (diceCamera != null) diceCamera.enabled = true;
    }

    public void DiceAfter(int n)
    {
        //呪２による減少
        if (curseGauge.curse1_2)
        {
            if (n == 5 ||  n == 6)
            {
                n = 4;
            }
            //curseGauge.curse1Turn--;
        }

        sai = n;
        detame = sai;
        saikoro.SetActive(true);

        StartCoroutine(HideDiceCameraWithDelay()); // 🎯 カメラの非表示を遅延
        // プレイヤーのサイコロの結果に応じてEnemyのサイコロ範囲を決定
        //targetScript.RollEnemyDice();

        i += sai;
        PlayerPrefs.SetInt("move", i);
        ii = 0;
        saikorotyu = false;
        idoutyu = true;
    }


    void FrontBack(int n)
    {
        Transform nextMasu = startMasu;
        switch (n)
        {
            case 1: nextMasu = Nmasu; break;
            case 2: nextMasu = Wmasu; break;
            case 3: nextMasu = Emasu; break;
            case 4: nextMasu = Smasu; break;
        }

        if (lastMasu == nextMasu)
        {
            idou(n, true);
        }
        else
        {
            idou(n, false);
        }
    }

    //移動
    [SerializeField] private AudioSource footstepSound; // 足音のAudioSource

    void idou(int n, bool back)
    {
        // 現在のPlayerのY軸の値を保持
        //Pos = Player.transform.position;

        //Player.transform.position = Pos; // 移動

        if (!back)
        {
            switch (n)
            {
                case 1:
                    //MasuColorChange(neonMaterial);
                    parentTransform.Add(nextDarkMasu);
                    parentTransformlast.Add(nextDarkMasu.GetChild(1).GetComponent<Renderer>().material);
                    lastMasu = nextDarkMasu;
                    nextDarkMasu = Nmasu;
                    player.TargetPosition = Nmasu.transform.position + new Vector3(0, 1.15f, 0); break; // 北に移動
                case 2:
                    //MasuColorChange(neonMaterial);
                    //parentTransform = nextDarkMasu;
                    parentTransform.Add(nextDarkMasu);
                    parentTransformlast.Add(nextDarkMasu.GetChild(1).GetComponent<Renderer>().material);
                    lastMasu = nextDarkMasu;
                    nextDarkMasu = Wmasu;
                    player.TargetPosition = Wmasu.transform.position + new Vector3(0, 1.15f, 0); break; // 西に移動
                case 3:
                    //MasuColorChange(neonMaterial);
                    //parentTransform = nextDarkMasu;
                    parentTransform.Add(nextDarkMasu);
                    parentTransformlast.Add(nextDarkMasu.GetChild(1).GetComponent<Renderer>().material);
                    lastMasu = nextDarkMasu;
                    nextDarkMasu = Emasu;
                    player.TargetPosition = Emasu.transform.position + new Vector3(0, 1.15f, 0); break; // 東に移動
                case 4:
                    //MasuColorChange(neonMaterial);
                    //parentTransform = nextDarkMasu;
                    parentTransform.Add(nextDarkMasu);
                    parentTransformlast.Add(nextDarkMasu.GetChild(1).GetComponent<Renderer>().material);
                    lastMasu = nextDarkMasu;
                    nextDarkMasu = Smasu;
                    player.TargetPosition = Smasu.transform.position + new Vector3(0, 1.15f, 0); break; // 南に移動
            }

            // 足音を鳴らす
            if (footstepSound != null)
            {
                footstepSound.Play();
                StartCoroutine(StopFootstepSound()); // 1秒後に止める
            }
            else
            {
                Debug.LogWarning("Footstep sound is not assigned.");
            }

            MasuColorChange(darkMaterial, parentTransform[detame - sai]);
            Debug.Log("天井の研ナ〇コが気になって眠れない");
            lastaction = n; // 来た方向を記憶
            Debug.Log(detame - sai + 1 + ":" + lastAction[detame - sai + 1]);
            walkCount++;
            sai--;
            diceRotation.GetDiceNumber(sai);
            if (sai == 0)
            {
                gaugeCircle = true;
                diceLight = false;
            }
        }
    }

    IEnumerator StopFootstepSound()
    {
        yield return new WaitForSeconds(1f); // 1秒待つ
        footstepSound.Stop();               // 音を止める
    }


    private IEnumerator RollDice()
    {
        saikoro.SetActive(true);
        for (int i = 0; i < 10; i++) // 10回ランダムに目を表示
        {
            sai = UnityEngine.Random.Range(1, 7);
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
    public void SetDiceRollRange(int min, int max)
    {
        minDiceValue = min;
        maxDiceValue = max;
    }

    /*public void NextTurn()
    {
        if (exploring){
            enemyEnd = true;
            exploring = false;
            if ((lastPos.x + 0.0001f > Enemy.transform.position.x && lastPos.x - 0.0001f < Enemy.transform.position.x) &&
            (lastPos.z + 0.0001f > Enemy.transform.position.z && lastPos.z - 0.0001f < Enemy.transform.position.z) &&
            !targetScript.enemyidoutyu)
            {
                enemyEnd = false;
                targetScript.idouspanTime = 0f;
                Debug.Log("探索モード終了、次のターンへ");
                gameManager.NextTurn();
            }
            lastPos = Enemy.transform.position;
        }
    }*/
    public void SetLegButtonEffect(bool isActive)
    {
        legButtonEffect = isActive;
    }

    public int RollDiceWithLegEffect()
    {
        int roll = UnityEngine.Random.Range(minDiceValue, maxDiceValue + 1);
        if (legButtonEffect)
        {
            switch (roll)
            {
                case 4:
                    roll = 1;
                    break;
                case 5:
                    roll = 2;
                    break;
                case 6:
                    roll = 3;
                    break;
            }
        }
        return roll;
    }

    private void MasuColorChange(Material n, Transform transform)
    {
        if (transform == null)
        {
            return;
        }

        // 子オブジェクトの4つの子オブジェクトを取得してマテリアルを変更
        for (int i = 0; i < transform.childCount && i < 4; i++)
        {
            Transform grandChild = transform.GetChild(i);
            Renderer renderer = grandChild.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material = n;
            }
        }
    }
    public IEnumerator HideDiceCameraWithDelay()
    {
        yield return new WaitForSeconds(diceCameraHideDelay); // 指定した秒数待機
        if (diceCamera != null) diceCamera.enabled = false; // 🎯 指定時間後にカメラを非表示
        gaugeCircle = false;
    }
}