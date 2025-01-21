using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; // 速さを変更
    public float jumpForce = 10f; // ジャンプ力を変更
    public CharacterController controller;

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        controller.Move(movement * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // スペースキーでジャンプ
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}