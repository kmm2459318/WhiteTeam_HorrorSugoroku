using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // �ړ����x
    public float mouseSensitivity = 2f; // �}�E�X���x
    public float jumpHeight = 2f; // �W�����v�̍���
    public float gravity = -9.81f; // �d��

    private UnityEngine.CharacterController characterController; // CharacterController�R���|�[�l���g
    private Vector3 velocity; // �v���C���[�̐������x�i�d�͗p�j
    private bool isGrounded; // �n�ʂɐڂ��Ă��邩�ǂ���

    public Transform cameraTransform; // �J�����i���_�j��Transform
    private float xRotation = 0f; // �J�����̏㉺���_��]�p�x

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CharacterController�R���|�[�l���g���擾
        characterController = GetComponent<UnityEngine.CharacterController>();

        // �}�E�X�J�[�\�����\���ɂ��ă��b�N
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
        // �n�ʂɐڂ��Ă��邩�m�F
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // �n�ʂɌŒ�i�킸���ɉ������j
        }

        // �ړ����́iWASD�j
        float moveX = Input.GetAxis("Horizontal"); // ���E�ړ�
        float moveZ = Input.GetAxis("Vertical");   // �O��ړ�

        // �v���C���[�̈ړ��������v�Z
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // CharacterController�ňړ�
        characterController.Move(move * speed * Time.deltaTime);

        // �W�����v����
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // �W�����v�̏����x�v�Z
        }

        // �d�͂�K�p
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void RotateCamera()
    {
        // �}�E�X���͂��擾
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // �J�����̏㉺��]�iX����]�j
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // �㉺��]�̐���

        // �J������Transform�ɓK�p
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �v���C���[�̍��E��]�iY����]�j
        transform.Rotate(Vector3.up * mouseX);
    }
}