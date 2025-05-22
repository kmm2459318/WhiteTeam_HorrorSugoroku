using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour
{
    public Transform innerDoorLeft;
    public Transform innerDoorRight;
    public Transform innerSensorLeft;
    public Transform innerSensorRight;

    [System.Serializable]
    public class OuterDoors
    {
        public Transform left;
        public Transform right;
    }

    public OuterDoors[] outerDoorsByFloor = new OuterDoors[3];
    public Transform elevator;

    private float doorOpenDistance = 0.6f;
    private float doorSpeed = 1.0f;
    private bool isDoorOpen = false;
    private int currentFloor = 1;
    private float moveSpeed = 2.0f;
    public bool isMoving = false;
    private bool isDoorMoving = false;

    private float[] baseFloorHeights = { -3.85f, 0f, 3.81f };
    private const float heightOffset = 0.0164361f;
    private float[] floorHeights;

    private void Awake()
    {
        floorHeights = new float[baseFloorHeights.Length];
        for (int i = 0; i < baseFloorHeights.Length; i++)
        {
            floorHeights[i] = baseFloorHeights[i] + heightOffset;
        }
    }

    private void Update()
    {
        if (isMoving || isDoorMoving) return;

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    StartCoroutine(ToggleDoors());
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha1)) StartCoroutine(MoveToFloor(0));
        //if (Input.GetKeyDown(KeyCode.Alpha2)) StartCoroutine(MoveToFloor(1));
        //if (Input.GetKeyDown(KeyCode.Alpha3)) StartCoroutine(MoveToFloor(2));
    }

    private float maxSensorOffset = 0.02f; // 扉との最大ずれを小さく
    private float sensorLeadTime = 0.15f;   // センサーが先に動き始める時間（閉まるとき）

    // 開くときに使うイージング（ゆっくり→速く）
    private float EaseOutCubic(float t) => 1 - Mathf.Pow(1 - t, 3);

    public IEnumerator ToggleDoors()
    {
        if (isDoorMoving) yield break;
        isDoorMoving = true;

        bool opening = !isDoorOpen;
        isDoorOpen = opening;
        float direction = opening ? 1 : -1;

        Transform outerLeft = outerDoorsByFloor[currentFloor].left;
        Transform outerRight = outerDoorsByFloor[currentFloor].right;

        Vector3 iLeftStart = innerDoorLeft.localPosition;
        Vector3 iRightStart = innerDoorRight.localPosition;
        Vector3 oLeftStart = outerLeft.localPosition;
        Vector3 oRightStart = outerRight.localPosition;
        Vector3 sLeftStart = innerSensorLeft.localPosition;
        Vector3 sRightStart = innerSensorRight.localPosition;

        Vector3 iLeftTarget = iLeftStart + Vector3.left * doorOpenDistance * direction;
        Vector3 iRightTarget = iRightStart + Vector3.right * doorOpenDistance * direction;
        Vector3 oLeftTarget = oLeftStart + Vector3.left * doorOpenDistance * direction;
        Vector3 oRightTarget = oRightStart + Vector3.right * doorOpenDistance * direction;
        Vector3 sLeftTarget = sLeftStart + Vector3.left * doorOpenDistance * direction;
        Vector3 sRightTarget = sRightStart + Vector3.right * doorOpenDistance * direction;

        float totalTime = 1f / doorSpeed;
        float elapsed = 0f;

        while (elapsed < totalTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / totalTime);

            float tSensor = opening ? EaseOutCubic(t) : Mathf.SmoothStep(0f, 1f, t);
            float tDoor;

            if (opening)
            {
                tDoor = EaseOutCubic(t);
            }
            else
            {
                if (t < sensorLeadTime)
                    tDoor = 0f;
                else
                    tDoor = Mathf.Clamp01((t - sensorLeadTime) / (1f - sensorLeadTime));
                tDoor = Mathf.SmoothStep(0f, 1f, tDoor); // スムーズに閉める
            }

            // センサーの途中オフセット（視覚的なずれの演出）
            float sensorOffset = maxSensorOffset * (1f - tSensor); // 最初は最大、最後は0

            Vector3 sensorLeftPos = Vector3.Lerp(sLeftStart, sLeftTarget, tSensor) + Vector3.right * sensorOffset * direction;
            Vector3 sensorRightPos = Vector3.Lerp(sRightStart, sRightTarget, tSensor) + Vector3.left * sensorOffset * direction;

            innerSensorLeft.localPosition = sensorLeftPos;
            innerSensorRight.localPosition = sensorRightPos;

            innerDoorLeft.localPosition = Vector3.Lerp(iLeftStart, iLeftTarget, tDoor);
            innerDoorRight.localPosition = Vector3.Lerp(iRightStart, iRightTarget, tDoor);
            outerLeft.localPosition = Vector3.Lerp(oLeftStart, oLeftTarget, tDoor);
            outerRight.localPosition = Vector3.Lerp(oRightStart, oRightTarget, tDoor);

            yield return null;
        }

        // 最終補正
        innerDoorLeft.localPosition = iLeftTarget;
        innerDoorRight.localPosition = iRightTarget;
        outerLeft.localPosition = oLeftTarget;
        outerRight.localPosition = oRightTarget;
        innerSensorLeft.localPosition = sLeftTarget;
        innerSensorRight.localPosition = sRightTarget;

        isDoorMoving = false;
    }

    public IEnumerator MoveToFloor(int floorIndex)
    {
        isMoving = true;

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
        yield return StartCoroutine(ToggleDoors());

        isMoving = false;
    }
}
