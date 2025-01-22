using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMover : MonoBehaviour
{
    public int remainingSteps = 10;   // �����̕���
    public float moveSpeed = 5f;     // �ړ����x

    private bool isMoving = false;   // �ړ������ǂ���
    public delegate void OnStepsDepleted(); // ������0�ɂȂ����Ƃ��̃C�x���g
    public event OnStepsDepleted StepsDepletedEvent; // �C�x���g�n���h���[
    public GridCell gridCell;
    void Start()
    {
        // ������0�ɂȂ����Ƃ��ɔ�������C�x���g��o�^
        StepsDepletedEvent += OnStepsDepletedAction;
    }
    void Update()
    {
        // �L�[�{�[�h���͂Ńv���C���[���ړ�
        if (!isMoving && remainingSteps > 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                StartCoroutine(Move(Vector3.forward));
            if (Input.GetKey(KeyCode.DownArrow))
                StartCoroutine(Move(Vector3.back));
            if (Input.GetKey(KeyCode.LeftArrow))
                StartCoroutine(Move(Vector3.left));
            if (Input.GetKey(KeyCode.RightArrow))
                StartCoroutine(Move(Vector3.right));
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction;

        float time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }

        transform.position = targetPosition;

        remainingSteps--; // ����������
        Debug.Log($"�c�����: {remainingSteps}");

        // ������0�ɂȂ����ꍇ�ɃC�x���g�𔭓�
        if (remainingSteps <= 0)
        {
            StepsDepletedEvent?.Invoke();
        }

        isMoving = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // �^�O��"�}�X"�̏ꍇ�ɕ��������炷
        if (other.CompareTag("masu"))
        {
            remainingSteps--;

            Debug.Log($"�c�����: {remainingSteps}");

            // ������0�ɂȂ����珈�����~�܂��͏I��
            if (remainingSteps <= 0)
            {
                Debug.Log("������0�ɂȂ�܂����B");
                // �K�v�ɉ����Ĉړ���~�⑼�̏��������s
            }
        }
    }

    // ������0�ɂȂ����Ƃ��̃C�x���g�A�N�V����
    private void OnStepsDepletedAction()
    {
        Debug.Log("������0�ɂȂ�܂����I�C�x���g�𔭓����܂��B");
        gridCell.ExecuteEvent();
    }
     
} // �K�v�ȃC�x���g�����������ɒǉ�
    // ��: �Q�[���̏I���A�v���C���[�̒�~�Ȃ�


