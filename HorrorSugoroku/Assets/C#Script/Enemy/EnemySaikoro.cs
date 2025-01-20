using UnityEngine;

public class EnemySaikoro : MonoBehaviour
{
    int min;
    int max;
    void Start()
    {
        // シーンを跨いでオブジェクトを保持するため、Destroyしない
        DontDestroyOnLoad(gameObject);
    }

    public void RollEnemyDice(int sai)
    {
        // プレイヤーのサイコロの結果に応じてEnemyのサイコロ範囲を決定
        if (sai <= 3)
        {
            // プレイヤーが1〜3を出した場合、Enemyは1〜3を出す
            min = 1;
            max = 3;
        }
        else
        {
            // プレイヤーが4〜6を出した場合、Enemyは4〜6を出す
            min = 4;
            max = 6;
        }
        // Enemyのサイコロを振る範囲を指定して振る
        int enemyRoll = Random.Range(min, max + 1);
        Debug.Log("Enemy rolled: " + enemyRoll);

        // サイコロの値に応じた処理
        if (enemyRoll == 6)
        {
            Debug.Log("Enemy is lucky! They rolled a 6!");
        }
        else if (enemyRoll <= 3)
        {
            Debug.Log("Enemy rolled a low number: " + enemyRoll + ". They are cautious.");
        }
        else
        {
            Debug.Log("Enemy rolled: " + enemyRoll + ". They proceed with caution.");
        }
    }
}