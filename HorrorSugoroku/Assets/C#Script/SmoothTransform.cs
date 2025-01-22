using UnityEngine;

namespace SmoothigTransform
{
    public class SmoothTransform : MonoBehaviour
    {
        public Vector3 TargetPosition;
        //public Vector3 TargetScale;
        public Quaternion TargetRotation;
        public float PosFact { set; get; } = 0.9f;
        public float RotFact { set; get; } = 0.6f;

        public void Start()
        {
            TargetPosition = transform.localPosition;
            //TargetScale = transform.localScale;
            TargetRotation = transform.localRotation;
        }

        public void Update()
        {
            var p = 1 - Mathf.Pow(0.1f, Time.deltaTime / PosFact);
            var r = 1 - Mathf.Pow(0.1f, Time.deltaTime / RotFact);  //TimeFactïbÇ≈ç°Ç¢ÇÈèÍèäÇ©ÇÁ1/10Ç‹Ç≈ä‘ÇãlÇﬂÇÈÇΩÇﬂÇÃíl
            transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, p);
            //transform.localScale = Vector3.Lerp(transform.localScale, TargetScale, t);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, TargetRotation, r);
        }
    }
}
