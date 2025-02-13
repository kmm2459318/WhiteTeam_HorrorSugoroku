using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator; // アニメーターコンポーネント
    private bool isMoving; // 移動中かどうかを示すフラグ
    public GameManager gameManager; // ゲームマネージャーへの参照
    public EnemySaikoro enemySaikoro; // 敵キャラクターのサイコロへの参照
    int mp = 0; // マップピースの数
    float mo = 3f; // 移動速度
    int id = 1; // 敵キャラクターのID

    void Start()
    {
        animator = GetComponent<Animator>(); // アニメーターコンポーネントの取得
    }

    void Update()
    {
        if (isMoving)
        {
            animator.SetBool("isRunning", true); // 移動中の場合、アニメーションを走る状態に設定
        }
        else
        {
            animator.SetBool("isRunning", false); // 移動していない場合、アニメーションを停止状態に設定
        }

        mp = gameManager.mapPiece; // ゲームマネージャーからマップピースの数を取得

        if (mp < 3) // １段階
        {
            mo = 6f;
            id = 1;
        }
        else if (mp < 6) // ２段階
        {
            mo = 8f;
            id = 2;
            enemySaikoro.skill1 = true; // スキル1を有効にする
        }
        else if (mp < 9) // ３段階
        {
            mo = 10f;
            id = 3;
            enemySaikoro.skill2 = true; // スキル2を有効にする
        }
        else // 目が進化
        {
            mo = 12f;
            id = 4;
        }

        enemySaikoro.mokushi = mo; // サイコロの移動速度を設定
        enemySaikoro.idoukagen = id; // サイコロのIDを設定
    }

    public void SetMovement(bool moving)
    {
        isMoving = moving; // 移動中かどうかを設定
        Debug.Log("SetMovement called with: " + moving); // デバッグログに移動状態を出力
    }
}