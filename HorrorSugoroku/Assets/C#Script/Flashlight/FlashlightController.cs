using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Header("�����d���ݒ�")]
    public Light flashlight; // �����d���̃��C�g
    public float maxBattery = 100f; // �ő�d�r�c��
    public float batteryDrainRate = 1f; // �d�r�����鑬�x�i���b�j

    [Header("UI�ݒ�")]
    public Slider batterySlider; // �c�ʃQ�[�W�p�̃X���C�_�[

    private float currentBattery;

    void Start()
    {
        currentBattery = maxBattery; // ������
        UpdateBatteryUI();
    }

    void Update()
    {
        if (currentBattery > 0)
        {
            // �d�r�������
            currentBattery -= batteryDrainRate * Time.deltaTime;
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

            // �c�ʂ��Ȃ��Ȃ����烉�C�g������
            if (currentBattery <= 0)
            {
                flashlight.enabled = false;
            }

            UpdateBatteryUI();
        }
    }

    public void AddBattery(float amount)
    {
        // �d�r���E�����Ƃ��̏���
        currentBattery += amount;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

        // �d�r�����������ꍇ�A���C�g���ēx�_����
        if (currentBattery > 0)
        {
            flashlight.enabled = true;
        }

        UpdateBatteryUI();
    }

    private void UpdateBatteryUI()
    {
        if (batterySlider != null)
        {
            batterySlider.value = currentBattery / maxBattery;
        }
    }
}
