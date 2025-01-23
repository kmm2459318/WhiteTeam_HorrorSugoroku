using UnityEngine;

namespace SmoothigTransform
{
    public class SmoothTransform : MonoBehaviour
    {
        // �v���C���[���ړ�����ڕW�̈ʒu
        public Vector3 TargetPosition;

        // �v���C���[�̖ڕW�̉�]
        public Quaternion TargetRotation;

        // �ʒu��Ԃ̃X���[�Y���𐧌䂷��W���i�������قǑ����ړ�����j
        public float PosFact { set; get; } = 0.9f;

        // ��]��Ԃ̃X���[�Y���𐧌䂷��W���i�������قǑ�����]����j
        public float RotFact { set; get; } = 0.6f;

        /// <summary>
        /// ���������Ɍ��݂�Transform��ڕW�ʒu�E��]�Ƃ��Đݒ�
        /// </summary>
        public void Start()
        {
            TargetPosition = transform.localPosition;
            TargetRotation = transform.localRotation;
        }

        /// <summary>
        /// �t���[�����ƂɖڕW�ʒu�Ɖ�]�Ɍ������ăX���[�Y�ɕ��
        /// </summary>
        public void Update()
        {
            // ��ԌW�����v�Z�iTime.deltaTime���g���t���[���Ɉˑ����Ȃ��悤�Ɂj
            var p = 1 - Mathf.Pow(0.1f, Time.deltaTime / PosFact); // �ʒu��ԗp
            var r = 1 - Mathf.Pow(0.1f, Time.deltaTime / RotFact); // ��]��ԗp

            // �ڕW�ʒu�ɃX���[�Y�ɋ߂Â�
            transform.localPosition = Vector3.Lerp(transform.localPosition, SnapToGrid(TargetPosition), p);

            // �ڕW��]�ɃX���[�Y�ɋ߂Â�
            transform.localRotation = Quaternion.Lerp(transform.localRotation, TargetRotation, r);
        }

        /// <summary>
        /// �w�肳�ꂽ�ʒu��X-Z���ʂ̃O���b�h�i2�P�ʊԊu�j�ɃX�i�b�v����
        /// </summary>
        /// <param name="position">�X�i�b�v�Ώۂ̈ʒu</param>
        /// <returns>�X�i�b�v���ꂽ�ʒu</returns>
        private Vector3 SnapToGrid(Vector3 position)
        {
            // X, Z���W��2�P�ʂ��ƂɃX�i�b�v�i�����{�Ɋۂ߂�j
            float snappedX = Mathf.Round(position.x / 2f) * 2f;
            float snappedZ = Mathf.Round(position.z / 2f) * 2f;

            // Y���W�͂��̂܂ܕێ�
            return new Vector3(snappedX, position.y, snappedZ);
        }
    }
}
