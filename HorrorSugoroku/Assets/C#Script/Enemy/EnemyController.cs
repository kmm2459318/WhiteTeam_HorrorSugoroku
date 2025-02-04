using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private bool isMoving;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            animator.SetBool("isRunning", true);
            //Debug.Log("isRunning set to true");
        }
        else
        {
            animator.SetBool("isRunning", false);
            //Debug.Log("isRunning set to false");
        }
    }

    public void SetMovement(bool moving)
    {
        isMoving = moving;
        Debug.Log("SetMovement called with: " + moving);
    }
}