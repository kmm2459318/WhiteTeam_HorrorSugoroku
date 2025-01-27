using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;  // プレイヤーの本体
    private float mouseSensitivity = 150f;  // マウス感度
    private float sensitivityMultiplier = 1.0f;  // 感度倍率
    private float upperLookLimit =90f;  // 上方向の回転制限
    private float lowerLookLimit = -90f;  // 下方向の回転制限

    private float xRotation = 0f;  // カメラの現在の上下回転
    private float yRotation = 0f;  // カメラの現在の左右回転
    private bool isMouseLocked = true;  // マウスロック状態

    private float targetYRotation = 0f;  // 目標Y軸回転角度（補間対象）
    private float smoothTime = 0.3f;  // 補間にかける時間
    private float yRotationVelocity = 0f;  // 補間のための速度

    float mouseX;

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

        // idoutyuがtrueのとき、カメラのX, Y軸を補間で0°にリセット
        if (FindObjectOfType<PlayerSaikoro>().idoutyu)
        {
            // 補間でスムーズにリセット
            xRotation = Mathf.Lerp(xRotation, 20f, Time.deltaTime / smoothTime);  // X軸回転補間
            targetYRotation = Mathf.Lerp(targetYRotation, 0f, Time.deltaTime / smoothTime);  // Y軸回転補間

            // 補間を滑らかに適用
            mouseX = Mathf.Lerp(mouseX, 0f, Time.deltaTime / smoothTime);
            yRotation = Mathf.Lerp(yRotation, targetYRotation, Time.deltaTime / smoothTime);
        }
        else
        {
            // 通常時の回転処理
            if (isMouseLocked)
            {
                // マウス入力に基づいてY軸の回転を更新
                mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * sensitivityMultiplier * Time.deltaTime;

                // Y軸回転に適用
                yRotation += mouseX;

                // Y軸回転をスムーズに反映
                transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            }
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
        // Altキーを押している間は視点操作を完全にロック
        if (!isMouseLocked)
        {
            return; // 視点操作を無効化
        }

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 上下回転は制限
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lowerLookLimit, upperLookLimit);


        // 上下回転の反映
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);  // 上下回転
    }
}