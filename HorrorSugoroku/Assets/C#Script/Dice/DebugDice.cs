using UnityEngine;

public class DebugDice : MonoBehaviour
{
    private Quaternion targetRotation;
    private float rotationSpeed = 10f;
    void Start()
    {
        new Vector3(-90, 0, 0);
    }

    [SerializeField] private int faceValue = 1; // インスペクターで指定する出目
    private Vector3[] faceRotations = new Vector3[]
    {
        new Vector3(-90, 0, 0),     // 1の面が上
        new Vector3(0, 0, 0),       // 2の面が上
        new Vector3(0, 0, -90),     // 3の面が上
        new Vector3(0, 0, 90),      // 4の面が上
        new Vector3(180, 180, 0),   // 5の面が上
        new Vector3(90, 0, 0)       // 6の面が上
    };

    void Update()
    {
        if (faceValue >= 1 && faceValue <= 6)
        {
            targetRotation = Quaternion.Euler(faceRotations[faceValue - 1]);
        }

        // 一定以上近づいたら最終的にピタッと合わせる
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
