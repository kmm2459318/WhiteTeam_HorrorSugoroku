using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 250f;
    public Slider sensitivitySlider;
    private float sensitivityMultiplier = 1.0f;
    private float upperLookLimit = 90f;
    private float lowerLookLimit = -90f;

    private float xRotation = 0f;
    private float yRotation = 0f;
    public bool isMouseLocked = true;
    private bool isOptionOpen = false; // オプションメニューが開いているかどうか

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
            HandleMouseLook();
        }
    }

    /// <summary>
    /// Altキーでマウスロックを切り替える（オプションメニューが開いていないときのみ）
    /// </summary>
    private void HandleMouseLock()
    {
        // オプションメニューが開いている場合は Altキーの処理を完全に無効化
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

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
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
