using UnityEngine;
using UnityEngine.UI;
using SmoothigTransform;

public class DiceController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    private bool isStopped = false;
    private bool hasBeenThrown = false;
    private float timeSinceThrown = 0f;
    public int result = 0;
    public PlayerSaikoro player;
    [SerializeField] private Transform[] faces;
    private float stopCheckDelay = 1f;
    private float stopThreshold = 0.05f;
    private float throwForceMultiplier = 0.8f;
    [SerializeField] private SmoothTransform smo;
    [SerializeField] private DiceRotation diceRotation;
    public CurseSlider curseGauge;
    public PlayerSaikoro playerSaikoro;

    public DiceRangeManager diceRangeManager;
    private Transform parentTransform;
    private Vector3 initialLocalPosition; // ✅ 親基準の初期位置

    private int minDiceValue = 1;
    private int maxDiceValue = 6;
    private bool legButtonEffect = false;

    private Vector3 targetLocalOffset = new Vector3(-5.47f, 0f, -2.54f)-;   //new Vector3(-5.47f, 0f, -2.54f)
    private bool moveToTarget = false;
    private bool moveToReset = false;
    private float moveSpeed = 30f; // 移動速度

    private Quaternion targetRotation; // 🎯 目標の回転
    private bool rotateToFace = false; // 🎯 回転フラグ
    private float rotationSpeed = 5f; // 🎯 回転速度

    // 出目ごとの回転 (上を向く面を基準)
    private Vector3[] faceRotations = new Vector3[]
    {
    new Vector3(-90, 0, 0),  // 1の面が上
    new Vector3(0, 0, 0),    // 2の面が上
    new Vector3(0, 0, -90),  // 3の面が上
    new Vector3(0, 0, 90),   // 4の面が上
    new Vector3(180, 180, 0),// 5の面が上
    new Vector3(90, 0, 0)    // 6の面が上
    };

    private int dice2miss = 3; 

    // 🎯 出目が決まったら回転と移動を開始
    void ApplyDiceResult(int result)
    {
        if (result >= 1 && result <= 6)
        {
            targetRotation = Quaternion.Euler(faceRotations[result - 1]); // 🎯 目標の回転を設定
            rotateToFace = true;
            moveToTarget = true;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // DiceRangeManager の設定
        diceRangeManager = FindObjectOfType<DiceRangeManager>();
        if (diceRangeManager != null)
        {
            diceRangeManager.SetDiceRollRange(1, 6);
            diceRangeManager.EnableTransformRoll();
        }
        else
        {
            Debug.LogError("DiceRangeManager がシーン内に見つかりません！");
        }

        // DiceRotation の取得
        if (diceRotation == null)
        {
            diceRotation = FindObjectOfType<DiceRotation>();
            if (diceRotation == null)
            {
                Debug.LogError("DiceRotation がシーン内に見つかりません！");
            }
        }

        // 親オブジェクトの取得
        parentTransform = transform.parent;
        if (parentTransform == null)
        {
            Debug.LogError("DiceController の親オブジェクトが設定されていません！");
        }

        // ✅ 初期ローカル座標を保存
        initialLocalPosition = transform.localPosition;

        // サイコロの初期位置を調整
        transform.localPosition = initialLocalPosition + new Vector3(0, 0.5f, 0);
    }

    void Update()
    {
        if (player.saikorotyu)
        {
            DiceRoll(0);
        }

        //呪い処理用さいころ
        if (curseGauge.isCurseDice1)
        {
            Debug.Log("diceroll1");
            DiceRoll(1);
        }
        //大きい呪い処理用さいころ
        if (curseGauge.isCurseDice2)
        {
            Debug.Log("diceroll2");
            DiceRoll(2);
        }

        if (rotateToFace) // 🎯 **回転処理を最優先に**
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                rotateToFace = false;
            }
        }

        if (moveToTarget)
        {
            Vector3 targetLocalPosition = initialLocalPosition + targetLocalOffset;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetLocalPosition, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.localPosition, targetLocalPosition) < 0.1f)
            {
                transform.localPosition = targetLocalPosition;
                moveToTarget = false;
            }
        }

        if (moveToReset)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialLocalPosition, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.localPosition, initialLocalPosition) < 0.1f)
            {
                transform.localPosition = initialLocalPosition;
                moveToReset = false;
            }
        }

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    ResetDiceState();
        //}
    }

    private void DiceRoll(int n) //nが１ならプレイヤーのさいころ、２なら呪いさいころ
    {
        if (Input.GetKey(KeyCode.Space) && !hasBeenThrown)
        {
            smo.enabled = true;
            smo.PosFact = 0.1f;
            isHeld = true;
            isStopped = false;
            hasBeenThrown = false;
            rb.isKinematic = true;
            transform.localPosition = new Vector3(0, 5f, 0);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isHeld)
        {
            isHeld = false;
            hasBeenThrown = true;
            isStopped = false;
            timeSinceThrown = 0f;

            rb.isKinematic = false;
            smo.enabled = false;

            Vector3 throwForce = new Vector3(Random.Range(-2f, 2f), 10f, Random.Range(-2f, 2f)) * throwForceMultiplier;
            rb.AddForce(throwForce, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 500f);
        }

        if (hasBeenThrown)
        {
            timeSinceThrown += Time.deltaTime;

            if (timeSinceThrown >= stopCheckDelay && !isStopped)
            {
                if (rb.linearVelocity.magnitude < stopThreshold && rb.angularVelocity.magnitude < stopThreshold)
                {
                    isStopped = true;
                    result = GetTopFace();

                    if (result != -1)
                    {
                        Debug.Log($"出た目: {result}");
                        if (n == 0)
                        {
                            player.DiceAfter(result);
                        }
                        else if (n == 1)
                        {
                            StartCoroutine(playerSaikoro.HideDiceCameraWithDelay());
                            curseGauge.Curse1(result);
                        }
                        else if (n == 2)
                        {
                            StartCoroutine(playerSaikoro.HideDiceCameraWithDelay());
                            if (result >= 1 && result <= dice2miss)
                            {
                                dice2miss = 3;
                                curseGauge.Curse2();
                            }
                            else
                            {
                                dice2miss++;
                                Debug.Log("大きい呪いダイス回避成功！失敗数が上昇→"　+ dice2miss);
                            }
                            curseGauge.isCardCanvas2 = false;
                            curseGauge.isCurseDice2 = false;
                        }
                        ApplyDiceResult(result); // 🎯 **ここで即回転開始**
                    }
                }
            }
        }
    }

    private int GetTopFace()
    {
        if (faces == null || faces.Length == 0) return -1;

        Transform topFace = null;
        float maxY = float.MinValue;

        foreach (Transform face in faces)
        {
            if (face.position.y > maxY)
            {
                maxY = face.position.y;
                topFace = face;
            }
        }

        int faceValue = topFace != null ? int.Parse(topFace.name) : -1;

        if (legButtonEffect && faceValue > 3)
        {
            faceValue = Random.Range(1, 4);
        }

        return faceValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isStopped = false;
    }

    public void ResetDiceState()
    {
        hasBeenThrown = false;
        isHeld = false;
        isStopped = false;
        rotateToFace = false;
        moveToReset = true; // 🎯 リセット時に親基準の初期位置に戻る
        Debug.Log("さいころリセットしたよん！");
    }

    public void SetDiceRollRange(int min, int max)
    {
        minDiceValue = min;
        maxDiceValue = max;
    }

    public void SetLegButtonEffect(bool isActive)
    {
        legButtonEffect = isActive;
    }
}
