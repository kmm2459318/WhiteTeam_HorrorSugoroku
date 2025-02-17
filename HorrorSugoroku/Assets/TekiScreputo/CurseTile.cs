using UnityEngine;

public class CurseTile : MonoBehaviour
{
    public int gridCellIncreaseAmount = 10; // GridCell 側の呪いゲージ増加量
    private CurseSlider curseSlider;

    private void Start()
    {
        // CurseSlider をシーン内から探す
        curseSlider = FindObjectOfType<CurseSlider>();

        if (curseSlider == null)
        {
            Debug.LogError("CurseSlider が見つかりません！");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("触れたよ");
        if (other.CompareTag("Player")) // プレイヤーが触れたとき

        {
            Debug.Log("触れたよ");
            IncreaseCurse(10); // 呪いゲージを10増やす
        }
    }

    public void IncreaseCurse(int amount)
    {
        if (curseSlider == null)
        {
            curseSlider = FindObjectOfType<CurseSlider>();

            if (curseSlider == null)
            {
                Debug.LogError("CurseSlider が見つかりません！シーンに配置されていますか？");
            }
        }
    }
}
