using UnityEngine;

public class DiceController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    private Vector3 initialMousePosition;
    private bool isStopped = false;
    private float timeSinceThrown = 0f; // 投げた後の時間を記録
    private bool hasBeenThrown = false; // サイコロが投げられたかどうか

    // さいころの面となる空のオブジェクト
    [SerializeField] private Transform[] faces;

    // 停止判定を行う猶予時間（秒）
    [SerializeField] private float stopCheckDelay = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 左クリックでつかむ処理
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isHeld = true;
                    isStopped = false; // 判定を無効化
                    hasBeenThrown = false; // 投げていない状態にリセット
                    initialMousePosition = Input.mousePosition;
                    rb.isKinematic = true; // 物理挙動を停止
                }
            }
        }

        // 左クリックを離したときに投げる処理
        if (Input.GetMouseButtonUp(0) && isHeld)
        {
            isHeld = false;
            hasBeenThrown = true; // 投げた状態にする
            timeSinceThrown = 0f; // 猶予時間のカウントを開始
            rb.isKinematic = false;

            // マウスの動きを力として加える
            Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
            rb.AddForce(new Vector3(mouseDelta.x, 200, mouseDelta.y) * 5f);
            rb.AddTorque(Random.insideUnitSphere * 1000f); // ランダムな回転力を付与
        }

        // つかんでいる間の動作
        if (isHeld)
        {
            // マウス位置に追従させる
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = Mathf.Min(targetPosition.y, 5); // Y座標を最大5に制限
                transform.position = targetPosition;
            }

            // 回転させる
            float rotationSpeed = 1000f;
            transform.Rotate(Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime,
                             Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime,
                             Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime);
        }

        // 投げた後の時間を計測
        if (hasBeenThrown)
        {
            timeSinceThrown += Time.deltaTime;
        }

        // 停止を判定して出た目を表示（つかんでいない & 猶予時間経過後に判定）
        if (!isHeld && hasBeenThrown && timeSinceThrown >= stopCheckDelay && !isStopped)
        {
            if (rb.linearVelocity.magnitude < 0.1f && rb.angularVelocity.magnitude < 0.1f)
            {
                isStopped = true; // 一度だけ判定する
                int result = GetTopFace();
                if (result != -1)
                {
                    Debug.Log($"出た目: {result}");
                }
            }
        }
    }

    // 一番Y座標が高い面を判定
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
        // 衝突時に停止判定をリセット（念のため）
        isStopped = false;
    }
}
