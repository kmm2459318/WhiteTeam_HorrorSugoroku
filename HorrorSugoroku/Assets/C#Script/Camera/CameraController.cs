using UnityEngine;
using UnityEngine.UI; // スライダーのために必要

public class CameraController : MonoBehaviour
{
    public Transform playerBody;  // プレイヤーの本体
    public float mouseSensitivity = 250f;  // マウス感度
    public Slider sensitivitySlider; // 感度倍率を設定するスライダー
    private float sensitivityMultiplier = 1.0f;  // 感度倍率
    private float upperLookLimit = 90f;  // 上方向の回転制限
    private float lowerLookLimit = -90f;  // 下方向の回転制限

    private float xRotation = 0f;  // カメラの現在の上下回転
    private float yRotation = 0f;  // カメラの現在の左右回転
    public bool isMouseLocked = true;  // マウスロック状態
    private bool isOptionOpen = false;  // オプションメニューの開閉状態

    void Start()
    {
        // カメラを開始時にマウスをロック
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // スライダーの初期値を設定し、リスナーを登録
        if (sensitivitySlider != null)
        {
            sensitivitySlider.minValue = 0.1f;
            sensitivitySlider.maxValue = 20.0f;
            sensitivitySlider.value = sensitivityMultiplier;
            sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        }
    }

    void Update()
    {
        HandleMouseLock();

        if (isMouseLocked)
        {
            HandleMouseLook(); // マウスでの視点移動
        }
    }

    private void HandleMouseLock()
    {
        // オプションメニューが開いている間は Alt キーの処理を無効化
        if (isOptionOpen)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isMouseLocked = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isMouseLocked = true;
        }
    }

    private void HandleMouseLook()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * sensitivityMultiplier * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * sensitivityMultiplier * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lowerLookLimit, upperLookLimit);
        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);  // 上下回転
    }

    private void OnSensitivityChanged(float value)
    {
        sensitivityMultiplier = value;
    }

    // オプション画面の開閉状態を更新する
    public void SetOptionOpen(bool state)
    {
        isOptionOpen = state;
    }
}
