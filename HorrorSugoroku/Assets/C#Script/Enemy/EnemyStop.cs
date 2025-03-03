using UnityEngine;

public class EnemyStop : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject ENorth;
    public GameObject EWest;
    public GameObject EEast;
    public GameObject ESouth;
    private bool EN = false;
    private bool EW = false;
    private bool EE = false;
    private bool ES = false;
    private Transform Smasu;
    private Transform Nmasu;
    private Transform Wmasu;
    private Transform Emasu;
    private Vector3 ms;
    public bool rideMasu = false;
    private Animator animator; // Animatorコンポーネント
    public string targetTag = "masu";  // 対象のタグ
    public float threshold = 0.1f; // どれくらいの誤差を許容するか

    void Start()
    {
        animator = GetComponent<Animator>(); // Animatorコンポーネントを取得
        ms = GameObject.Find("masu").transform.position; // masuの位置を取得

        if (ENorth == null) Debug.LogError("ENorth is not assigned.");
        if (EWest == null) Debug.LogError("EWest is not assigned.");
        if (EEast == null) Debug.LogError("EEast is not assigned.");
        if (ESouth == null) Debug.LogError("ESouth is not assigned.");
    }

    void Update()
    {
        EN = ENorth.GetComponent<PlayerNSEWCheck>().masuCheck;
        EW = EWest.GetComponent<PlayerNSEWCheck>().masuCheck;
        EE = EEast.GetComponent<PlayerNSEWCheck>().masuCheck;
        ES = ESouth.GetComponent<PlayerNSEWCheck>().masuCheck;
        Nmasu = ENorth.GetComponent<PlayerCloseMass>().GetClosestObject();
        Wmasu = EWest.GetComponent<PlayerCloseMass>().GetClosestObject();
        Emasu = EEast.GetComponent<PlayerCloseMass>().GetClosestObject();
        Smasu = ESouth.GetComponent<PlayerCloseMass>().GetClosestObject();

        CheckPositionMatch();

        if (!gameManager.isPlayerTurn)
        {
            // プレイヤーのターンでない場合の処理
        }

        if (((ms.x + 0.1f > this.transform.position.x && ms.x - 0.1f < this.transform.position.x) &&
            (ms.z + 0.1f > this.transform.position.z && ms.z - 0.1f < this.transform.position.z)))
        {
            Debug.Log("マスに乗った");
            rideMasu = true;
            animator.SetBool("isIdle", true); // Idleアニメーションを再生
            animator.SetBool("isRunning", false); // Runアニメーションを停止
        }
        else
        {
            rideMasu = false;
            animator.SetBool("isIdle", false); // Idleアニメーションを停止
            animator.SetBool("isRunning", true); // Runアニメーションを再生
        }
    }

    private void CheckPositionMatch()
    {
        GameObject[] masuObjects = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject masu in masuObjects)
        {
            Vector3 targetPos = masu.transform.position;
            Vector3 currentPos = transform.position;

            // XとZ座標がほぼ一致しているか判定
            if (Mathf.Abs(currentPos.x - targetPos.x) < threshold && Mathf.Abs(currentPos.z - targetPos.z) < threshold && Mathf.Abs(currentPos.y - targetPos.y) < 1f)
            {
                OnMatched(masu);
                break; // 一致するオブジェクトが見つかったらループを抜ける
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
        // ここに処理を追加（例：アニメーション再生、スコア加算など）
    }
}