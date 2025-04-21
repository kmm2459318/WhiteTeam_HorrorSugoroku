using UnityEngine;

public class ColliderTriggerController : MonoBehaviour
{
    [SerializeField] private GameObject playerObject; // プレイヤーオブジェクト
    [SerializeField] private GameObject triggerObject; // トリガーオブジェクト
    [SerializeField] private GlobalLightController globalLightController; // GlobalLightControllerの参照

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            Debug.Log($"プレイヤー '{playerObject.name}' が '{triggerObject.name}' のエリアに入りました！");
            if (globalLightController != null)
            {
                globalLightController.SetPlayerInZone(true); // プレイヤーがエリア内にいると通知
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            Debug.Log($"プレイヤー '{playerObject.name}' が '{triggerObject.name}' のエリアを出ました！");
            if (globalLightController != null)
            {
                globalLightController.SetPlayerInZone(false); // プレイヤーがエリア外と通知
            }
        }
    }
}