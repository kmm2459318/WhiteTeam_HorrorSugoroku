using UnityEngine;
using TMPro; // TextMeshPro を使用するための名前空間

public class GlobalLightController : MonoBehaviour
{
    [SerializeField] private bool turnOn = false; // インスペクターでライトのオン・オフを設定
    [SerializeField] private Light[] alwaysOnLights; // 常にオンにしたいライト
    [SerializeField] private GameObject outlineObject; // Outline.cs がアタッチされたオブジェクト
    [SerializeField] private Color outlineColor = Color.red; // アウトラインの色をインスペクターで設定
    [SerializeField, Range(0f, 10f)] private float outlineWidth = 2f; // アウトラインの幅
    [SerializeField] private TextMeshProUGUI messageText; // テキスト表示用の TextMeshPro コンポーネント

    private Outline outline; // Outline コンポーネントをキャッシュ

    public void ApplyLightState()
    {
        Light[] allLights = FindObjectsOfType<Light>(); // シーン内のすべてのライトを取得
        foreach (Light light in allLights)
        {
            if (light != null && !IsAlwaysOn(light))
            {
                light.enabled = turnOn; // ライトのオン・オフを設定
            }
        }

        // 常にオンのライトは維持
        foreach (Light alwaysOnLight in alwaysOnLights)
        {
            if (alwaysOnLight != null)
            {
                alwaysOnLight.enabled = true;
            }
        }

        // ライトの状態に応じてアウトラインとテキストを切り替え
        if (outline != null)
        {
            if (!turnOn) // ライトがオフのとき
            {
                EnableOutline();
                ShowMessage("Go MachineRoom");
            }
            else // ライトがオンのとき
            {
                DisableOutline();
                ShowMessage(""); // テキスト非表示
            }
        }
    }

    private bool IsAlwaysOn(Light light)
    {
        foreach (Light alwaysOnLight in alwaysOnLights)
        {
            if (alwaysOnLight == light)
            {
                return true; // 常にオンのライトリストに含まれる場合
            }
        }
        return false; // 含まれない場合
    }

    private void Start()
    {
        if (outlineObject != null)
        {
            // Outline コンポーネントを取得
            outline = outlineObject.GetComponent<Outline>();
            if (outline == null)
            {
                Debug.LogError("Outline コンポーネントが指定されたオブジェクトに見つかりませんでした。");
                return;
            }

            // 初期設定
            outline.OutlineColor = outlineColor;
            outline.OutlineWidth = outlineWidth;
            outline.enabled = !turnOn; // ライトの状態に応じてアウトラインを初期化
        }

        ApplyLightState(); // スタート時に適用
    }

    private void EnableOutline()
    {
        outline.OutlineColor = outlineColor; // アウトラインの色を設定
        outline.OutlineWidth = outlineWidth; // アウトラインの幅を設定
        outline.enabled = true; // アウトラインを有効化
    }

    private void DisableOutline()
    {
        outline.enabled = false; // アウトラインを無効化
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message; // 指定されたテキストを表示
        }
    }
}