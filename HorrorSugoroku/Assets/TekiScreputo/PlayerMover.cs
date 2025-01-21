using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMover : MonoBehaviour
{
    public int remainingSteps = 10;   // 初期の歩数
    public float moveSpeed = 5f;     // 移動速度

    private bool isMoving = false;   // 移動中かどうか
    public delegate void OnStepsDepleted(); // 歩数が0になったときのイベント
    public event OnStepsDepleted StepsDepletedEvent; // イベントハンドラー
    public GridCell gridCell;
    void Start()
    {
        // 歩数が0になったときに発動するイベントを登録
        StepsDepletedEvent += OnStepsDepletedAction;
    }
    void Update()
    {
        // キーボード入力でプレイヤーを移動
        if (!isMoving && remainingSteps > 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                StartCoroutine(Move(Vector3.forward));
            if (Input.GetKey(KeyCode.DownArrow))
                StartCoroutine(Move(Vector3.back));
            if (Input.GetKey(KeyCode.LeftArrow))
                StartCoroutine(Move(Vector3.left));
            if (Input.GetKey(KeyCode.RightArrow))
                StartCoroutine(Move(Vector3.right));
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction;

        float time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }

        transform.position = targetPosition;

        remainingSteps--; // 歩数を減少
        Debug.Log($"残り歩数: {remainingSteps}");

        // 歩数が0になった場合にイベントを発動
        if (remainingSteps <= 0)
        {
            StepsDepletedEvent?.Invoke();
        }

        isMoving = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // タグが"マス"の場合に歩数を減らす
        if (other.CompareTag("masu"))
        {
            remainingSteps--;

            Debug.Log($"残り歩数: {remainingSteps}");

            // 歩数が0になったら処理を停止または終了
            if (remainingSteps <= 0)
            {
                Debug.Log("歩数が0になりました。");
                // 必要に応じて移動停止や他の処理を実行
            }
        }
    }

    // 歩数が0になったときのイベントアクション
    private void OnStepsDepletedAction()
    {
        Debug.Log("歩数が0になりました！イベントを発動します。");
        gridCell.ExecuteEvent();
    }
     
} // 必要なイベント処理をここに追加
    // 例: ゲームの終了、プレイヤーの停止など


