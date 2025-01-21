using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;  // プレイヤーの本体
    private float mouseSensitivity = 150f;  // マウス感度
    private float upperLookLimit = 90f;  // 上方向の回転制限
    private float lowerLookLimit = -90f;  // 下方向の回転制限

    private float xRotation = 0f;  // カメラの現在の上下回転
    private bool isMouseLocked = true;  // マウスロック状態

    void Start()
    {
        // カメラを開始時にマウスをロック
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLock();

        if (isMouseLocked)
        {
            HandleMouseLook(); // マウスでの視点移動
        }
    }

    /// <summary>
    /// Altキーでのマウスロック/解除の切り替え。
    /// </summary>
    private void HandleMouseLock()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isMouseLocked = false; // 視点操作を無効化
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isMouseLocked = true; // 視点操作を有効化
        }
    }

    /// <summary>
    /// 上下方向の視点操作（X軸回転のみ）
    /// </summary>
    private void HandleMouseLook()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lowerLookLimit, upperLookLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // 上下回転
    }
}
