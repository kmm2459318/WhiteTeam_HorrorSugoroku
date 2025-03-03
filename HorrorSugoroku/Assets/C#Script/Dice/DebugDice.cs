using UnityEngine;

public class DebugDice : MonoBehaviour
{
    private Quaternion targetRotation;
    private float rotationSpeed = 10f;
    void Start()
    {
        new Vector3(-90, 0, 0);
    }

    [SerializeField] private int faceValue = 1; // �C���X�y�N�^�[�Ŏw�肷��o��
    private Vector3[] faceRotations = new Vector3[]
    {
        new Vector3(-90, 0, 0),     // 1�̖ʂ���
        new Vector3(0, 0, 0),       // 2�̖ʂ���
        new Vector3(0, 0, -90),     // 3�̖ʂ���
        new Vector3(0, 0, 90),      // 4�̖ʂ���
        new Vector3(180, 180, 0),   // 5�̖ʂ���
        new Vector3(90, 0, 0)       // 6�̖ʂ���
    };

    void Update()
    {
        if (faceValue >= 1 && faceValue <= 6)
        {
            targetRotation = Quaternion.Euler(faceRotations[faceValue - 1]);
        }

        // ���ȏ�߂Â�����ŏI�I�Ƀs�^�b�ƍ��킹��
        if (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = targetRotation;
        }
    }
}
