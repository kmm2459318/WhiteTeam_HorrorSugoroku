using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;  // �v���C���[�̖{��
    private float mouseSensitivity = 150f;  // �}�E�X���x
    private float sensitivityMultiplier = 2.0f;  // ���x�{��
    private float upperLookLimit = 90f;  // ������̉�]����
    private float lowerLookLimit = -90f;  // �������̉�]����

    private float xRotation = 0f;  // �J�����̌��݂̏㉺��]
    private float yRotation = 0f;  // �J�����̌��݂̍��E��]
    private bool isMouseLocked = true;  // �}�E�X���b�N���

    private float targetYRotation = 0f;  // �ڕWY����]�p�x�i��ԑΏہj
    private float smoothTime = 0.3f;  // ��Ԃɂ����鎞��
    private float yRotationVelocity = 0f;  // ��Ԃ̂��߂̑��x

    float mouseX;

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

        // idoutyu��true�̂Ƃ��A�J������X, Y�����Ԃ�0���Ƀ��Z�b�g
        if (FindObjectOfType<PlayerSaikoro>().idoutyu)
        {
            // ��ԂŃX���[�Y�Ƀ��Z�b�g
            xRotation = Mathf.Lerp(xRotation, 0f, Time.deltaTime / smoothTime);  // X����]���
            targetYRotation = Mathf.Lerp(targetYRotation, 0f, Time.deltaTime / smoothTime);  // Y����]���

            // ��Ԃ����炩�ɓK�p
            mouseX = Mathf.Lerp(mouseX, 0f, Time.deltaTime / smoothTime);
            yRotation = Mathf.Lerp(yRotation, targetYRotation, Time.deltaTime / smoothTime);
        }
        else
        {
            // �ʏ펞�̉�]����
            if (isMouseLocked)
            {
                // �}�E�X���͂Ɋ�Â���Y���̉�]���X�V
                mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * sensitivityMultiplier * Time.deltaTime;

                // Y����]�ɓK�p
                yRotation += mouseX;

                // Y����]���X���[�Y�ɔ��f
                transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            }
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

        // �㉺��]�͐���
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lowerLookLimit, upperLookLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // �㉺��]
        playerBody.Rotate(Vector3.up * mouseX);  // ���E��]
    }
}