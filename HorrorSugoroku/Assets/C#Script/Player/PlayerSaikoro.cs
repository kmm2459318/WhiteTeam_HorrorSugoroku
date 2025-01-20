using UnityEngine;

public class PlayerSaikoro : MonoBehaviour
{
    private EnemySaikoro targetScript; // コマンドを受け取るEnemySaikoro
    private int sai = 1; // ランダムなサイコロの値
    private bool saikorotyu = false; // サイコロを振っているか
    private float delta = 0; // 時間の計測
    private int ii = 0; // 繰り返し回数

    [System.Obsolete]
    void Start()
    {
        // プレイヤーシーンがロードされる際に、EnemySaikoroを探して参照を保持
        targetScript = FindObjectOfType<EnemySaikoro>();

        // Enemyがシーンに存在しない場合、エラーメッセージを出力
        if (targetScript == null)
        {
            Debug.LogError("EnemySaikoro not found in the scene.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || saikorotyu)
        {
            saikorotyu = true;
            this.delta += Time.deltaTime;

            if (this.delta > 0.1f)
            {
                this.delta = 0f;

                if (ii < 7)
                {
                    sai = Random.Range(1, 7);
                    //Debug.Log("Player rolling: " + sai);
                    ii++;
                }
                else
                {
                    Debug.Log("Player rolled: " + sai);

                    // プレイヤーのサイコロの結果に応じてEnemyのサイコロ範囲を決定
                    if (sai <= 3)
                    {
                        // プレイヤーが1〜3を出した場合、Enemyは4〜6を出す
                        targetScript.RollEnemyDice(4, 6);
                    }
                    else
                    {
                        // プレイヤーが4〜6を出した場合、Enemyは1〜3を出す
                        targetScript.RollEnemyDice(1, 3);
                    }

                    ii = 0;
                    saikorotyu = false;
                }
            }
        }
    }
}