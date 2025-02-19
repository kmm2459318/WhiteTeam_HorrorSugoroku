using UnityEngine;
using UnityEngine.UI;

public class SubstitutedollController : MonoBehaviour
{
    private static int substituteDollCount = 3; // デバッグ用に3つ持たせる
    public CurseSlider curseSlider; // 呪いゲージの管理

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("Buttonコンポーネントがアタッチされていません！");
        }
    }

    private void OnButtonPressed()
    {
        if (substituteDollCount > 0)
        {
            substituteDollCount--;
            SceneChanger3D.hasSubstituteDoll = true;

            Debug.Log("身代わり人形を使用！ 残り: " + substituteDollCount);

            // 呪いゲージを10増加
            if (curseSlider != null)
            {
                curseSlider.IncreaseDashPoint(10);
            }

            if (substituteDollCount <= 0)
            {
                Destroy(gameObject);
                Debug.Log("身代わり人形がなくなった！");
            }
        }
        else
        {
            Debug.Log("身代わり人形がありません！");
        }
    }
}
