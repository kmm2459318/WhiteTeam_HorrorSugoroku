using UnityEngine;
using UnityEngine.UI;

public class BeartrapController : MonoBehaviour
{
    public GameObject beartrapPrefab; // トラバサミのPrefab
    public Transform spawnPoint; // トラバサミを生成する場所
    public EnemySaikoro enemySaikoro; // EnemySaikoroへの参照
    public CurseSlider curseSlider; // 呪いゲージの管理
    public Button beartrapButton; // ボタンをアタッチ
    private int itemCount = 30; // アイテムの数

    private void Start()
    {
        if (beartrapButton == null)
        {
            Debug.LogError("❌ beartrapButton がアタッチされていません！");
            return;
        }

        beartrapButton.interactable = true;
        beartrapButton.onClick.AddListener(OnButtonPressed);
        UpdateButtonVisibility();
    }

    public void AddItem()
    {
        itemCount++;
        Debug.Log("✅ トラバサミが1つ増えました！現在の数: " + itemCount);
        UpdateButtonVisibility();
    }

    private void OnButtonPressed()
    {
        Debug.Log("🖱 トラバサミボタンが押されました！");

        // ✅ 呪いゲージを10増加
        if (curseSlider != null)
        {
            curseSlider.IncreaseDashPoint(10);
            Debug.Log("🔮 呪いゲージが10増加！");
        }
        else
        {
            Debug.LogError("❌ curseSlider が設定されていません！");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemySaikoro>();
            if (enemy != null)
            {
                enemy.isTrapped = true;
                Debug.Log("🪤 敵がトラバサミにかかった！");

                if (curseSlider != null)
                {
                    curseSlider.IncreaseDashPoint(10);
                    Debug.Log("🔮 呪いゲージが10増加！");
                }
            }
        }
    }

    public void PlaceBeartrap()
    {
        if (itemCount <= 0)
        {
            Debug.LogWarning("⚠ トラバサミのアイテムがありません！");
            return;
        }
        if (beartrapPrefab == null)
        {
            Debug.LogError("❌ Beartrapのプレハブが設定されていません！");
            return;
        }
        if (spawnPoint == null)
        {
            Debug.LogError("❌ Beartrapのスポーンポイントが設定されていません！");
            return;
        }

        Instantiate(beartrapPrefab, spawnPoint.position, Quaternion.identity);
        itemCount--;
        Debug.Log("🪤 トラバサミを設置しました！ 残り: " + itemCount);
        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
        if (beartrapButton != null)
        {
            bool isVisible = itemCount > 0;
            beartrapButton.gameObject.SetActive(isVisible);
            Debug.Log("🖲 ボタンの表示状態を更新: " + isVisible);
        }
    }
}