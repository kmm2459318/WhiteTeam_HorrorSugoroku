using UnityEngine;
using UnityEngine.UI;

public class HeelCurseGage : MonoBehaviour
{
    [SerializeField] private Image[] ImageGages; // ゲージ画像
    [SerializeField] private Button resetButton; // ゲージをリセットするボタン

    void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.RemoveAllListeners();
            resetButton.onClick.AddListener(ResetGages); // ボタンが押されたときにリセット
        }
    }

   
    // ゲージのリセット
    public void ResetGages()
    {
        foreach (Image img in ImageGages)
        {
            img.fillAmount = 0; // ゲージをリセット
        }
        Debug.Log("ゲージがリセットされました");
    }
}
