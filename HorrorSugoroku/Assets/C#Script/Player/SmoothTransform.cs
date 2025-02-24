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
            var p = 1 - Mathf.Pow(0.1f, Time.deltaTime / PosFact); // ˆÊ’u•âŠÔ—p
            var r = 1 - Mathf.Pow(0.1f, Time.deltaTime / RotFact); // ‰ñ“]•âŠÔ—p

            transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, p);

            // YŽ²‚Ì‰ñ“]‚Ì‚Ý‚ð•âŠÔ
            Quaternion currentRotation = transform.localRotation;
            Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles.x, TargetRotation.eulerAngles.y, currentRotation.eulerAngles.z);
            transform.localRotation = Quaternion.Lerp(currentRotation, targetRotation, r);
        }

        /*private Vector3 SnapToGrid(Vector3 position)
        {
            float snappedX = Mathf.Round(position.x / 2f) * 2f;
            float snappedZ = Mathf.Round(position.z / 2f) * 2f;
            return new Vector3(snappedX, position.y, snappedZ);
        }*/
    }
}