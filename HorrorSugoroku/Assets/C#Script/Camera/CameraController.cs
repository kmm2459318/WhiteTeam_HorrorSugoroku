using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;  // �v���C���[�̖{��
    public float mouseSensitivity = 150f;  // �}�E�X���x
    private float upperLookLimit = 90f;  // ������̉�]����
    private float lowerLookLimit = -90f;  // �������̉�]����

    private float xRotation = 0f;  // �J�����̌��݂̏㉺��]
    private bool isMouseLocked = true;  // �}�E�X���b�N���

    void Start()
    {
        // �J�������J�n���Ƀ}�E�X�����b�N
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLock();

        if (isMouseLocked)
        {
            HandleMouseLook(); // �}�E�X�ł̎��_�ړ�
        }
    }

    /// <summary>
    /// Alt�L�[�ł̃}�E�X���b�N/�����̐؂�ւ��B
    /// </summary>
    private void HandleMouseLock()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            isMouseLocked = !isMouseLocked;
            Cursor.lockState = isMouseLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isMouseLocked;
        }
    }

    /// <summary>
    /// �㉺�����̎��_����iX����]�̂݁j
    /// </summary>
    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lowerLookLimit, upperLookLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // �㉺��]
        playerBody.Rotate(Vector3.up * mouseX);  // ���E��]
    }
}