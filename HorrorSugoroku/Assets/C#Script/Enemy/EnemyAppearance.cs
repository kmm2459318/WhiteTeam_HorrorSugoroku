using System.Collections;
using UnityEngine;

public class EnemyAppearance : MonoBehaviour
{
    public GameObject enemyModel; // エネミーのモデル
    public float displayDuration = 2f; // エネミーが表示される時間（秒）

    void Start()
    {
        if (enemyModel == null)
        {
            Debug.LogError("エネミーモデルが設定されていません");
        }
        else
        {
            enemyModel.SetActive(true); // 最初から表示する
            Debug.Log("エネミーが初期状態で表示されました");
        }
    }

    public void HideEnemyAfterDelay()
    {
        Debug.Log("aaa");
        if (enemyModel != null)
        {
            StartCoroutine(HideEnemyCoroutine(displayDuration)); // 一定時間後に非表示にするコルーチンを開始
        }
    }

    private IEnumerator HideEnemyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定された時間待つ
        if (enemyModel != null)
        {
            enemyModel.SetActive(false); // エネミーを非表示にする
            Debug.Log("エネミーが非表示になりました");
        }
    }
}
