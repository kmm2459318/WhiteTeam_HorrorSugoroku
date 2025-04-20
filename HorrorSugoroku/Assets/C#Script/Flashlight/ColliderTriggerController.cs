using UnityEngine;

public class ColliderTriggerController : MonoBehaviour
{
    [SerializeField] private GameObject playerObject; // プレイヤーオブジェクトを直接指定
    [SerializeField] private GlobalLightController globalLightController; // GlobalLightControllerへの参照
    private bool isPlayerInZone = false; // プレイヤーが範囲内にいるかどうか

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnterが呼び出されました"); // 確認ログ
        Debug.Log($"対象オブジェクト: {other.name} (Tag: {other.tag})"); // 衝突オブジェクト情報

        if (other.gameObject == playerObject)
        {
            isPlayerInZone = true;
            Debug.Log($"指定されたプレイヤーが範囲に入りました: {other.name}");
        }
        else
        {
            Debug.Log($"指定されたプレイヤーではありません: {other.name}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExitが呼び出されました"); // 確認ログ
        Debug.Log($"対象オブジェクト: {other.name} (Tag: {other.tag})"); // 離脱オブジェクト情報

        if (other.gameObject == playerObject)
        {
            isPlayerInZone = false;
            Debug.Log($"指定されたプレイヤーが範囲を離れました: {other.name}");
        }
        else
        {
            Debug.Log($"指定されたプレイヤーではありません: {other.name}");
        }
    }

    void Update()
    {
        Debug.Log($"Update内チェック: isPlayerInZone = {isPlayerInZone}"); // フラグ状態を常に確認

        // Aキーが押されるかどうかを確認
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aキーが押されました");
        }

        // プレイヤーが範囲内にいる場合のみAキーを受付
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.A))
        {
            if (globalLightController != null)
            {
                globalLightController.ApplyLightState();
                Debug.Log("GlobalLightController の ApplyLightState を呼び出しました");
            }
            else
            {
                Debug.LogWarning("GlobalLightController が設定されていません！");
            }
        }
    }
}