using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // 移動速度
    public float mouseSensitivity = 2f; // マウス感度
    public float jumpHeight = 2f; // ジャンプの高さ
    public float gravity = -9.81f; // 重力

    private UnityEngine.CharacterController characterController; // CharacterControllerコンポーネント
    private Vector3 velocity; // プレイヤーの垂直速度（重力用）
    private bool isGrounded; // 地面に接しているかどうか

    public Transform cameraTransform; // カメラ（視点）のTransform
    private float xRotation = 0f; // カメラの上下視点回転角度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CharacterControllerコンポーネントを取得
        characterController = GetComponent<UnityEngine.CharacterController>();

        // マウスカーソルを非表示にしてロック
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {
        // 地面に接しているか確認
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 地面に固定（わずかに下向き）
        }

        // 移動入力（WASD）
        float moveX = Input.GetAxis("Horizontal"); // 左右移動
        float moveZ = Input.GetAxis("Vertical");   // 前後移動

        // プレイヤーの移動方向を計算
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // CharacterControllerで移動
        characterController.Move(move * speed * Time.deltaTime);

        // ジャンプ処理
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // ジャンプの初速度計算
        }

        // 重力を適用
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void RotateCamera()
    {
        // マウス入力を取得
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // カメラの上下回転（X軸回転）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 上下回転の制限

        // カメラのTransformに適用
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // プレイヤーの左右回転（Y軸回転）
        transform.Rotate(Vector3.up * mouseX);
    }
}