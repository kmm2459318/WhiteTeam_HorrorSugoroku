using UnityEngine;

public class BlockByDoor : MonoBehaviour
{
    public float detectionDistance = 2f; // 検知する距離
    public LayerMask doorLayer; // Doorオブジェクトのレイヤー

    public PlayerSaikoro movementScript;
    public Transform cameraTransform; // カメラのTransform

    void Update()
    {
        // カメラの向いてる方向にRayを飛ばす
        Vector3 direction = cameraTransform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance, doorLayer))
        {
            if (hit.collider.CompareTag("Door"))
            {
                // Doorが前方にある場合、移動を止める
                movementScript.enabled = false;
                Debug.DrawRay(transform.position, direction * detectionDistance, Color.red);
                return;
            }
        }

        // Doorがなければ移動できる
        movementScript.enabled = true;
        Debug.DrawRay(transform.position, direction * detectionDistance, Color.green);
    }
}
