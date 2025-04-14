using TMPro;
using UnityEngine;

public class SubstitutedollController : MonoBehaviour
{
   //public int itemCount = useCount; // 初期値を3に設定
    public int useCount = 0; // 使用回数
    public PlayerInventory playerInventory; // インベントリ参照
    private const int maxUsage = 3; // 使用上限
    public CurseSlider curseSlider; // 呪いゲージの管理
    
    public TextMeshProUGUI Possessions;

    private void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }
        UpdateItemCountUI();
        Debug.Log($"🎭 所持している身代わり人形: {playerInventory.GetItemCount("身代わり人形")}");
        //Possessions.text = itemCount.ToString();
        //Debug.Log($"🎭 身代わり人形を{itemCount}つ持っています！");
    }

    public void UseSubstituteDoll()
    {
        int count = playerInventory.GetItemCount("身代わり人形");

        if (count > 0 && useCount < maxUsage)
        {
            bool used = playerInventory.UseItem("身代わり人形");
            if (used)
            {
                useCount++;
                SceneChanger3D.hasSubstituteDoll = true;

                if (curseSlider != null)
                {
                    curseSlider.IncreaseDashPoint(10);
                    Debug.Log("🔮 呪いゲージが10増加しました！");
                }

                Debug.Log($"✨ 身代わり人形を使用。残り: {playerInventory.GetItemCount("身代わり人形")}");
                UpdateItemCountUI();
            }
        }
        else
        {
            Debug.Log("⚠ 身代わり人形がない、または使用上限です！");
        }
    }

    public void AddItem()
    {
        useCount++;
        Debug.Log("身代わり人形が1つ増えました！現在の数: " + useCount);
        Possessions.text = useCount.ToString();
        UseSubstituteDoll();
        playerInventory.AddItem("身代わり人形");
        Debug.Log("👻 身代わり人形を1つ追加！");
        UpdateItemCountUI();
    }

    private void UpdateItemCountUI()
    {
        int currentCount = playerInventory.GetItemCount("身代わり人形");
        Possessions.text = currentCount.ToString();
    }
    //    if (useCount < maxUsage)
    //    {
    //        useCount++;
    //        itemCount--;
    //        SceneChanger3D.hasSubstituteDoll = true;

    //        Debug.Log($"✨ 身代わり人形を使用！ 残り: {itemCount} 使用回数: {useCount}/{maxUsage}");

    //        // ✅ 呪いゲージを10増加
    //        if (curseSlider != null)
    //        {
    //            Debug.Log("🔮 生命反応あり3 - 呪いゲージ増加");
    //            curseSlider.IncreaseDashPoint(10);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("⚠ 身代わり人形の使用上限に達しました！");
    //    }
    //}
    //public void AddItem()
    //{
    //    itemCount++;
    //    Debug.Log("身代わり人形が1つ増えました！現在の数: " + itemCount);
    //    Possessions.text = itemCount.ToString();
    //    UseSubstituteDoll();
    //}
}
