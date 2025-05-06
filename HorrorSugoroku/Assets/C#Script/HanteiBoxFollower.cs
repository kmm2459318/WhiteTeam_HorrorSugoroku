using UnityEngine;

public class HanteiBoxFollower : MonoBehaviour
{
    public GameObject player;      // プレイヤー（位置の基準）
    public GameObject hanteiBox;   // 判定ボックス
    public float forwardDistance = 2.0f;  // 視点の前方に出す距離
    public float heightOffset = -1.0f;    // 高さ調整

    void Update()
    {
        if (player != null && hanteiBox != null && Camera.main != null)
        {
            // 視点方向の前に出す
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0; // 水平方向だけに限定（必要なら）

            Vector3 basePosition = player.transform.position;
            Vector3 targetPosition = basePosition + cameraForward.normalized * forwardDistance;
            targetPosition.y += heightOffset;

            hanteiBox.transform.position = targetPosition;

            // カメラと同じ向きに回転させたい場合はこれも追加
            hanteiBox.transform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }
    //    public GameObject player; // Playerオブジェクト
    //    public GameObject hanteiBox; // HanteiBoxオブジェクト
    //    public float heightOffset = -1.0f; // 高さのオフセット
    //    public float forwardOffset = 1.0f; // 前方のオフセット

    //    void Update()
    //    {
    //        if (player != null && hanteiBox != null)
    //        {
    //            // Playerの位置にHanteiBoxを追従させる
    //            Vector3 newPosition = player.transform.position;
    //            newPosition.y += heightOffset; // 高さを調整
    //            newPosition += player.transform.forward * forwardOffset; // 前方にオフセット
    //            hanteiBox.transform.position = newPosition;
    //        }
    //    }
}