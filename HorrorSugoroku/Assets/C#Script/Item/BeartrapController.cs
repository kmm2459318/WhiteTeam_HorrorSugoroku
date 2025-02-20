using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeartrapController : MonoBehaviour
{
    public GameObject beartrapPrefab; // トラバサミのPrefab
    public Transform spawnPoint; // トラバサミを生成する場所
    public EnemySaikoro enemySaikoro; // EnemySaikoroへの参照
    public int itemCount = 0; // アイテムの所持数
    public TMP_Text beartrapCountText; // UIに表示するテキスト

    private void Start()
    {
        // 初回のテキスト更新
        UpdateBeartrapCountText();
    }

    public void AddItem()
    {
        itemCount++;
        Debug.Log("トラバサミが1つ増えました！現在の数: " + itemCount);
        UpdateBeartrapCountText(); // UIを更新
    }

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

    // ボタンを押すとトラバサミを生成するメソッド
    public void PlaceBeartrap()
    {
        if (itemCount > 0)
        {
            // トラバサミのPrefabを生成
            Instantiate(beartrapPrefab, spawnPoint.position, Quaternion.identity);
            itemCount--; // 所持数を減らす
            Debug.Log("トラバサミを設置！ 残り: " + itemCount);
            UpdateBeartrapCountText(); // UIを更新
        }
        else
        {
            Debug.Log("トラバサミがありません！");
        }
    }

    private void UpdateBeartrapCountText()
    {
        if (beartrapCountText != null)
        {
            beartrapCountText.text = "トラバサミ: " + itemCount;
        }
        else
        {
            Debug.LogError("beartrapCountText が設定されていません！");
        }
    }
}
