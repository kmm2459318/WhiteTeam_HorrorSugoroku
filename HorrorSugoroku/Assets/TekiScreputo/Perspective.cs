using UnityEngine;

public class Stenn : MonoBehaviour
{
    public Transform cameraTransform;    // �J������Transform
    public float rotationSpeed = 50f;    // ��]���x
    private float xRotation = 0f;        // �㉺��]�̊p�x

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleViewRotation();
    }
    void HandleViewRotation()
    {
        // �L�[���͎擾
        float verticalInput = Input.GetAxis("Vertical");   // �㉺�ړ� (W/S�L�[�܂��́�/���L�[)
        float horizontalInput = Input.GetAxis("Horizontal"); // ���E�ړ� (A/D�L�[�܂��́�/���L�[)

        // �㉺��] (�J�����̂݉�])
        xRotation -= verticalInput * rotationSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // �㉺�̉�]�͈͂𐧌�
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ���E��] (�v���C���[�S�̂���])
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
