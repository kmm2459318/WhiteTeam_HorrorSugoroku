using UnityEngine;

public class DynamicLightRang : MonoBehaviour
{
    [Header("�ݒ�")]
    public float changeSpeed = 1f; // ���x�𒲐�����
    public float stepSize = 1f;    // �����̒l�𒲐�����

    private float currentValue;    // ���݂̒l
    private float targetValue;     // �ڕW�l
    private const float minValue = -126.342f; // �͈͂̍ŏ��l
    private const float maxValue = -95.996f;  // �͈͂̍ő�l

    void Start()
    {
        ResetValues();
    }

    void Update()
    {
        // �ڕW�l�ɓ��B�����烊�Z�b�g
        if (Mathf.Approximately(currentValue, targetValue))
        {
            ResetValues();
        }
        else
        {
            // �}stepSize�������_���ɕω�
            float step = Random.Range(0, 2) == 0 ? stepSize : -stepSize;
            currentValue = Mathf.MoveTowards(currentValue, targetValue, changeSpeed * Time.deltaTime);

            // ��]�p�x���X�V
            transform.rotation = Quaternion.Euler(currentValue, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }

    private void ResetValues()
    {
        currentValue = Random.Range(minValue, maxValue); // ���݂̒l�������_���ɐݒ�
        targetValue = Random.Range(minValue, maxValue);  // �ڕW�l�������_���ɐݒ�
    }
}