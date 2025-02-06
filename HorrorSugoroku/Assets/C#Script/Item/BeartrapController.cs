using UnityEngine;

public class BeartrapController : MonoBehaviour
{
    public GameObject beartrapPrefab; // トラばさみのPrefab
    public Transform spawnPoint; // トラばさみを生成する場所
    public EnemySaikoro enemySaikoro; // EnemySaikoroへの参照

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // タグがEnemyのオブジェクトとの接触をチェック
        {
            // 反応した敵に対して処理を行う
            var enemy = other.GetComponent<EnemySaikoro>();
            if (enemy != null)
            {
                enemy.isTrapped = true; // トラバサミにかかったときの処理
                Debug.Log("敵がトラバサミにかかった！");
            }
        }
    }

    // ボタンを押すとトラばさみを生成するメソッド
    public void PlaceBeartrap()
    {
        // トラばさみのPrefabを生成
        Instantiate(beartrapPrefab, spawnPoint.position, Quaternion.identity);
    }
}
