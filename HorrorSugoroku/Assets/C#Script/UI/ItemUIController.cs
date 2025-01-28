using UnityEngine;
using UnityEngine.UI;

public class ItemUIController : MonoBehaviour
{
    public GameObject itemObject; // ゲームオブジェクト
    public GameObject backObjectUI; // ゲームオブジェクト
    public Button itemButton; // ボタン
    public Button backButton; // ボタン

    void Start()
    {
        // ボタンのクリックイベントにメソッドを登録
        itemButton.onClick.AddListener(ItemObject);
        backButton.onClick.AddListener(BackObject);
        itemObject.SetActive(false);
        backObjectUI.SetActive(false);
    }

    void ItemObject()
    {
            itemObject.SetActive(true);
            backObjectUI.SetActive(true);
    }

    void BackObject()
    {
            itemObject.SetActive(false);
            backObjectUI.SetActive(false);
    }
}
