using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour
{
    // ���h�A�i���E�j
    public Transform innerDoorLeft;
    public Transform innerDoorRight;

    // �O�h�A�i�K���Ƃɍ��E�j
    [System.Serializable]
    public class OuterDoors
    {
        public Transform left;
        public Transform right;
    }

    public OuterDoors[] outerDoorsByFloor = new OuterDoors[3];

    private float doorOpenDistance = 0.6f; // �J�������iX�����j
    private float doorSpeed = 2.0f;

    private bool isDoorOpen = false;
    private int currentFloor = 1; // ������ԁF1F�i�C���f�b�N�X1�j

    // �G���x�[�^�[�ړ�
    public Transform elevator; // �G���x�[�^�[�{��
    private float[] floorHeights = { -3.85f + 0.0164361f, 0 + 0.0164361f, 3.81f + 0.0164361f }; // B1, 1F, 2F (Y���W) ���ڕW���W + 0.0164361f��
    private float moveSpeed = 2.0f;
    private bool isMoving = false; // �G���x�[�^�[���ړ������ǂ����𔻒肷��t���O
    private bool isDoorMoving = false; // �h�A���J�����ǂ����𔻒肷��t���O

    void Update()
    {
        // �G���x�[�^�[�ړ�����h�A�J���ɃL�[�𖳌���
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
        // �h�A�J���͉������Ȃ�
        if (isDoorMoving) yield break;

        isDoorMoving = true;
        isDoorOpen = !isDoorOpen;

        // ���h�A
        Vector3 innerLeftTarget = innerDoorLeft.localPosition + Vector3.left * doorOpenDistance * (isDoorOpen ? 1 : -1);
        Vector3 innerRightTarget = innerDoorRight.localPosition + Vector3.right * doorOpenDistance * (isDoorOpen ? 1 : -1);

        // ���݂̊K�̊O�h�A
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

        isDoorMoving = false; // �h�A�̃A�j���[�V�������I��������t���O�����Z�b�g
    }

    IEnumerator MoveToFloor(int floorIndex)
    {
        isMoving = true;

        // �h�A���J���Ă�����߂�
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

        // ������h�A���J����
        yield return StartCoroutine(ToggleDoors());

        isMoving = false;
    }
}
