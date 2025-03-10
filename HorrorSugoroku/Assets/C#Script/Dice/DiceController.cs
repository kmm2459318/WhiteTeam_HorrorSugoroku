using UnityEngine;
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
    [SerializeField] private DiceRotation diceRotation; // インスペクターで設定

    public DiceRangeManager diceRangeManager;
    private Transform parentTransform;
    private Vector3 initialPosition;

    private int minDiceValue = 1;
    private int maxDiceValue = 6;
    private bool legButtonEffect = false;

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

        // DiceRotation の取得（インスペクター設定がなければ自動で探す）
        if (diceRotation == null)
        {
            diceRotation = FindObjectOfType<DiceRotation>();
            if (diceRotation == null)
            {
                Debug.LogError("DiceRotation がシーン内に見つかりません！");
            }
        }

        // 初期位置設定
        parentTransform = transform.parent;
        initialPosition = transform.position;
        transform.position = new Vector3(parentTransform.position.x, initialPosition.y + 0.5f, parentTransform.position.z);
    }

    void Update()
    {
        if (player.saikorotyu)
        {
            // スペースキーを押し続けている間、サイコロを持つ
            if (Input.GetKey(KeyCode.Space) && !hasBeenThrown)
            {
                smo.enabled = true;
                smo.PosFact = 0.1f;
                isHeld = true;
                isStopped = false;
                hasBeenThrown = false;
                rb.isKinematic = true;
                transform.position = new Vector3(parentTransform.position.x, 5f, parentTransform.position.z);
            }

            // スペースキーを離したら投げる
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

            // サイコロが止まったかどうかをチェック
            if (hasBeenThrown)
            {
                timeSinceThrown += Time.deltaTime;

                if (timeSinceThrown >= stopCheckDelay && !isStopped)
                {
                    if (rb.linearVelocity.magnitude < stopThreshold && rb.angularVelocity.magnitude < stopThreshold)
                    {
                        isStopped = true;
                        result = GetTopFace();
                        ResetDiceState();

                        if (diceRotation != null) // Null チェックを追加
                        {
                            diceRotation.GetDiceNumber(result);
                        }
                        else
                        {
                            Debug.LogError("diceRotation が設定されていません！");
                        }

                        if (result != -1)
                        {
                            Debug.Log($"出た目: {result}");
                            player.DiceAfter(result);
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ResetDiceState();
        }
    }

    /// <summary>
    /// 最も上にある面の数値を取得する
    /// </summary>
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

    /// <summary>
    /// 衝突時にサイコロの停止状態を解除
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        isStopped = false;
    }

    /// <summary>
    /// サイコロの状態をリセットする
    /// </summary>
    private void ResetDiceState()
    {
        hasBeenThrown = false;
        isHeld = false;
        isStopped = false;
    }

    /// <summary>
    /// サイコロの出目範囲を設定
    /// </summary>
    public void SetDiceRollRange(int min, int max)
    {
        minDiceValue = min;
        maxDiceValue = max;
    }

    /// <summary>
    /// 足ボタンの効果を設定
    /// </summary>
    public void SetLegButtonEffect(bool isActive)
    {
        legButtonEffect = isActive;
    }
}