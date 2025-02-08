using UnityEngine;

public class TemporaryPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �ړ����x
    public float jumpForce = 7f; // �W�����v�̗�
    public Transform groundCheck; // �n�ʂ̔���ʒu
    public LayerMask groundLayer; // �n�ʃ��C���[
    public Transform cameraTransform; // �J������Transform

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �n�ʂɂ��邩�ǂ����`�F�b�N
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // �ړ�����
        MovePlayer();

        // �W�����v����
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D�܂��͍�/�E���L�[
        float moveZ = Input.GetAxisRaw("Vertical"); // W/S�܂��͏�/�����L�[

        // �J�����̑O���������XZ���ʂňړ�
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Y���̉e����r���i�n�ʂƕ��s�Ɉړ��j
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // �J�����̌������l�������ړ��x�N�g��
        Vector3 move = (forward * moveZ + right * moveX).normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + move);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
