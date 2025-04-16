using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float shiftMultiplier = 2f;

    private bool cursorMode = false;
    private Vector2 rotation = Vector2.zero;

    void Start()
    {
        LockCursor();
        rotation = new Vector2(transform.eulerAngles.y, -transform.eulerAngles.x);
    }

    void Update()
    {
        HandleCursorMode();

        if (!cursorMode)
        {
            HandleMouseLook();
        }

        HandleMovement();
    }

    void HandleCursorMode()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (!cursorMode)
            {
                cursorMode = true;
                UnlockCursor();
                // カーソルを中央に戻す
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
        else
        {
            if (cursorMode)
            {
                cursorMode = false;
                LockCursor();
            }
        }
    }

    void HandleMouseLook()
    {
        rotation.x += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.y += Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);
        transform.eulerAngles = new Vector3(-rotation.y, rotation.x, 0);
    }

    void HandleMovement()
    {
        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? shiftMultiplier : 1f);
        Vector3 direction = Vector3.zero;

        // 前後左右（WASDまたは矢印キー）
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) direction += transform.forward;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) direction -= transform.forward;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) direction -= transform.right;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) direction += transform.right;

        // 上下（Space/E 上昇、Shift/Q 下降）
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E)) direction += Vector3.up;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Q)) direction -= Vector3.up;

        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
