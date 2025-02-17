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
        if (Vector3.Distance(currentPosition, lastPosition) > 0.01f) // 0.01fは閾値です
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        lastPosition = currentPosition;

        // キャラクターの移動方向に体を向ける
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