using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageFillerWithLight : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private List<Light> targetLights; // 複数ライト対応
    [SerializeField] public PlayerSaikoro playerSaikoro;
    [SerializeField] public EnemyStop enemyStop;

    private bool lightTurnedOn = false;
    private float fillSpeed = 1f;

    private void Start()
    {
        if (targetImage != null)
        {
            targetImage.type = Image.Type.Filled;
            targetImage.fillAmount = 0f;
        }

        SetLightsEnabled(false);

        if (enemyStop.walkNumber > 0f)
        {
            fillSpeed = 1f / enemyStop.walkNumber;
        }
    }

    private void Update()
    {
        if (targetImage == null || targetLights == null || playerSaikoro == null || enemyStop == null) return;

        // walkNumberが動的に変化しても対応（0除算を避ける）
        fillSpeed = (enemyStop.walkNumber > 0f) ? 1f / enemyStop.walkNumber : 0f;

        if (playerSaikoro.diceLight)
        {
            SetLightsEnabled(true);
            targetImage.gameObject.SetActive(false);
            lightTurnedOn = true;
        }

        if (playerSaikoro.gaugeCircle)
        {
            if (targetImage.fillAmount < 1f)
            {
                targetImage.fillAmount += fillSpeed * Time.deltaTime;
                targetImage.fillAmount = Mathf.Clamp01(targetImage.fillAmount);
                SetLightsEnabled(false);
                lightTurnedOn = false;
            }
            else if (!lightTurnedOn)
            {
                SetLightsEnabled(true);
                targetImage.gameObject.SetActive(false);
                lightTurnedOn = true;
            }
        }
        else
        {
            targetImage.fillAmount = 0f;
            targetImage.gameObject.SetActive(true);
            lightTurnedOn = true;
        }
    }

    // 複数ライトの有効/無効を一括で切り替える関数
    private void SetLightsEnabled(bool enabled)
    {
        foreach (var light in targetLights)
        {
            if (light != null)
            {
                light.enabled = enabled;
            }
        }
    }
}
