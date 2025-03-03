using UnityEngine;

public class BloclByDoor : MonoBehaviour
{
    public float detectionDistance = 2f; // 検知する距離
    public LayerMask doorLayer; // Doorオブジェクトのレイヤー

    private PlayerSaikoro movementScript;
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
        movementScript = GetComponent<PlayerSaikoro>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの前方にRayを飛ばす
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance, doorLayer))
        {
            if (hit.collider.CompareTag("Door"))
            {
                // Doorが前方にある場合、移動を止める
                movementScript.enabled = false;
                return;
            }
        }  movementScript.enabled = true;
    }
  
}
