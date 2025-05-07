using UnityEngine;
using UnityEngine.UI;

public class ImageFillerWithLight : MonoBehaviour
{
    [SerializeField] private Image targetImage;       // FilledタイプのImage
    [SerializeField] private float fillSpeed = 0.5f;   // 1秒でfillAmountが0.5増加
    [SerializeField] private Light targetLight;        // 操作するライト
    [SerializeField] public PlayerSaikoro playerSaikoro;

    private bool lightTurnedOn = false;

    private void Start()
    {
        if (targetImage != null)
        {
            targetImage.type = Image.Type.Filled;
            targetImage.fillAmount = 0f;
        }

        if (targetLight != null)
        {
            targetLight.enabled = false;
        }
    }

    private void Update()
    {
        if (targetImage == null || targetLight == null || playerSaikoro == null) return;

        if (playerSaikoro.gaugeCircle)
        {
            // ゲージ増加処理
            if (targetImage.fillAmount < 1f)
            {
                targetImage.fillAmount += fillSpeed * Time.deltaTime;
                targetImage.fillAmount = Mathf.Clamp01(targetImage.fillAmount);
                targetLight.enabled = false;
                lightTurnedOn = false;
            }
            else if (!lightTurnedOn)
            {
                // fillAmountが1になった瞬間にライトオン＆ゲージ非表示
                targetLight.enabled = true;
                lightTurnedOn = true;
                targetImage.gameObject.SetActive(false);
            }
        }
        else
        {
            // ゲージをリセット（gaugeCircleがfalseのとき）
            targetImage.fillAmount = 0f;
            targetImage.gameObject.SetActive(true); // 必要に応じて再表示
            targetLight.enabled = false;
            lightTurnedOn = true;
        }
    }
}
