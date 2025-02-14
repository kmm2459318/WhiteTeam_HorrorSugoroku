using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 250f;
    private float sensitivityMultiplier = 1.0f;
    private float upperLookLimit = 90f;
    private float lowerLookLimit = -90f;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private bool isMouseLocked = true;

    public Option optionMenu; // オプションメニューの参照

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        // オプションメニューが開いている場合は Altキーの処理を無効化
        if (optionMenu != null && optionMenu.IsOptionOpen())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isMouseLocked = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isMouseLocked = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
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

    // Option.cs からマウスロックの制御を受け取る
    public void SetMouseLock(bool state)
    {
        isMouseLocked = state;
    }
}
