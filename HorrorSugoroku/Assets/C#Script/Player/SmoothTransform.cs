using UnityEngine;

namespace SmoothigTransform
{
    public class SmoothTransform : MonoBehaviour
    {
        // プレイヤーが移動する目標の位置
        public Vector3 TargetPosition;

        // プレイヤーの目標の回転
        public Quaternion TargetRotation;

        // 位置補間のスムーズさを制御する係数（小さいほど早く移動する）
        public float PosFact { set; get; } = 0.9f;

        // 回転補間のスムーズさを制御する係数（小さいほど早く回転する）
        public float RotFact { set; get; } = 0.6f;

        /// <summary>
        /// 初期化時に現在のTransformを目標位置・回転として設定
        /// </summary>
        public void Start()
        {
            TargetPosition = transform.localPosition;
            TargetRotation = transform.localRotation;
        }

        /// <summary>
        /// フレームごとに目標位置と回転に向かってスムーズに補間
        /// </summary>
        public void Update()
        {
            // 補間係数を計算（Time.deltaTimeを使いフレームに依存しないように）
            var p = 1 - Mathf.Pow(0.1f, Time.deltaTime / PosFact); // 位置補間用
            var r = 1 - Mathf.Pow(0.1f, Time.deltaTime / RotFact); // 回転補間用

            // 目標位置にスムーズに近づく
            transform.localPosition = Vector3.Lerp(transform.localPosition, SnapToGrid(TargetPosition), p);

            // 目標回転にスムーズに近づく
            transform.localRotation = Quaternion.Lerp(transform.localRotation, TargetRotation, r);
        }

        /// <summary>
        /// 指定された位置をX-Z平面のグリッド（2単位間隔）にスナップする
        /// </summary>
        /// <param name="position">スナップ対象の位置</param>
        /// <returns>スナップされた位置</returns>
        private Vector3 SnapToGrid(Vector3 position)
        {
            // X, Z座標を2単位ごとにスナップ（整数倍に丸める）
            float snappedX = Mathf.Round(position.x / 2f) * 2f;
            float snappedZ = Mathf.Round(position.z / 2f) * 2f;

            // Y座標はそのまま保持
            return new Vector3(snappedX, position.y, snappedZ);
        }
    }
}
