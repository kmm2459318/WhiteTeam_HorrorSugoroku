using UnityEngine;
using UnityEngine.UI;

public class HeelCurseGage : MonoBehaviour
{
    [SerializeField] Image[] ImageGages; // ゲージ画像
    [SerializeField] Button resetButton; // ゲージをリセットするボタン
    [SerializeField] Slider DashGage;
    public CurseSlider curseslider;

    void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.RemoveAllListeners();
            resetButton.onClick.AddListener(ResetGages); // ボタンが押されたときにリセット
        }
        curseslider.dashPoint = 0;
    }


    // ゲージのリセット
    public void ResetGages()
    {
        curseslider.dashPoint = 0;
        foreach (Image img in ImageGages)
        {
            curseslider.dashPoint = 0;
            img.fillAmount = 0; // ゲージをリセット
            Debug.Log("ゲージがリセットされました");
        }
        
    }
}
