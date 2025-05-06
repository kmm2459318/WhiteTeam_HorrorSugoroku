using UnityEngine;

public class PlaceInFrontOfplayer : MonoBehaviour
{
    public Transform player;           // プレイヤーのTransform
    public float distanceInFront = 2f; // プレイヤーの前に出す距離
    public bool matchRotation = true;  // プレイヤーの向きに合わせるかどうか

    void Update()
    {
        if (player != null)
        {
            // プレイヤーの前方位置を計算
            Vector3 frontPosition = player.position + player.forward * distanceInFront;

            // 高さはプレイヤーに合わせる（必要なら）
           // frontPosition.y = player.position.y;

            // オブジェクトの位置を更新
            transform.position = frontPosition;

            // 向きもプレイヤーに合わせる（必要な場合）
            if (matchRotation)
            {
                transform.rotation = player.rotation;
            }
        }
    }
}
