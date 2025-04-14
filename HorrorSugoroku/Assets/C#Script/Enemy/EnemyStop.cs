using UnityEngine;
using UnityEngine.AI;

public class EnemyStop : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerSaikoro player;
    public EnemyWalkCounter enemyWalkCounter;
    public NavMeshAgent nuvMeshAgent;
    private Animator animator;
    private bool rideMasu = false;
    public bool stopMasu = false;
    private bool se = false;
    public string targetTag = "masu";
    private float exploringCoolTime = 0;
    public float threshold = 0.1f;
    private int walkNumber = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        nuvMeshAgent.enabled = false;
    }

    void Update()
    {
        if (player.exploring && !stopMasu)
        {
            this.exploringCoolTime += Time.deltaTime;
        }

        if (player.enemyEnd && rideMasu && exploringCoolTime > 0.8f
            && walkNumber <= enemyWalkCounter.walkMasu)
        {
            exploringCoolTime = 0f;
            nuvMeshAgent.enabled = false;
            stopMasu = true;
        }

        if (!gameManager.isPlayerTurn && !se)
        {
            se = true;
            stopMasu = false;
            nuvMeshAgent.enabled = true;

            enemyWalkCounter.walkMasu = 0;
            walkNumber = Random.Range(0, gameManager.Doll + 2) + 1;
            Debug.Log("最低移動回数:" +  walkNumber);
        }

        if (player.idoutyu)
        {
            se = false;
        }

        CheckPositionMatch();
    }

    private void CheckPositionMatch()
    {
        GameObject[] masuObjects = GameObject.FindGameObjectsWithTag(targetTag);
        bool matched = false; // 一致判定用のフラグ

        foreach (GameObject masu in masuObjects)
        {
            Vector3 targetPos = masu.transform.position;
            Vector3 currentPos = transform.position;

            if (Mathf.Abs(currentPos.x - targetPos.x) < threshold
                && Mathf.Abs(currentPos.z - targetPos.z) < threshold
                && Mathf.Abs(currentPos.y - targetPos.y) < 1f)
            {
                OnMatched(masu);
                matched = true; // 一致したらフラグを立てる
                break;
            }
        }

        // 一致していなかった場合の処理
        if (!matched)
        {
            rideMasu = false;
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", true); // スペースを削除
        }
    }

    private void OnMatched(GameObject masu)
    {
        Debug.Log($"一致: {masu.name} と {gameObject.name}");
        rideMasu = true;
        //animator.SetBool("isIdle", true);
        //animator.SetBool("isRunning", false);
    }
}