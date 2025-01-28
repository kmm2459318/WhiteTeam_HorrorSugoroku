using UnityEngine;
using UnityEngine.UI;

public class BeartrapController : MonoBehaviour
{
    private void Start()
    {
        // Buttonコンポーネントを取得
        Button button = GetComponent<Button>();

        // Buttonコンポーネントが存在するか確認
        if (button != null)
        {
            // ボタンが押されたときにOnButtonPressedメソッドを呼び出す
            button.onClick.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("Buttonコンポーネントがアタッチされていません！");
        }
    }

    // ボタンが押されたときに呼び出されるメソッド
    private void OnButtonPressed()
    {
        Debug.Log(gameObject.name + " がクリックされました！");
    }
}
