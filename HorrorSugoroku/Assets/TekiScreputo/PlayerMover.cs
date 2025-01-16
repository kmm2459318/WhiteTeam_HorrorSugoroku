using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed = 5f;        // プレイヤーの移動速度
    private Vector3 targetPosition;    // 次の移動先
    private bool isMoving = false;     // 移動中フラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = transform.position; // 初期位置
    }

    // Update is called once per frame
    void Update()
    {
        // 移動中であれば移動処理を実行
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 移動が完了したら停止
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                Debug.Log("移動完了");
            }
        }

        // キー入力でテスト移動
        if (!isMoving && Input.GetKeyDown(KeyCode.Space)) // Spaceキーで移動
        {
            MoveToNextCell(Vector3.forward); // 仮に前方へ1マス移動
        }
    }
    public void MoveToNextCell(Vector3 direction)
    {
        if (!isMoving)
        {
            targetPosition = transform.position + direction; // 次の目標地点を設定
            isMoving = true; // 移動中フラグを設定
        }
    }
}
