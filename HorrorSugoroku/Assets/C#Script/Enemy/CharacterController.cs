using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        if (Vector3.Distance(currentPosition, lastPosition) > 0.01f) // 0.01f��臒l�ł�
        {
            animator.SetBool("is Running", true); // �ړ�����isRunning��true�ɐݒ�
        }
        else
        {
            animator.SetBool("is Running", false); // �ҋ@����isRunning��false�ɐݒ�
        }
        lastPosition = currentPosition;

        // �L�����N�^�[�̈ړ������ɑ̂�������
        if (currentPosition != lastPosition)
        {
            Vector3 direction = (currentPosition - lastPosition).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }
}