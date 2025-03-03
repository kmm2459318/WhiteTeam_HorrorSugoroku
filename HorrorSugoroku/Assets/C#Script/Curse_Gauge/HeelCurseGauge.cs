using UnityEngine;
using UnityEngine.UI;

public class HeelCurseGage : MonoBehaviour
{
    [SerializeField] Image[] ImageGages; // ゲージ画像
    [SerializeField] Button resetButton; // ボタン
    [SerializeField] Slider DashGage;
    public CurseSlider curseslider;

    void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.RemoveAllListeners();
            resetButton.onClick.AddListener(UpdateGages); // ボタンが押されたときにゲージ更新
        }
        curseslider.dashPoint = 0;
    }

    // ゲージの更新
    public void UpdateGages()
    {
        if (curseslider.dashPoint <= 20)
        {
            curseslider.dashPoint = 0;
        }
        else if (curseslider.dashPoint <= 40)
        {
            curseslider.dashPoint = 20;
        }
        else if (curseslider.dashPoint <= 60)
        {
            curseslider.dashPoint = 40;
        }
        else if (curseslider.dashPoint <= 80)
        {
            curseslider.dashPoint = 60;
        }
        else if (curseslider.dashPoint <= 100)
        {
            curseslider.dashPoint = 80;
        }

        foreach (Image img in ImageGages)
        {
            img.fillAmount = (float)curseslider.dashPoint / 100f; // ゲージの更新
        }

        Debug.Log("ゲージが更新されました: " + curseslider.dashPoint);
    }
}
