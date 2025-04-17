using UnityEngine;
using TMPro; // TextMeshPro を使用するための名前空間
using System.Collections; // IEnumerator を使用

public class GlobalLightController : MonoBehaviour
{
    [SerializeField] private bool turnOn = false; // ライトの初期状態はオフ
    [SerializeField] private Light[] alwaysOnLights; // 常にオンにしたいライト
    [SerializeField] private GameObject[] outlineObjects; // 複数のOutlineオブジェクト
    [SerializeField] private Color outlineColor = Color.red; // アウトラインの色
    [SerializeField, Range(0f, 10f)] private float outlineWidth = 2f; // アウトラインの幅
    [SerializeField] private TextMeshProUGUI messageText; // TextMeshPro でテキスト表示
    [SerializeField] private Animator objectAnimator; // アニメーターを追加
    [SerializeField] private float fadeDuration = 2f; // ライトのフェードイン時間
    [SerializeField] private float maxIntensity = 2f; // ライトの最大輝度

    private Outline[] outlines; // Outline コンポーネントの配列

    void Update()
    {
        // Aキーが押されたらライトをオンにし、アニメーションのTriggerを発火
        if (Input.GetKeyDown(KeyCode.A))
        {
            turnOn = true;
            StartCoroutine(FadeInLights()); // コルーチンを開始
            ApplyLightState();

            if (objectAnimator != null)
            {
                objectAnimator.SetTrigger("On");
            }
        }
    }

    IEnumerator FadeInLights()
    {
        Light[] allLights = FindObjectsOfType<Light>();
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float intensity = Mathf.Lerp(0, maxIntensity, timer / fadeDuration);

            foreach (Light light in allLights)
            {
                if (light != null && !IsAlwaysOn(light))
                {
                    light.intensity = intensity;
                }
            }

            yield return null; // 次のフレームまで待機
        }
    }

    public void ApplyLightState()
    {
        Light[] allLights = FindObjectsOfType<Light>();
        foreach (Light light in allLights)
        {
            if (light != null && !IsAlwaysOn(light))
            {
                light.enabled = turnOn;
                if (!turnOn) light.intensity = 0; // ライトがオフのときは輝度を0に
            }
        }

        foreach (Light alwaysOnLight in alwaysOnLights)
        {
            if (alwaysOnLight != null)
            {
                alwaysOnLight.enabled = true;
            }
        }

        // 複数のアウトラインオブジェクトに対応
        if (outlines != null)
        {
            foreach (var outline in outlines)
            {
                if (outline != null)
                {
                    outline.enabled = !turnOn; // ライトがオフならアウトラインを表示
                    if (!turnOn) // オフのときのみ色と幅を設定
                    {
                        outline.OutlineColor = outlineColor;
                        outline.OutlineWidth = outlineWidth;
                    }
                }
            }
        }

        ShowMessage(turnOn ? "" : "Go MachineRoom");
    }

    private bool IsAlwaysOn(Light light)
    {
        foreach (Light alwaysOnLight in alwaysOnLights)
        {
            if (alwaysOnLight == light)
            {
                return true;
            }
        }
        return false;
    }

    private void Start()
    {
        if (outlineObjects != null && outlineObjects.Length > 0)
        {
            outlines = new Outline[outlineObjects.Length];
            for (int i = 0; i < outlineObjects.Length; i++)
            {
                if (outlineObjects[i] != null)
                {
                    outlines[i] = outlineObjects[i].GetComponent<Outline>();
                    if (outlines[i] == null)
                    {
                        Debug.LogError($"Outline コンポーネントがオブジェクト {outlineObjects[i].name} に見つかりませんでした。");
                    }
                }
            }
        }

        ApplyLightState();
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }
}