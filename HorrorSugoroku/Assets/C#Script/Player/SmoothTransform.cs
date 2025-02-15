using UnityEngine;

namespace SmoothigTransform
{
    public class SmoothTransform : MonoBehaviour
    {
        public Vector3 TargetPosition;
        public Quaternion TargetRotation;
        public float PosFact { set; get; } = 0.9f;
        public float RotFact { set; get; } = 0.6f;

        public void Start()
        {
            TargetPosition = transform.localPosition;
            TargetRotation = transform.localRotation;
        }

        public void Update()
        {
            var p = 1 - Mathf.Pow(0.1f, Time.deltaTime / PosFact); // �ʒu��ԗp
            var r = 1 - Mathf.Pow(0.1f, Time.deltaTime / RotFact); // ��]��ԗp

            transform.localPosition = Vector3.Lerp(transform.localPosition, SnapToGrid(TargetPosition), p);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, TargetRotation, r);
        }

        private Vector3 SnapToGrid(Vector3 position)
        {
            float snappedX = Mathf.Round(position.x / 2f) * 2f;
            float snappedZ = Mathf.Round(position.z / 2f) * 2f;
            return new Vector3(snappedX, position.y, snappedZ);
        }
    }
}