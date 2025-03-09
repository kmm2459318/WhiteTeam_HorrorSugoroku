using UnityEngine;

public class DiceRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    private float rotationSpeed = 10f;
    private int dice;
    [SerializeField] private PlayerSaikoro playerSaikoro;
    private bool shouldRotate = false;

    // 出目ごとの回転 (上を向く面を基準)
    private Vector3[] faceRotations = new Vector3[]
    {
        new Vector3(-90, 0, 0),  // 1の面が上
        new Vector3(0, 0, 0),    // 2の面が上
        new Vector3(0, 0, -90),  // 3の面が上
        new Vector3(0, 0, 90),   // 4の面が上
        new Vector3(180, 180, 0),// 5の面が上
        new Vector3(90, 0, 0)    // 6の面が上
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
            Debug.LogWarning("不正なサイコロの出目: " + dice);
            shouldRotate = false;
            return;
        }

        // 出目に対応する回転角度を取得
        targetRotation = Quaternion.Euler(faceRotations[dice - 1]);

        // スムーズに回転させる
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // 目標の角度にほぼ到達したら回転を停止
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation;
            shouldRotate = false;
            Debug.Log("回転完了: " + dice);
        }
    }

    public void GetDiceNumber(int sai)
    {
        shouldRotate = true;  // 回転を開始
        dice = sai;
        Debug.Log($"受け取った出目: {dice}");
    }
}
