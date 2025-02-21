using UnityEngine;
using System.Collections;

public class EyeEffectController : MonoBehaviour
{
    [SerializeField] private GameObject blackOverlay; // 黒いオーバーレイのゲームオブジェクト
    [SerializeField] private Canvas parentCanvas; // 親Canvasの参照
    private CanvasGroup canvasGroup;

    void Start()
    {
        if (blackOverlay != null)
        {
            canvasGroup = blackOverlay.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = blackOverlay.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0; // 初期状態では透明
            blackOverlay.SetActive(false); // 初期状態では非表示
        }
        else
        {
            Debug.LogError("blackOverlayが設定されていません");
        }
    }

    public void ApplyEyeEffect()
    {
        if (blackOverlay != null && parentCanvas != null)
        {
            parentCanvas.gameObject.SetActive(true); // 親Canvasをアクティブにする
            blackOverlay.SetActive(true); // 黒いオーバーレイを表示

            // canvasGroupが設定されていない場合は再度取得
            if (canvasGroup == null)
            {
                canvasGroup = blackOverlay.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = blackOverlay.AddComponent<CanvasGroup>();
                }
            }

            StartCoroutine(FadeIn());
        }
        else
        {
            Debug.LogError("blackOverlayまたはparentCanvasが設定されていません");
        }
    }

    private IEnumerator FadeIn()
    {
        if (canvasGroup == null)
        {
            Debug.LogError("canvasGroupが設定されていません");
            yield break;
        }

        float duration = 1.0f; // フェードインの時間（秒）
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }
    }
}