using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SubstitutedollController : MonoBehaviour
{
    // 身代わり人形の所持数
    private static int substituteDollCount = 3; // デバッグ用に3つ持たせる
    public int itemCount = 0; // アイテムの数
    public TMP_Text dollCountText; // ボタンに表示するテキスト

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

        // 初回のテキスト更新
        UpdateDollCountText();
    }

    public void AddItem()
    {
        itemCount++;
        substituteDollCount++;
        Debug.Log("身代わり人形が1つ増えました！現在の数: " + substituteDollCount);
        UpdateDollCountText(); // テキスト更新
    }

    private void OnButtonPressed()
    {
        if (substituteDollCount > 0)
        {
            substituteDollCount--; // 所持数を減らす
            SceneChanger3D.hasSubstituteDoll = true; // 使用判定

            Debug.Log("身代わり人形を使用！ 残り: " + substituteDollCount);
            UpdateDollCountText(); // テキスト更新
        }
        else
        {
            Debug.Log("身代わり人形がありません！");
        }
    }

    private void UpdateDollCountText()
    {
        if (dollCountText != null)
        {
            dollCountText.text = "身代わり人形: " + substituteDollCount;
        }
        else
        {
            Debug.LogError("dollCountText が設定されていません！");
        }
    }
}
