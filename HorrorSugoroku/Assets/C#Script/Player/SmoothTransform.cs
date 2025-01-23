using UnityEngine;

namespace SmoothigTransform
{
    public class SmoothTransform : MonoBehaviour
    {
        //public Vector3 TargetPosition;
        //public Vector3 TargetScale;
        public Quaternion TargetRotation;
        public float TimeFact { set; get; } = 0.15f;

        public void Start()
        {
            //TargetPosition = transform.localPosition;
            //TargetScale = transform.localScale;
            TargetRotation = transform.localRotation;
        }

        public void Update()
        {
            var t = 1 - Mathf.Pow(0.1f, Time.deltaTime / TimeFact);  //TimeFactïbÇ≈ç°Ç¢ÇÈèÍèäÇ©ÇÁ1/10Ç‹Ç≈ä‘ÇãlÇﬂÇÈÇΩÇﬂÇÃíl
            //transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, t);
            //transform.localScale = Vector3.Lerp(transform.localScale, TargetScale, t);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, TargetRotation, t);
        }
    }
}
