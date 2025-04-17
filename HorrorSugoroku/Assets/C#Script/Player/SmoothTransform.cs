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
            // ��ԌW���i�o�ߎ��Ԃɉ����Ď��R�Ɂj
            float p = 1 - Mathf.Pow(0.1f, Time.deltaTime / PosFact);
            float r = 1 - Mathf.Pow(0.1f, Time.deltaTime / RotFact);

            // �ʒu��ԁi���[���h���W�j
            transform.position = Vector3.Lerp(transform.position, TargetPosition, p);

            // Y����]�������
            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(
                currentRotation.eulerAngles.x,
                TargetRotation.eulerAngles.y,
                currentRotation.eulerAngles.z
            );
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, r);
        }

        /// <summary>
        /// Y���W�������O�������ԃ^�[�Q�b�g�Ƃ��Đݒ肷��
        /// </summary>
        /// <param name="y">�V����Y���W</param>
        public void SetTargetY(float y)
        {
            TargetPosition = new Vector3(TargetPosition.x, y, TargetPosition.z);
        }
    }
}
