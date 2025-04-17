using UnityEngine;

namespace SmoothigTransform
{
    public class SmoothTransform : MonoBehaviour
    {
        public Vector3 TargetPosition;
        public Quaternion TargetRotation;
        public float PosFact { set; get; } = 0.9f;
        public float RotFact { set; get; } = 0.6f;

        void Start()
        {
            TargetPosition = transform.position;
            TargetRotation = transform.rotation;
        }

        void Update()
        {
            // 補間係数（経過時間に応じて自然に）
            float p = 1 - Mathf.Pow(0.1f, Time.deltaTime / PosFact);
            float r = 1 - Mathf.Pow(0.1f, Time.deltaTime / RotFact);

            // 位置補間（ワールド座標）
            transform.position = Vector3.Lerp(transform.position, TargetPosition, p);

            // Y軸回転だけ補間
            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(
                currentRotation.eulerAngles.x,
                TargetRotation.eulerAngles.y,
                currentRotation.eulerAngles.z
            );
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, r);
        }

        /// <summary>
        /// Y座標だけを外部から補間ターゲットとして設定する
        /// </summary>
        /// <param name="y">新しいY座標</param>
        public void SetTargetY(float y)
        {
            TargetPosition = new Vector3(TargetPosition.x, y, TargetPosition.z);
        }
    }
}
