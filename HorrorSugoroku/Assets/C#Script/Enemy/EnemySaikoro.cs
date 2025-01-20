using UnityEngine;

public class EnemySaikoro : MonoBehaviour
{
    void Start()
    {
        // シーンを跨いでオブジェクトを保持するため、Destroyしない
        DontDestroyOnLoad(gameObject);
    }

    public void RollEnemyDice(int min, int max)
    {
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