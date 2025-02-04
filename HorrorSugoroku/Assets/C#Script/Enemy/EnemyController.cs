using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private bool isMoving;
    public GameManager gameManager;
    public EnemySaikoro enemySaikoro;
    int mp = 0;
    float mo = 3f;
    int id = 1;

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

        mp = gameManager.mapPiece;

        if (mp < 3) //１段階　アチャモ
        {
            mo = 6f;
            id = 1;
        }
        else if (mp < 6) //２段階　ワカシャモ
        {
            mo = 8f;
            id = 2;
            enemySaikoro.skill1 = true;
        }
        else if (mp < 9) //３段階　バシャーモ
        {
            mo = 10f;
            id = 3;
            enemySaikoro.skill2 = true;
        }
        else //目が進化　メガバシャーモ
        {
            mo = 12f;
            id = 4;
        }

        enemySaikoro.mokushi = mo;
        enemySaikoro.idoukagen = id;
    }

    public void SetMovement(bool moving)
    {
        isMoving = moving;
        Debug.Log("SetMovement called with: " + moving);
    }
}