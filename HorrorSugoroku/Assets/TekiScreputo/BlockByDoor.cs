using UnityEngine;

public class BlockByDoor : MonoBehaviour
{
    public float detectionDistance = 2f; // 検知する距離
    public LayerMask obstacleLayer; // DoorやWallのレイヤー

    public PlayerSaikoro movementScript;
    public Transform cameraTransform; // カメラのTransform

    void Update()
    {
        // カメラの向いてる方向にRayを飛ばす
        Vector3 direction = cameraTransform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance, obstacleLayer))
        {
            if (hit.collider.CompareTag("Door") || hit.collider.CompareTag("Wall"))
            {
                // DoorやWallが前方にある場合、移動を止める
                movementScript.enabled = false;
                Debug.DrawRay(transform.position, direction * detectionDistance, Color.red);
                return;
            }
        }

        // 障害物がなければ移動できる
        movementScript.enabled = true;
        Debug.DrawRay(transform.position, direction * detectionDistance, Color.green);
    }
}
