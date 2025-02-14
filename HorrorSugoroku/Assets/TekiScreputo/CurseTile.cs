using UnityEngine;

public class CurseTile : MonoBehaviour
{
    public int gridCellIncreaseAmount = 20; // GridCell 側の呪いゲージ増加量
    public CurseSlider curseSlider;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CurseSlider をシーン内から取得
        curseSlider = FindObjectOfType<CurseSlider>();

        if (curseSlider == null)
        {
            Debug.LogError("CurseSlider がシーンに見つかりません！");
        }
    }
    public void CurEs(int increaseAmount)
    {
        Debug.Log($"呪いゲージが {increaseAmount} 増えました！");

        // curseSlider を通じて IncreaseDashPoint() を呼ぶ
        if (curseSlider != null)
        {
            curseSlider.IncreaseDashPoint(increaseAmount);
        }
        else
        {
            Debug.LogError("curseSlider が設定されていません！");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
