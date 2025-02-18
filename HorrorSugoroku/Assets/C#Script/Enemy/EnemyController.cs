using UnityEngine;
using System.Collections; // 追加

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

        if (enemySaikoro == null)
        {
            Debug.LogError("enemySaikoro is not assigned.");
        }

        // AttackRoutineコルーチンを開始
        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
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
        if (animator != null)
        {
            animator.SetBool("is Running", moving); // 移動中の場合、アニメーションを走る状態に設定
        }
        Debug.Log("SetMovement called with: " + moving); // デバッグログに移動状態を出力
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(120f); // 2分待機
            if (animator != null && !animator.GetBool("is Running")) // is Runningがfalseの場合のみAttackを発動
            {
                animator.SetBool("Attack", true); // Attackアニメーションを開始
                yield return new WaitForSeconds(1f); // アタックモーションの再生時間を待機（適宜調整）
                animator.SetBool("Attack", false); // Attackアニメーションを終了
                animator.SetBool("is Running", false); // Idle状態に戻す
            }
        }
    }
}