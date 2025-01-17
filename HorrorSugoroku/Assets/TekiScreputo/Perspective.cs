using UnityEngine;

public class Stenn : MonoBehaviour
{
    public Transform cameraTransform;    // カメラのTransform
    public float rotationSpeed = 50f;    // 回転速度
    private float xRotation = 0f;        // 上下回転の角度

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
        // キー入力取得
        float verticalInput = Input.GetAxis("Vertical");   // 上下移動 (W/Sキーまたは↑/↓キー)
        float horizontalInput = Input.GetAxis("Horizontal"); // 左右移動 (A/Dキーまたは←/→キー)

        // 上下回転 (カメラのみ回転)
        xRotation -= verticalInput * rotationSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 上下の回転範囲を制限
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 左右回転 (プレイヤー全体を回転)
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
