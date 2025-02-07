using UnityEngine;

public class TemporaryPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 7f; // ジャンプの力
    public Transform groundCheck; // 地面の判定位置
    public LayerMask groundLayer; // 地面レイヤー

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

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
