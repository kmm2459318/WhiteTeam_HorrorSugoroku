using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class CurseSlider : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;  //呪いゲージTEXT
    [SerializeField] Slider DashGage;
    [SerializeField] SceneChanger3D sceneChanger;
    [SerializeField] GameObject CardCanvas1;
    [SerializeField] GameObject CardCanvas2;
    //小さい呪い
    [SerializeField] Button extraButton;
    [SerializeField] Button hideButton;
    [SerializeField] Button cursegiveButton;
    //大きな呪い
    [SerializeField] Button ArmButton;
    [SerializeField] Button LegButton;
    [SerializeField] Button EyeButton;

    [SerializeField] private Master_Curse master_Curse;
    [SerializeField] private Image[] ImageGages; // 画像ゲージ（下から上に増える）
    [SerializeField] private TextMeshProUGUI eyeButtonText;

    private float maxDashPoint = 100;
    private float dashIncreasePerTurn = 5;
    private int curseincrease = 0;

    public int CountGauge = 0;              //ゲームオーバーカウント
    public float dashPoint = 0;
    public GameManager gameManager;
    public TurnManager turnManager;
    public DiceController DiceController;
    public CutIn cutIn;
    private bool CardSelect1 = false;
    private bool CardSelect2 = false;
    private bool CardSelect3 = false;
    private bool CardSelect4 = false;
    private bool CardSelect5 = false;

    // ボタンの使用状態を管理
    private bool isArmButtonUsed = false;
    private bool isLegButtonUsed = false;
    private bool isEyeButtonUsed = false;
    public int sai = 1; // ランダムなサイコロの値（publicに変更）

    [SerializeField] private EyeEffectController eyeEffectController; // EyeEffectControllerの参照
    [SerializeField] private FlashlightManager flashlightManager; // FlashlightManagerの参照
    [SerializeField] private Transform playerTransform; // プレイヤーのTransform
    private PlayerSaikoro playerSaikoro; // PlayerSaikoroクラスのインスタンスを保持するフィールド
    [SerializeField] private DiceRangeManager diceRangeManager; // DiceRangeManagerへの参照
    public DiceController diceController; // DiceControllerへの参照
    [SerializeField] private GameObject eyeButtonCanvas;
    private bool isDisplayingText = false;
    [SerializeField] private Camera diceCamera;
    [SerializeField] private Camera MainCamera;
    // 笑い声の AudioSource を事前に設定しておく
    [SerializeField] private AudioSource laughAudioSource;

    // カード表示の閾値（20,40,60,80,100）

    //小さい呪い、大きい呪いどちらを表示しているかの判定
    public bool isCardCanvas1 = false;
    public bool isCardCanvas2 = false;
    public bool isCurseDice1 = false;
    public bool isCurseDice2 = false;

    //大きい呪いを実行するかのフラグ
    public bool isCurseActivation = false;

    public int curse1Number = 0;
    public bool curse1_1 = false;
    public bool curse1_2 = false;
    public bool curse1_3 = false;
    public bool curse2_1 = false;
    public bool curse2_2 = false;
    public bool curse3_3 = false;
    public int curse1_1Turn = 0;
    public int curse1_2Turn = 0;
    public int curse1_3Turn = 0;
    public bool endTurn = false;

    public GameObject Card12;
    public GameObject Card34;
    public GameObject Card56;
    public GameObject Curse1Canvas;
    public GameObject Curse2Canvas;
    public GameObject Curse3Canvas;
    public GameObject Curse1Card;
    public GameObject Curse2Card;
    public GameObject Curse3Card;

    public GameObject Canvas12;
    public GameObject Canvas34;
    public GameObject Canvas56;

    public GameObject DescriptionCanvas;
    public GameObject HanteiCanvas;
    public TextMeshProUGUI HanteiText;

    //public TextMeshProUGUI curseText; // 呪い発動テキスト
    public Button armButton;
    public Button legButton;
    public Button headButton;

    public GameObject curseAuraEffect1;
    public GameObject curseAuraEffect2;
    public GameObject curseAuraEffect3;
    public GameObject curseAuraEffect4;

    [SerializeField] private AudioSource curseAuraAudioSource1;
    [SerializeField] private AudioSource curseAuraAudioSource2;
    [SerializeField] private AudioSource curseAuraAudioSource3;
    [SerializeField] private AudioSource curseAuraAudioSource4;
    private GameObject activeEffect = null; // 現在流れているエフェクトを記録



    [SerializeField] private Vector3 effectOffset = new Vector3(0, -1.5f, -0.5f);
    private float previousDashPoint = 0; // 前回の呪いゲージ値を記録


    
    void Start()
    {
        DashGage.maxValue = maxDashPoint;
        DashGage.value = dashPoint;
        ResetGaugeImages();
        // PlayerSaikoroクラスのインスタンスを取得
        playerSaikoro = FindObjectOfType<PlayerSaikoro>();

        if (extraButton != null)
        {
            extraButton.onClick.RemoveAllListeners();
            extraButton.onClick.AddListener(() => { ExtraButtonAction(); HideCardCanvas1(); });
        }
        if (hideButton != null)
        {
            hideButton.onClick.RemoveAllListeners();
            hideButton.onClick.AddListener(() => { HideCardCanvasAndModifyDashIncrease(); HideCardCanvas1(); });
        }
        if (cursegiveButton != null)
        {
            cursegiveButton.onClick.RemoveAllListeners();
            cursegiveButton.onClick.AddListener(() => { CursegiveButtonAction(); HideCardCanvas1(); });
        }

        if (ArmButton != null)
        {
            ArmButton.onClick.RemoveAllListeners();
            //ArmButton.onClick.AddListener(() => { Arm_ButtonAction(); HideCardCanvas2(); });
            ArmButton.onClick.AddListener(() => { flashlightManager.DeactivateFlashlight(); }); // 懐中電灯を非アクティブにする
        }

        if (LegButton != null)
        {
            LegButton.onClick.RemoveAllListeners();
            LegButton.onClick.AddListener(Leg_ButtonAction);
        }
        if (EyeButton != null)
        {
            EyeButton.onClick.RemoveAllListeners();
            //EyeButton.onClick.AddListener(() => { Eye_ButtonAction(); HideCardCanvas2(); });
            EyeButton.onClick.AddListener(() => { eyeEffectController.ApplyEyeEffect(); }); // 追加
        }

        HideCardCanvas1();
        if (curseAuraEffect1 != null) curseAuraEffect1.SetActive(false);
        if (curseAuraEffect2 != null) curseAuraEffect2.SetActive(false);


    }


    private void ExtraButtonAction()
    {
        Debug.Log("aaa");
    }

    public void HideCardCanvasAndModifyDashIncrease()
    {
        curseincrease += master_Curse.CurseSheet[1].TurnIncrease;
        Debug.Log("[CurseSlider] Dash Increase Per Turn set to: " + curseincrease);
        Time.timeScale = 1;

    }

    public void Update()
    {
        //小さい呪い画面表示でASDキーで押せるようにする
        if (isCardCanvas1)
        {
            if (Input.GetKeyDown(KeyCode.A) && extraButton != null && extraButton.interactable)
            {
                extraButton.onClick.Invoke();
                isCardCanvas1 = false;

            }

            if (Input.GetKeyDown(KeyCode.S) && hideButton != null)
            {
                hideButton.onClick.Invoke();
                isCardCanvas1 = false;
            }

            if (Input.GetKeyDown(KeyCode.D) && cursegiveButton != null)
            {
                cursegiveButton.onClick.Invoke();
                isCardCanvas1 = false;
            }

            //if (curseAuraEffect1.activeSelf)
            //{
            //    Vector3 currentPosition = curseAuraEffect1.transform.position;
            //    currentPosition.y -= 1.0f; // y座標を強制的に低く設定
            //    curseAuraEffect1.transform.position = currentPosition;

            //    Debug.Log($"curseAuraEffect1 の位置: {curseAuraEffect1.transform.position}");
            //}

            //if (curseAuraEffect2.activeSelf)
            //{
            //    Vector3 currentPosition = curseAuraEffect2.transform.position;
            //    currentPosition.y -= 1.0f; // y座標を強制的に低く設定
            //    curseAuraEffect2.transform.position = currentPosition;
            //}



        }

        //大きい呪い画面表示でASDキーで押せるようにする
        if (isCardCanvas2)
        {
            if (Input.GetKeyDown(KeyCode.A) && ArmButton != null)
            {
                ArmButton.onClick.Invoke();
                isCardCanvas2 = false;
            }

            if (Input.GetKeyDown(KeyCode.S) && LegButton != null)
            {
                LegButton.onClick.Invoke();
                isCardCanvas2 = false;
            }

            if (Input.GetKeyDown(KeyCode.D) && EyeButton != null)
            {
                EyeButton.onClick.Invoke();
                isCardCanvas2 = false;
            }
        }
        if (80 <= dashPoint && dashPoint >= 100 && CardSelect1 == false)
        {
            CardSelect1 = true;
            StartCoroutine(ShowCardCanvas2());
        }
        if (80 <= dashPoint && dashPoint < 100 && CardSelect2 == false)
        {
            CardSelect2 = true;
            StartCoroutine(ShowCardCanvas1());
        }
        else if (60 <= dashPoint && dashPoint < 80 && CardSelect3 == false)
        {
            CardSelect3 = true;
            StartCoroutine(ShowCardCanvas1());
        }
        else if (40 <= dashPoint && dashPoint < 60 && CardSelect4 == false)
        {
            CardSelect4 = true;
            StartCoroutine(ShowCardCanvas1());
        }
        else if (20 <= dashPoint && dashPoint < 40 && CardSelect5 == false)
        {
            CardSelect5 = true;
            StartCoroutine(ShowCardCanvas1());
        }

        DashGage.value = dashPoint;

        //呪いゲージがMAXになったら
        if (dashPoint >= maxDashPoint)
        {
            diceCamera.enabled = true;
            diceController.ResetDiceState();
            isCardCanvas2 = true;
            isCurseDice2 = true;
            dashPoint = 0;
            CardSelect1 = false;
            CardSelect2 = false;
            CardSelect3 = false;
            CardSelect4 = false;
            CardSelect5 = false;

            ResetGaugeImages();
        }

        UpdateImageGauges();

        if (endTurn && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(cardCanvas1End());
        }
        //if (curseAuraEffect1.activeSelf)
        //{
        //    curseAuraEffect1.transform.position = effectOffset;
        //}

    }

    IEnumerator cardCanvas1End()
    {
        yield return null;

        endTurn = false;
        isCardCanvas1 = false;

        Canvas12.SetActive(false);
        Canvas34.SetActive(false);
        Canvas56.SetActive(false);
        Curse1Canvas.SetActive(false);
        Curse2Canvas.SetActive(false);
        Curse3Canvas.SetActive(false);
    }

    void FixedUpdate()
    {
        if (curseAuraEffect1.activeSelf)
        {
            curseAuraEffect1.transform.position = playerTransform.position + effectOffset;
        }
        if (curseAuraEffect2.activeSelf)
        {
            curseAuraEffect2.transform.position = playerTransform.position + effectOffset;
        }
    }
    public void Curse1(int r)
    {
        DescriptionCanvas.SetActive(false);

        int ct = UnityEngine.Random.Range(3, 6);
        if (curse1Number == 1)
        {
            curse1_1Turn = ct;
            Debug.Log("1呪ターン：" + curse1_1Turn);
        }
        else if (curse1Number == 2)
        {
            curse1_2Turn = ct;
            Debug.Log("2呪ターン：" + curse1_2Turn);
        }
        else if (curse1Number == 3)
        {
            curse1_3Turn = ct;
            Debug.Log("3呪ターン：" + curse1_3Turn);
        }

        if (r == 1 || r == 2)
        {
            //Debug.Log("呪１：敵の最低移動数が増加");
            //curse1_1 = true;
            Canvas12.GetComponent<TurnCard>().StartTurn(curse1Number, 12);
        }

        if (r == 3 || r == 4)
        {
            //Debug.Log("呪２：プレイヤーの歩数が減少");
            //curse1_2 = true;
            Canvas34.GetComponent<TurnCard>().StartTurn(curse1Number, 34);
        }

        if (r == 5 || r == 6)
        {
            //Debug.Log("呪３：回復、無敵アイテムの取得不可");
            //curse1_3 = true;
            Canvas56.GetComponent<TurnCard>().StartTurn(curse1Number, 56);
        }

        isCurseDice1 = false;
    }

    public void Curse2()
    {
        CountGauge++;

        //呪いゲージが100ごとに増加するごとに呪いを実行
        switch (CountGauge)
        {
            //懐中電灯の電気が消える
            case 1:
                Arm_ButtonAction();

                // 笑い声を再生
                if (laughAudioSource != null)
                {
                    laughAudioSource.Play();
                }

                break;

            //出目が1～3しか出ない、目線が下に下がる
            case 2:
                Leg_ButtonAction();
                //足にもやがかかり、笑い声が聞こえながら切れる
                //足はその場に残すようにする
                break;
            //首ちょんぱ、体ちょんぱ、トニートニーチョッパー
            case 3:
                SceneManager.LoadScene("Jump Scare");
                //首にもやがかかり、笑い声が聞こえながら切れるようにする
                break;
        }

        UpdateCountText();
    }

    public void IncreaseDashPointPerTurn()
    {
        dashPoint = Mathf.Min(dashPoint + dashIncreasePerTurn, maxDashPoint);
        DashGage.value = dashPoint;

        Debug.Log("今の呪いゲージ量: " + dashPoint);
    }

    private void UpdateImageGauges()
    {
        for (int i = 0; i < ImageGages.Length; i++)
        {
            float min = i * 20;
            float max = (i + 1) * 20;
            float fill = Mathf.Clamp01((dashPoint - min) / (max - min));
            ImageGages[i].fillAmount = fill;
        }
    }

    private void ResetGaugeImages()
    {
        foreach (Image img in ImageGages)
        {
            img.fillAmount = 0;
        }
    }

    public IEnumerator ShowCardCanvas1()
    {
        Debug.Log("ShowCardCanvas1() が呼ばれました");
        if (CardCanvas1 != null)
        {
            diceCamera.enabled = true;
            diceController.ResetDiceState();
            isCardCanvas1 = true;
            isCurseDice1 = true;

            // 呪いカードを表示する処理
            curse1Number = UnityEngine.Random.Range(1, 4);
            //Curse1Canvas.SetActive(true); // 呪いカード表示

            DescriptionCanvas.SetActive(true);
            Canvas12.SetActive(true);
            Canvas34.SetActive(true);
            Canvas56.SetActive(true);
            Canvas12.GetComponent<TurnCard>().CardReset();
            Canvas34.GetComponent<TurnCard>().CardReset();
            Canvas56.GetComponent<TurnCard>().CardReset();

            yield return null;
        }
        else
        {
            Debug.LogError("CardCanvas1 が null です");
        }
    }

    public IEnumerator ShowCardCanvas2()
    {
        Debug.Log("ShowCardCanvas2() が呼ばれました");
        if (CardCanvas2 != null)
        {
            isCardCanvas2 = true;
            DescriptionCanvas.SetActive(true);
            HanteiCanvas.SetActive(true);
            HanteiText.text = "サイコロ " + DiceController.dice2miss + " 以上で回避成功";
            Debug.Log("CardCanvas2 をアクティブにしました");

            ArmButton.interactable = !isArmButtonUsed;
            LegButton.interactable = !isLegButtonUsed;
            EyeButton.interactable = !isEyeButtonUsed;

            // インスペクターで指定したエフェクトを4秒間流す
            StartCoroutine(PlayEffectForDuration(curseAuraEffect3, 7.0f));

            yield return new WaitForSeconds(1.0f);
        }
        else
        {
            Debug.LogError("CardCanvas2 が null です");
        }
    }



    //public void HideCardCanvas2()
    //{
    //    if (CardCanvas2 != null)
    //    {
    //        CardCanvas2.SetActive(false);
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;
    //        Time.timeScale = 1;
    //        StartCoroutine(WaitThenShowCutIn());
    //    }
    //}

    public void HideCardCanvas1()
    {
        if (CardCanvas1 != null)
        {
            CardCanvas1.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            StartCoroutine(WaitThenShowCutIn());

        }
    }

    private IEnumerator WaitThenShowCutIn()
    {
        // フレーム待機または少しの遅延を入れて、全てがリセットされるのを待つ
        yield return null;
        StartCoroutine(cutIn.ShowCutIn());
    }

    private bool isArmButtonClicked = false;

    public void Arm_ButtonAction()
    {
        if (isArmButtonClicked) return;
        isArmButtonClicked = true;

        Debug.Log("Arm_Buttonが呼び出されました。");
        //Debug.Log("腕がなくなった");
        ArmButton.interactable = false;
        ArmButton.gameObject.SetActive(false); // ボタンを非表示にする
        isArmButtonUsed = true;

        // 懐中電灯を非アクティブにする
        flashlightManager.DeactivateFlashlight();

        // Canvasをアクティブにしてテキストを一文字ずつ表示
        if (eyeButtonText != null && eyeButtonCanvas != null && !isDisplayingText)
        {
            StartCoroutine(ActivateCanvasForDuration(eyeButtonCanvas, 5f));
            StartCoroutine(DisplayTextOneByOne("片手ヲ失っタ。\n懐中電灯が使えナイ。", eyeButtonText, 0.1f));
        }
    }

    private bool isLegButtonClicked = false;

    public void Leg_ButtonAction()
    {
        Debug.Log("足がなくなった");
        Debug.Log("Leg_Buttonが呼び出されました");
        LegButton.interactable = false;
        LegButton.gameObject.SetActive(false); // ボタンを非表示にする
        isLegButtonUsed = true;

        // サイコロの出目を1から3に設定
        diceRangeManager.SetDiceRollRange(1, 3);
        diceRangeManager.EnableTransformRoll(); // 出目の変換を有効にする
        diceController.SetLegButtonEffect(true); // DiceControllerの効果を有効にする
        playerSaikoro.SetLegButtonEffect(true); // PlayerSaikoroの効果を有効にする
        //HideCardCanvas2();

        // カメラの位置を低くする
        MainCamera.GetComponent<CameraController>().OnLegButtonPressed();

        // Canvasをアクティブにしてテキストを一文字ずつ表示
        if (eyeButtonText != null && eyeButtonCanvas != null && !isDisplayingText)
        {
            StartCoroutine(ActivateCanvasForDuration(eyeButtonCanvas, 5f));
            StartCoroutine(DisplayTextOneByOne("片足ヲ失っタ。\nサイコロが1,2,3しか出ナイ。", eyeButtonText, 0.1f));
        }
    }

    public void Head_ButtonAction()
    {
        Debug.Log("死ぬ メイドインワリオ");
    }

    public void Eye_ButtonAction()
    {
        Debug.Log("目が落ちた");
        EyeButton.interactable = false;
        EyeButton.gameObject.SetActive(false); // ボタンを非表示にする
        isEyeButtonUsed = true;

        // Canvasをアクティブにしてテキストを一文字ずつ表示
        if (eyeButtonText != null && eyeButtonCanvas != null && !isDisplayingText)
        {
            StartCoroutine(ActivateCanvasForDuration(eyeButtonCanvas, 5f));
            StartCoroutine(DisplayTextOneByOne("片目ヲ失っタ。", eyeButtonText, 0.1f));
        }
    }

    private IEnumerator DisplayTextOneByOne(string message, TextMeshProUGUI textComponent, float delay)
    {
        isDisplayingText = true;
        textComponent.text = "";
        foreach (char letter in message)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(delay);
        }
        isDisplayingText = false;
    }

    private IEnumerator ActivateCanvasForDuration(GameObject canvas, float duration)
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(duration);
        canvas.SetActive(false);
    }

    public void CursegiveButtonAction()
    {
        dashPoint = Mathf.Min(dashPoint + 5, maxDashPoint);
        DashGage.value = dashPoint;
        Debug.Log("[CursegiveButton] After: DashPoint = " + dashPoint);
        Time.timeScale = 1;
    }


    private void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = (CountGauge >= 2) ? "呪" : (3 - CountGauge).ToString();
        }
    }

    public void IncreaseDashPoint(int amount)
    {
        dashPoint = Mathf.Max(dashPoint - amount, 0);
        DashGage.value = dashPoint;
        Debug.Log("[CurseSlider] 呪いゲージ減少: " + amount + " 現在の値: " + dashPoint);

    }
    // 呪い増加
    public void DecreaseDashPoint(int amount)
    {
        dashPoint = Mathf.Min(dashPoint + amount + curseincrease, maxDashPoint);
        DashGage.value = dashPoint;
        Debug.Log("[CurseSlider] 呪いゲージ増加: " + amount + " 現在の値: " + dashPoint);

        UpdateCurseEffect();
    }
    private void DivideDiceRoll()
    {
        if (playerSaikoro != null)
        {
            // サイコロの出目を2で割って切り捨て
            playerSaikoro.sai = Mathf.FloorToInt(playerSaikoro.sai / 2.0f);
            Debug.Log("サイコロの出目を2で割って切り捨てた結果: " + playerSaikoro.sai);
        }
        else
        {
            Debug.LogError("PlayerSaikoroのインスタンスが見つかりませんでした。");
        }
    }

    //呪いカード、大きい呪いが表示しているかの判定を返す
    public bool IsCardCanvasActive()
    {
        return CardCanvas1.activeSelf || CardCanvas2.activeSelf;
    }

    private void PlayCurseSoundForDuration(AudioSource source, float duration)
    {
        if (source != null && !source.isPlaying)
        {
            source.Play();
            StartCoroutine(StopSoundAfterDuration(source, duration));
        }
    }


    private IEnumerator StopSoundAfterDuration(AudioSource source, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (source != null && source.isPlaying)
        {
            source.Stop();
            source.loop = false;
            Debug.Log(source.clip.name + " の再生を停止しました");
        }
    }
    private void UpdateCurseEffect()
    {
        Debug.Log($"現在の dashPoint 値: {dashPoint}");
        // **呪いゲージが増加した場合のみ発動**
        if (dashPoint > previousDashPoint)
        {

            if (dashPoint >= 10 && dashPoint <= 19)
            {
                curseAuraEffect1.SetActive(true);
                curseAuraEffect2.SetActive(false);
                curseAuraEffect3.SetActive(false);
                curseAuraEffect4.SetActive(false);

                StartCoroutine(PlayEffectForDuration(curseAuraEffect1, 5.0f));
                StartCoroutine(PlaySoundForDuration(curseAuraAudioSource1, 3.0f));
            }
            else if (dashPoint >= 20 && dashPoint <= 99)
            {
                curseAuraEffect1.SetActive(false);
                curseAuraEffect2.SetActive(true);
                curseAuraEffect3.SetActive(false);
                curseAuraEffect4.SetActive(false);

                StartCoroutine(PlayEffectForDuration(curseAuraEffect2, 5.0f));
                StartCoroutine(PlaySoundForDuration(curseAuraAudioSource2, 3.0f));
            }
            else if (dashPoint >= 100 && dashPoint <= 199)
            {
                curseAuraEffect1.SetActive(false);
                curseAuraEffect2.SetActive(false);
                curseAuraEffect4.SetActive(false);
                curseAuraEffect3.SetActive(true);

                StartCoroutine(PlayEffectForDuration(curseAuraEffect3, 7.0f));
                StartCoroutine(PlaySoundForDuration(curseAuraAudioSource3, 3.0f));
            }
            else if (dashPoint >= 200)
            {
                curseAuraEffect1.SetActive(false);
                curseAuraEffect2.SetActive(false);
                curseAuraEffect3.SetActive(false);
                curseAuraEffect4.SetActive(true);

                StartCoroutine(PlayEffectForDuration(curseAuraEffect4, 7.0f));
                StartCoroutine(PlaySoundForDuration(curseAuraAudioSource4, 3.0f));
            }


        }
        previousDashPoint = dashPoint;

    }


    private HashSet<AudioSource> playedSounds = new HashSet<AudioSource>();

    void LateUpdate()
    {
        if (curseAuraEffect1.activeSelf)
        {
            curseAuraEffect1.transform.position = playerTransform.position + new Vector3(0, -1.0f, 0f);
        }
        if (curseAuraEffect2.activeSelf)
        {
            curseAuraEffect2.transform.position = playerTransform.position + new Vector3(0, -1.0f, 0f);
        }
    }
    void ActivateEffect()
    {
        if (curseAuraEffect1 != null)
        {
            curseAuraEffect1.SetActive(true);
            curseAuraEffect1.GetComponent<ParticleSystem>().Play();
            Debug.Log("curseAuraEffect1 が再生されました");
        }
        else
        {
            Debug.LogError("curseAuraEffect1 がアタッチされていません！");
        }
    }
    private IEnumerator PlayEffectForDuration(GameObject effect, float duration)
    {
        if (effect != null)
        {
            effect.SetActive(true);
            Debug.Log(effect.name + " が開始しました");

            yield return new WaitForSeconds(duration);

            effect.SetActive(false);
            Debug.Log(effect.name + " を停止しました");
        }
    }



    // 3秒後に音声を停止
    private IEnumerator PlaySoundForDuration(AudioSource source, float duration)
    {
        if (source != null && !source.isPlaying)
        {
            source.Play();
            yield return new WaitForSeconds(duration);
            source.Stop();
        }
    }

}
