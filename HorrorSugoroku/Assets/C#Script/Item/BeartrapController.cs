using UnityEngine;

public class BeartrapController : MonoBehaviour
{
    public GameObject beartrapPrefab; // トラばさみのPrefab
    public Transform spawnPoint; // トラばさみを生成する場所
    public EnemySaikoro enemySaikoro; // EnemySaikoroへの参照
    public CurseSlider curseSlider; // 呪いゲージの管理

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // タグがEnemyのオブジェクトとの接触をチェック
        {
            var enemy = other.GetComponent<EnemySaikoro>();
            if (enemy != null)
            {
                enemy.isTrapped = true; // トラバサミにかかったときの処理
                Debug.Log("敵がトラバサミにかかった！");

                // 呪いゲージを10増加
                if (curseSlider != null)
                {
                    curseSlider.IncreaseDashPoint(10);
                }
            }
        }
    }

    public void PlaceBeartrap()
    {
        Instantiate(beartrapPrefab, spawnPoint.position, Quaternion.identity);
    }
}
