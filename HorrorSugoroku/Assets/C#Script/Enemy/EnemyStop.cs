using UnityEngine;

public class EnemyStop : MonoBehaviour
{
    public GameManager gameManager;
    public bool rideMasu = false;
    private Animator animator;
    public string targetTag = "masu";
    public float threshold = 0.1f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
        animator.SetBool("isIdle", true);
        animator.SetBool("isRunning", false);
    }
}