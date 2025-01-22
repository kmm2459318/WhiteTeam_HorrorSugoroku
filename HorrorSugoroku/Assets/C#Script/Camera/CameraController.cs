using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;  // �v���C���[�̖{��
    private float mouseSensitivity = 150f;  // �}�E�X���x
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isMouseLocked = false; // ���_����𖳌���
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isMouseLocked = true; // ���_�����L����
        }
    }

    /// <summary>
    /// �㉺�����̎��_����iX����]�̂݁j
    /// </summary>
    private void HandleMouseLook()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lowerLookLimit, upperLookLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // �㉺��]
    }
}
