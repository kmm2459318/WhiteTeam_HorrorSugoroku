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
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if (Physics.Raycast(ray, out RaycastHit hit))
                //{
                //if (hit.collider.gameObject == gameObject)
                //{
                smo.enabled = true;
                smo.PosFact = 0.1f;
                isHeld = true;
                isStopped = false;
                hasBeenThrown = false;
                //initialMousePosition = Input.mousePosition;
                rb.isKinematic = true; // 物理挙動を停止
                                       //}
                                       //}
                                       //}
                                       //}
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

                // マウス移動量から力を計算
                //Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
                Vector3 throwForce = new Vector3(Random.Range(0, 40) / 10, 100, Random.Range(0, 40) / 10) * throwForceMultiplier;
                rb.AddForce(throwForce);
                rb.AddTorque(Random.insideUnitSphere * 500f); // ランダムな回転力
            }

            // サイコロをつかんでいる間
            if (isHeld)
            {
                // マウス位置に追従
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if (Physics.Raycast(ray, out RaycastHit hit))
                //{
                //Vector3 targetPosition = transform.position;
                //targetPosition.y = 5f; // Y座標の上限を5に制限
                //transform.position = targetPosition;
                smo.TargetPosition = new Vector3(0f, 5f, -3.363407f);
                //smo.TargetPosition.y = 5.0f;
                //smo.TargetPosition.z = -f;
                //}

                // ランダム回転
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
                    // サイコロが停止しているか確認
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

        return topFace != null ? int.Parse(topFace.name) : -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突時はまだ停止していないとみなす
        isStopped = false;
    }

    // サイコロの状態をリセット
    private void ResetDiceState()
    {
        hasBeenThrown = false;
        isHeld = false;
        isStopped = false;
    }
}
