using UnityEngine;

public class EnemyStop : MonoBehaviour
{
    public GameManager gameManager;
    public string targetTag = "masu";  // 対象のタグ
    public float threshold = 0.1f; // どれくらいの誤差を許容するか
    public bool rideMasu = false;

    void Start()
    {
        
    }

    void Update()
    {
        CheckPositionMatch();

        if (!gameManager.isPlayerTurn)
        {

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
