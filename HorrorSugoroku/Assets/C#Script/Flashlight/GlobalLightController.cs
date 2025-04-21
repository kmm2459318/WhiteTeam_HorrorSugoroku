using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GlobalLightController : MonoBehaviour
{
    [SerializeField] private string alwaysBrightTag = "AlwaysBright"; // 常に明るいライトのタグ
    [SerializeField] private float maxIntensity = 2f;
    [SerializeField] private float transitionDuration = 1f; // 明るさが変わるまでの時間
    [SerializeField] private GameObject[] outlinedObjects; // Outline を持つオブジェクトの配列
    [SerializeField] private Animator targetAnimator; // アニメーション制御用
    [SerializeField] private string triggerName = "On"; // アニメーションのトリガー名

    private Light[] allLights; // シーン内の全ライト
    private List<Light> alwaysBrightLights = new List<Light>(); // 常に明るいライト
    private bool areOtherLightsOn = false; // その他のライトの状態
    private bool isPlayerInZone = false; // プレイヤーがエリア内にいるかどうか

    private void Start()
    {
        // シーン内の全ライトを取得
        allLights = FindObjectsOfType<Light>();

        // タグで常に明るいライトを取得
        alwaysBrightLights = allLights.Where(light => light.CompareTag(alwaysBrightTag)).ToList();

        // タグ付きライトを明るく設定（変更せず、明るさを維持）
        foreach (Light light in alwaysBrightLights)
        {
            light.enabled = true;
            light.intensity = maxIntensity;
            Debug.Log($"ライト '{light.name}' はタグ '{alwaysBrightTag}' に基づき常に明るい状態に設定されました");
        }
    }

    public void SetPlayerInZone(bool state)
    {
        isPlayerInZone = state;
        Debug.Log($"プレイヤーがエリア内: {isPlayerInZone}");
    }

    private void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aキーが押されました！ その他のライトの状態を切り替えます。");
            ToggleOtherLights();
        }
    }

    private void ToggleOtherLights()
    {
        areOtherLightsOn = !areOtherLightsOn; // 状態を反転
        Debug.Log($"その他のライトの状態変更: {areOtherLightsOn}");

        // 常に明るいライト以外を対象に明るさを切り替える
        foreach (Light light in allLights)
        {
            if (!alwaysBrightLights.Contains(light)) // 常に明るいライトを除外
            {
                StartCoroutine(AdjustLightIntensity(light, areOtherLightsOn ? maxIntensity : light.intensity));
            }
        }

        // 明るくなったらアウトラインを無効化
        if (areOtherLightsOn && outlinedObjects.Length > 0)
        {
            foreach (GameObject obj in outlinedObjects)
            {
                Outline outlineComponent = obj.GetComponent<Outline>();
                if (outlineComponent != null)
                {
                    outlineComponent.enabled = false;
                    Debug.Log($"'{obj.name}' のアウトラインを非表示にしました");
                }
            }
        }

        // 明るくなったらアニメーションのトリガーをオンにする
        if (areOtherLightsOn && targetAnimator != null)
        {
            targetAnimator.SetTrigger(triggerName);
            Debug.Log($"アニメーションのトリガー '{triggerName}' をオンにしました");
        }
    }

    private IEnumerator AdjustLightIntensity(Light light, float targetIntensity)
    {
        float currentIntensity = light.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            light.intensity = Mathf.Lerp(currentIntensity, targetIntensity, elapsedTime / transitionDuration);
            yield return null; // 次のフレームを待機
        }

        light.intensity = targetIntensity; // 最終的な強度を設定
        Debug.Log($"ライト '{light.name}' の明るさを {targetIntensity} に設定しました");
    }
}