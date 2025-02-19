using UnityEngine;
using UnityEngine.UI;

public class SubstitutedollController : MonoBehaviour
{
    // 身代わり人形の所持数
    private static int substituteDollCount ; // デバッグ用に3つ持たせる
    public int itemCount = 0; // アイテムの数
    public Button substituteDollButton; // ボタンをアタッチ

    private void Start()
    {
        if (substituteDollButton == null)
        {
            Debug.LogError("substituteDollButton がアタッチされていません！");
            return;
        }

        // ボタンのリスナーを設定
        substituteDollButton.onClick.AddListener(OnButtonPressed);

        // 初期状態でボタンの表示/非表示を更新
        UpdateButtonVisibility();
    }
    public void AddItem()
    {
        itemCount++;
        substituteDollCount++;
        Debug.Log("身代わり人形が1つ増えました！現在の数: " + itemCount);
    }
    private void OnButtonPressed()
    {
        if( itemCount > 0)
        {
            substituteDollCount--;  // 所持数を減らす
            SceneChanger3D.hasSubstituteDoll = true; // 使用判定

            Debug.Log("身代わり人形を使用！ 残り: " + itemCount);

            // ボタンの表示を更新
            UpdateButtonVisibility();
        }
        else
        {
            Debug.Log("身代わり人形がありません！");
        }
    }

    private void UpdateButtonVisibility()
    {
        if (substituteDollButton != null)
        {
            substituteDollButton.gameObject.SetActive(itemCount > 0);
        }
    }
    //private void Start()
    //{
    //    Button button = GetComponent<Button>();
    //    if (button != null)
    //    {
    //        button.onClick.AddListener(OnButtonPressed);
    //    }
    //    else
    //    {
    //        Debug.LogError("Buttonコンポーネントがアタッチされていません！");
    //    }
    //}

    //private void OnButtonPressed()
    //{
    //    if (substituteDollCount > 0)
    //    {
    //        substituteDollCount--; // 所持数を減らす
    //        SceneChanger3D.hasSubstituteDoll = true; // 使用判定

    //        Debug.Log("身代わり人形を使用！ 残り: " + substituteDollCount);
    //        UpdateButtonVisibility();
    //        if (substituteDollCount <= 0)
    //        {
    //            Destroy(gameObject); // 0になったらボタンを削除
    //            Debug.Log("身代わり人形がなくなった！");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("身代わり人形がありません！");
    //    }

    //}
    //private void UpdateButtonVisibility()
    //{
    //    if (substituteDollButton != null)
    //    {
    //        substituteDollButton.gameObject.SetActive(substituteDollCount > 0);
    //    }
    //}
}
