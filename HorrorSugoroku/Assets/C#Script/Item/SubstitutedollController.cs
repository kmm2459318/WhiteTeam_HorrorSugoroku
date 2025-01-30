using UnityEngine;
using UnityEngine.UI;

public class SubstitutedollController : MonoBehaviour
{
    // 身代わり人形の所持数
    private static int substituteDollCount = 3; // デバッグ用に3つ持たせる

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
            substituteDollCount--; // 所持数を減らす
            SceneChanger3D.hasSubstituteDoll = true; // 使用判定

            Debug.Log("身代わり人形を使用！ 残り: " + substituteDollCount);

            if (substituteDollCount <= 0)
            {
                Destroy(gameObject); // 0になったらボタンを削除
                Debug.Log("身代わり人形がなくなった！");
            }
        }
        else
        {
            Debug.Log("身代わり人形がありません！");
        }
    }
}
