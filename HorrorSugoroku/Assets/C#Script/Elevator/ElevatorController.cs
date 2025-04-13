using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour
{
    // 内ドア（左右）
    public Transform innerDoorLeft;
    public Transform innerDoorRight;

    // 外ドア（階ごとに左右）
    [System.Serializable]
    public class OuterDoors
    {
        public Transform left;
        public Transform right;
    }

    public OuterDoors[] outerDoorsByFloor = new OuterDoors[3];

    private float doorOpenDistance = 0.6f; // 開く距離（X方向）
    private float doorSpeed = 2.0f;

    private bool isDoorOpen = false;
    private int currentFloor = 1; // 初期状態：1F（インデックス1）

    // エレベーター移動
    public Transform elevator; // エレベーター本体
    private float[] floorHeights = { -3.85f + 0.0164361f, 0 + 0.0164361f, 3.81f + 0.0164361f }; // B1, 1F, 2F (Y座標) ★目標座標 + 0.0164361f★
    private float moveSpeed = 2.0f;
    private bool isMoving = false; // エレベーターが移動中かどうかを判定するフラグ
    private bool isDoorMoving = false; // ドアが開閉中かどうかを判定するフラグ

    void Update()
    {
        // エレベーター移動中やドア開閉中にキーを無効化
        if (isMoving || isDoorMoving) return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(ToggleDoors());
        }

        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartCoroutine(MoveToFloor(0));
            if (Input.GetKeyDown(KeyCode.Alpha2))
                StartCoroutine(MoveToFloor(1));
            if (Input.GetKeyDown(KeyCode.Alpha3))
                StartCoroutine(MoveToFloor(2));
        }
    }

    IEnumerator ToggleDoors()
    {
        // ドア開閉中は何もしない
        if (isDoorMoving) yield break;

        isDoorMoving = true;
        isDoorOpen = !isDoorOpen;

        // 内ドア
        Vector3 innerLeftTarget = innerDoorLeft.localPosition + Vector3.left * doorOpenDistance * (isDoorOpen ? 1 : -1);
        Vector3 innerRightTarget = innerDoorRight.localPosition + Vector3.right * doorOpenDistance * (isDoorOpen ? 1 : -1);

        // 現在の階の外ドア
        Transform outerLeft = outerDoorsByFloor[currentFloor].left;
        Transform outerRight = outerDoorsByFloor[currentFloor].right;

        Vector3 outerLeftTarget = outerLeft.localPosition + Vector3.left * doorOpenDistance * (isDoorOpen ? 1 : -1);
        Vector3 outerRightTarget = outerRight.localPosition + Vector3.right * doorOpenDistance * (isDoorOpen ? 1 : -1);

        float elapsed = 0f;
        Vector3 iLeftStart = innerDoorLeft.localPosition;
        Vector3 iRightStart = innerDoorRight.localPosition;
        Vector3 oLeftStart = outerLeft.localPosition;
        Vector3 oRightStart = outerRight.localPosition;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * doorSpeed;
            innerDoorLeft.localPosition = Vector3.Lerp(iLeftStart, innerLeftTarget, elapsed);
            innerDoorRight.localPosition = Vector3.Lerp(iRightStart, innerRightTarget, elapsed);
            outerLeft.localPosition = Vector3.Lerp(oLeftStart, outerLeftTarget, elapsed);
            outerRight.localPosition = Vector3.Lerp(oRightStart, outerRightTarget, elapsed);
            yield return null;
        }

        isDoorMoving = false; // ドアのアニメーションが終了したらフラグをリセット
    }

    IEnumerator MoveToFloor(int floorIndex)
    {
        isMoving = true;

        // ドアが開いていたら閉める
        if (isDoorOpen)
        {
            yield return StartCoroutine(ToggleDoors());
        }

        currentFloor = floorIndex;

        float targetY = floorHeights[floorIndex];
        Vector3 start = elevator.position;
        Vector3 end = new Vector3(elevator.position.x, targetY, elevator.position.z);

        float elapsed = 0f;
        float duration = Mathf.Abs(targetY - elevator.position.y) / moveSpeed;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            elevator.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        elevator.position = end;

        // 到着後ドアを開ける
        yield return StartCoroutine(ToggleDoors());

        isMoving = false;
    }
}
