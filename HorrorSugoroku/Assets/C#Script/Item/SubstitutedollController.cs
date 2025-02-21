using UnityEngine;
using UnityEngine.UI;

public class SubstitutedollController : MonoBehaviour
{
    private static int substituteDollCount; // 身代わり人形の所持数
    public int itemCount = 0; // アイテムの数
    public Button substituteDollButton; // ボタンをアタッチ
    public CurseSlider curseSlider; // 呪いゲージの管理

    private void Start()
    {
        if (substituteDollButton == null)
        {
            Debug.LogError("❌ substituteDollButton がアタッチされていません！");
            return;
        }

        // 💡 ボタンの interactable を true にしておく
        substituteDollButton.interactable = true;

        substituteDollButton.onClick.AddListener(OnButtonPressed);
        UpdateButtonVisibility();
    }

    public void AddItem()
    {
        itemCount++;
        substituteDollCount++;
        Debug.Log("✅ 身代わり人形が1つ増えました！現在の数: " + itemCount);
        UpdateButtonVisibility();
    }

    private void OnButtonPressed()
    {
        Debug.Log("🖱 ボタンが押されました！ itemCount: " + itemCount);

        if (itemCount > 0)
        {
            substituteDollCount--;
            itemCount--; // 🛠 itemCount を減らす
            SceneChanger3D.hasSubstituteDoll = true;

            Debug.Log("✨ 身代わり人形を使用！ 残り: " + itemCount);

            // ✅ 呪いゲージを10増加
            if (curseSlider != null)
            {
                Debug.Log("🔮 生命反応あり3 - 呪いゲージ増加");
                curseSlider.IncreaseDashPoint(10);
            }

            UpdateButtonVisibility();
        }
        else
        {
            Debug.Log("⚠ 身代わり人形がありません！");
        }
    }

    private void UpdateButtonVisibility()
    {
        if (substituteDollButton != null)
        {
            bool isVisible = itemCount > 0;
            substituteDollButton.gameObject.SetActive(isVisible);
            Debug.Log("🖲 ボタンの表示状態を更新: " + isVisible);
        }
    }
}