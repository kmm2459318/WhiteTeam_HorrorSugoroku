using UnityEngine;
using UnityEngine.AI;

public class EnemyStop : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerSaikoro player;
    public EnemyWalkCounter enemyWalkCounter;
    public CurseSlider curse;
    public NavMeshAgent nuvMeshAgent;
    private bool rideMasu = false;
    public bool stopMasu = false;
    private bool se = false;
    public string targetTag = "masu";
    private float exploringCoolTime = 0;
    public float threshold = 0.1f;
    public int walkNumber = 0;

    void Start()
    {
        nuvMeshAgent.speed = 0;
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
            nuvMeshAgent.speed = 0f;
            stopMasu = true;
        }

        if (!gameManager.isPlayerTurn && !se)
        {
            se = true;
            stopMasu = false;
            nuvMeshAgent.speed = 1.62f;

            enemyWalkCounter.walkMasu = 0;
            walkNumber = Random.Range(0, gameManager.Doll + 2) + 1;
            if (curse.curse1_1) //呪１による増加
            {
                walkNumber += 2;
                //curse.curse1Turn--;
            }
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
            else
            {
                rideMasu = false;
            }
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