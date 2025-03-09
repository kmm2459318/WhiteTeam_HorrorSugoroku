using UnityEngine;

public class DiceRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    private float rotationSpeed = 10f;
    private int dice;
    [SerializeField] private PlayerSaikoro playerSaikoro;
    private bool shouldRotate = false;

    // �o�ڂ��Ƃ̉�] (��������ʂ��)
    private Vector3[] faceRotations = new Vector3[]
    {
        new Vector3(-90, 0, 0),  // 1�̖ʂ���
        new Vector3(0, 0, 0),    // 2�̖ʂ���
        new Vector3(0, 0, -90),  // 3�̖ʂ���
        new Vector3(0, 0, 90),   // 4�̖ʂ���
        new Vector3(180, 180, 0),// 5�̖ʂ���
        new Vector3(90, 0, 0)    // 6�̖ʂ���
    };

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (shouldRotate)
        {
            RotateDice();
        }
    }

    private void RotateDice()
    {
        if (dice < 1 || dice > 6)
        {
            Debug.LogWarning("�s���ȃT�C�R���̏o��: " + dice);
            shouldRotate = false;
            return;
        }

        // �o�ڂɑΉ������]�p�x���擾
        targetRotation = Quaternion.Euler(faceRotations[dice - 1]);

        // �X���[�Y�ɉ�]������
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // �ڕW�̊p�x�ɂقړ��B�������]���~
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation;
            shouldRotate = false;
            Debug.Log("��]����: " + dice);
        }
    }

    public void GetDiceNumber(int sai)
    {
        shouldRotate = true;  // ��]���J�n
        dice = sai;
        Debug.Log($"�󂯎�����o��: {dice}");
    }
}
