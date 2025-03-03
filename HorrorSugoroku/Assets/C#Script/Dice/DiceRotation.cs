using UnityEngine;

public class DiceRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    private float rotationSpeed = 10f;
    private float moveSpeed = 5f; // 移動速度

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
        Debug.Log("初期位置: " + centerPosition);
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
            shouldMoveToSide = true; // 回転完了後に端へ移動
            Debug.Log("回転完了 → 端へ移動開始");
        }
    }

    private void MoveToSide()
    {
        Debug.Log("移動中 (端へ): " + transform.position);
        transform.position = Vector3.MoveTowards(transform.position, sidePosition, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, sidePosition) < 0.1f)
        {
            transform.position = sidePosition;
            shouldMoveToSide = false;
            Debug.Log("端への移動完了");
        }
    }

    private void MoveToCenter()
    {
        Debug.Log("移動中 (中央へ): " + transform.position);
        transform.position = Vector3.MoveTowards(transform.position, centerPosition, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, centerPosition) < 0.1f)
        {
            transform.position = centerPosition;
            shouldMoveToCenter = false;
            Debug.Log("中央への移動完了");
        }
    }

    public void StartRotation()
    {
        shouldRotate = true;
        Debug.Log("サイコロ回転開始");
    }

    public void ResetToCenter()
    {
        shouldMoveToCenter = true;
        Debug.Log("中央へ戻す処理開始");
    }
}
