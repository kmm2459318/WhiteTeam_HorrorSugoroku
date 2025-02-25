using UnityEngine;
using SmoothigTransform;

public class DiceController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    private bool isStopped = false;
    private bool hasBeenThrown = false;
    private Vector3 initialMousePosition;
    private float timeSinceThrown = 0f;
    public int result = 0;
    public PlayerSaikoro player;
    public CameraChange cameraChange;

    [SerializeField] private Transform[] faces; // サイコロの面の空オブジェクト
    [SerializeField] private float stopCheckDelay = 1f; // 停止判定の猶予時間
    [SerializeField] private float stopThreshold = 0.05f; // 停止判定の速度閾値
    [SerializeField] private float throwForceMultiplier = 2f; // 投げる力の調整
    [SerializeField] SmoothTransform smo;

    private int minDiceValue = 1;
    private int maxDiceValue = 6;
    private bool legButtonEffect = false; // LegButtonの効果を管理するフラグ

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (cameraChange.DiceC)
        {
            // 左クリックでサイコロをつかむ
            if (Input.GetKey(KeyCode.Space) && !hasBeenThrown)
            {
                smo.enabled = true;
                smo.PosFact = 0.1f;
                isHeld = true;
                isStopped = false;
                hasBeenThrown = false;
                rb.isKinematic = true; // 物理挙動を停止
            }

            // 左クリックを離してサイコロを投げる
            if (Input.GetKeyUp(KeyCode.Space) && isHeld)
            {
                isHeld = false;
                hasBeenThrown = true;
                isStopped = false;
                timeSinceThrown = 0f;

                rb.isKinematic = false; // 物理挙動を再開
                smo.enabled = false;

                Vector3 throwForce = new Vector3(Random.Range(0, 40) / 10, 100, Random.Range(0, 40) / 10) * throwForceMultiplier;
                rb.AddForce(throwForce);
                rb.AddTorque(Random.insideUnitSphere * 500f); // ランダムな回転力
            }

            // サイコロをつかんでいる間
            if (isHeld)
            {
                smo.TargetPosition = new Vector3(0f, 5f, -3.363407f);

                float rotationSpeed = 300f;
                transform.Rotate(Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime,
                                 Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime,
                                 Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime);
            }

            // 投げた後の停止判定
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
                            ResetDiceState(); // 判定後に状態をリセット
                            player.DiceAfter(result);
                        }
                    }
                }
            }
        }
    }

    // 一番Y座標が高い面を取得
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

        // LegButtonの効果が有効な場合、出目を1,2,3に制限
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

    // サイコロの状態をリセット
    private void ResetDiceState()
    {
        hasBeenThrown = false;
        isHeld = false;
        isStopped = false;
    }

    // サイコロの出目の範囲を設定
    public void SetDiceRollRange(int min, int max)
    {
        minDiceValue = min;
        maxDiceValue = max;
    }

    // LegButtonの効果を設定
    public void SetLegButtonEffect(bool isActive)
    {
        legButtonEffect = isActive;
    }
}