using UnityEngine;

public class DiceRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    private float rotationSpeed = 10f;
    private float moveSpeed = 5f; // �ړ����x

    [SerializeField] private PlayerSaikoro playerSaikoro;
    private bool shouldRotate = false;
    private bool shouldMoveToSide = false;
    private bool shouldMoveToCenter = false;

    private Vector3 centerPosition;
    private Vector3 sidePosition = new Vector3(-6.5f, 1f, -3f);

    private Vector3[] faceRotations = new Vector3[]
    {
        new Vector3(-90, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, -90),
        new Vector3(0, 0, 90),
        new Vector3(180, 180, 0),
        new Vector3(90, 0, 0)
    };

    void Start()
    {
        targetRotation = transform.rotation;
        centerPosition = transform.position;
        Debug.Log("�����ʒu: " + centerPosition);
    }

    void Update()
    {
        if (shouldRotate)
        {
            RotateDice();
        }
        else if (shouldMoveToSide)
        {
            MoveToSide();
        }
        else if (shouldMoveToCenter)
        {
            MoveToCenter();
        }
    }

    private void RotateDice()
    {
        if (playerSaikoro != null)
        {
            int faceValue = playerSaikoro.sai;
            if (faceValue >= 1 && faceValue <= 6)
            {
                targetRotation = Quaternion.Euler(faceRotations[faceValue - 1]);
            }
        }

        if (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = targetRotation;
            shouldRotate = false;
            shouldMoveToSide = true; // ��]������ɒ[�ֈړ�
            Debug.Log("��]���� �� �[�ֈړ��J�n");
        }
    }

    private void MoveToSide()
    {
        Debug.Log("�ړ��� (�[��): " + transform.position);
        transform.position = Vector3.MoveTowards(transform.position, sidePosition, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, sidePosition) < 0.1f)
        {
            transform.position = sidePosition;
            shouldMoveToSide = false;
            Debug.Log("�[�ւ̈ړ�����");
        }
    }

    private void MoveToCenter()
    {
        Debug.Log("�ړ��� (������): " + transform.position);
        transform.position = Vector3.MoveTowards(transform.position, centerPosition, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, centerPosition) < 0.1f)
        {
            transform.position = centerPosition;
            shouldMoveToCenter = false;
            Debug.Log("�����ւ̈ړ�����");
        }
    }

    public void StartRotation()
    {
        shouldRotate = true;
        Debug.Log("�T�C�R����]�J�n");
    }

    public void ResetToCenter()
    {
        shouldMoveToCenter = true;
        Debug.Log("�����֖߂������J�n");
    }
}
