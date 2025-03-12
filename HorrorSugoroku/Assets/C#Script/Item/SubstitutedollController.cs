using TMPro;
using UnityEngine;

public class SubstitutedollController : MonoBehaviour
{
    public int itemCount = 3; // 初期値を3に設定
    private int useCount = 0; // 使用回数
    private const int maxUsage = 3; // 使用上限
    public CurseSlider curseSlider; // 呪いゲージの管理

    public TextMeshProUGUI Possessions;

    private void Start()
    {
        Possessions.text = itemCount.ToString();
        Debug.Log($"🎭 身代わり人形を{itemCount}つ持っています！");
    }

    public void UseSubstituteDoll()
    {
        if (useCount < maxUsage)
        {
            useCount++;
            itemCount--;
            SceneChanger3D.hasSubstituteDoll = true;

            Debug.Log($"✨ 身代わり人形を使用！ 残り: {itemCount} 使用回数: {useCount}/{maxUsage}");

            // ✅ 呪いゲージを10増加
            if (curseSlider != null)
            {
                Debug.Log("🔮 生命反応あり3 - 呪いゲージ増加");
                curseSlider.IncreaseDashPoint(10);
            }
        }
        else
        {
            Debug.Log("⚠ 身代わり人形の使用上限に達しました！");
        }
    }
    public void AddItem()
    {
        itemCount++;
        Debug.Log("身代わり人形が1つ増えました！現在の数: " + itemCount);
        Possessions.text = itemCount.ToString();
        UseSubstituteDoll();
    }
}
