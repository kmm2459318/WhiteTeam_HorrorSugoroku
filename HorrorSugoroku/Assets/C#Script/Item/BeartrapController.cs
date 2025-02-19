using UnityEngine;

public class BeartrapController : MonoBehaviour
{
    public GameObject beartrapPrefab; // トラばさみのPrefab
    public Transform spawnPoint; // トラばさみを生成する場所
    public EnemySaikoro enemySaikoro; // EnemySaikoroへの参照
    public int itemCount = 0; // アイテムの数

    public void AddItem()
    {
        itemCount++;
        Debug.Log("トラバサミが1つ増えました！現在の数: " + itemCount);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // タグがEnemyのオブジェクトとの接触をチェック
        {
            var enemy = other.GetComponent<EnemySaikoro>();
            if (enemy != null)
            {
                enemy.isTrapped = true; // �g���o�T�~�ɂ��������Ƃ��̏���
                Debug.Log("�G���g���o�T�~�ɂ��������I");

                // �􂢃Q�[�W��10����
                if (curseSlider != null)
                {
                    curseSlider.IncreaseDashPoint(10);
                }
            }
        }
    }

    // 🛠 ボタンを押すとトラバサミを設置
    public void PlaceBeartrap()
    {
        if (itemCount <= 0) // ❌ アイテムがない場合、設置しない
        {
            Debug.LogWarning("⚠ トラバサミのアイテムがありません！");
            return;
        }

        if (beartrapPrefab == null) // ❌ プレハブが設定されていない場合、生成しない
        {
            Debug.LogError("❌ Beartrapのプレハブが設定されていません！");
            return;
        }

        if (spawnPoint == null) // ❌ スポーンポイントが設定されていない場合、生成しない
        {
            Debug.LogError("❌ Beartrapのスポーンポイントが設定されていません！");
            return;
        }

        // ✅ トラバサミを生成
        Instantiate(beartrapPrefab, spawnPoint.position, Quaternion.identity);
        itemCount--; // 🛠 アイテムを消費
        Debug.Log("🪤 トラバサミを設置しました！ 残り: " + itemCount);
    }
}
