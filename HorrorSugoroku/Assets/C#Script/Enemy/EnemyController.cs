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

        if (mp < 3) //�P�i�K�@�A�`����
        {
            mo = 6f;
            id = 1;
        }
        else if (mp < 6) //�Q�i�K�@���J�V����
        {
            mo = 8f;
            id = 2;
            enemySaikoro.skill1 = true;
        }
        else if (mp < 9) //�R�i�K�@�o�V���[��
        {
            mo = 10f;
            id = 3;
            enemySaikoro.skill2 = true;
        }
        else //�ڂ��i���@���K�o�V���[��
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