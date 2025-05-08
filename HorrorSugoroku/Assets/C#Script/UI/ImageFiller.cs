using UnityEngine;
using UnityEngine.UI;

public class ImageFillerWithLight : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private Light targetLight;
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

        if (targetLight != null)
        {
            targetLight.enabled = false;
        }

        if (enemyStop.walkNumber > 0f)
        {
            fillSpeed = 1f / enemyStop.walkNumber;
        }
    }

    private void Update()
    {
        if (targetImage == null || targetLight == null || playerSaikoro == null || enemyStop == null) return;

        // walkNumber‚ª“®“I‚É•Ï‰»‚µ‚Ä‚à‘Î‰ži0œŽZ‚ð”ð‚¯‚éj
        if (enemyStop.walkNumber > 0f)
        {
            fillSpeed = 1f / enemyStop.walkNumber;
        }
        else
        {
            fillSpeed = 0f;
        }

        if (playerSaikoro.diceLight)
        {
            targetLight.enabled = true;
        }

        if (playerSaikoro.gaugeCircle)
        {
            if (targetImage.fillAmount < 1f)
            {
                targetImage.fillAmount += fillSpeed * Time.deltaTime;
                targetImage.fillAmount = Mathf.Clamp01(targetImage.fillAmount);
                targetLight.enabled = false;
                lightTurnedOn = false;
            }
            else if (!lightTurnedOn)
            {
                targetLight.enabled = true;
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
}
