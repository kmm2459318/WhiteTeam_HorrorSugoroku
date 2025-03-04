using UnityEngine;

public class HanteiBoxFollower : MonoBehaviour
{
    public GameObject player; // Playerオブジェクト
    public GameObject hanteiBox; // HanteiBoxオブジェクト
    public float heightOffset = -1.0f; // 高さのオフセット
    public float forwardOffset = 1.0f; // 前方のオフセット

    void Update()
    {
        if (player != null && hanteiBox != null)
        {
            // Playerの位置にHanteiBoxを追従させる
            Vector3 newPosition = player.transform.position;
            newPosition.y += heightOffset; // 高さを調整
            newPosition += player.transform.forward * forwardOffset; // 前方にオフセット
            hanteiBox.transform.position = newPosition;
        }
    }
}