using UnityEngine;
using UnityEngine.UI;

public class ImageFillerWithLight : MonoBehaviour
{
    [SerializeField] private Image targetImage;       // Filled�^�C�v��Image
    [SerializeField] private float fillSpeed = 0.5f;   // 1�b��fillAmount��0.5����
    [SerializeField] private Light targetLight;        // ���삷�郉�C�g
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
            // �Q�[�W��������
            if (targetImage.fillAmount < 1f)
            {
                targetImage.fillAmount += fillSpeed * Time.deltaTime;
                targetImage.fillAmount = Mathf.Clamp01(targetImage.fillAmount);
                targetLight.enabled = false;
                lightTurnedOn = false;
            }
            else if (!lightTurnedOn)
            {
                // fillAmount��1�ɂȂ����u�ԂɃ��C�g�I�����Q�[�W��\��
                targetLight.enabled = true;
                lightTurnedOn = true;
                targetImage.gameObject.SetActive(false);
            }
        }
        else
        {
            // �Q�[�W�����Z�b�g�igaugeCircle��false�̂Ƃ��j
            targetImage.fillAmount = 0f;
            targetImage.gameObject.SetActive(true); // �K�v�ɉ����čĕ\��
            targetLight.enabled = false;
            lightTurnedOn = true;
        }
    }
}
