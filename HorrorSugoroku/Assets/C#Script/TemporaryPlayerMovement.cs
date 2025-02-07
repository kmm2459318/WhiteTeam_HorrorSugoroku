using UnityEngine;

public class TemporaryPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 7f; // ジャンプの力
    public Transform groundCheck; // 地面の判定位置
    public LayerMask groundLayer; // 地面レイヤー
    public Transform cameraTransform; // カメラのTransform

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 地面にいるかどうかチェック
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // 移動処理
        MovePlayer();

        // ジャンプ処理
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/Dまたは左/右矢印キー
        float moveZ = Input.GetAxisRaw("Vertical"); // W/Sまたは上/下矢印キー

        // カメラの前方向を基準にXZ平面で移動
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Y軸の影響を排除（地面と平行に移動）
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // カメラの向きを考慮した移動ベクトル
        Vector3 move = (forward * moveZ + right * moveX).normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + move);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
