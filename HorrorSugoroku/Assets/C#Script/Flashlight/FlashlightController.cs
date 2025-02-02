using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Header("�����d���ݒ�")]
    public Light flashlight; // �����d���̃��C�g
    private float maxBattery = 100f; // �ő�d�r�c��
    private float batteryDrainPerTurn = 20f; // 1�^�[��������̏����
    private float maxRange = 22f; // ���C�g�̍ő�Range
    private float minRange = 0f;  // ���C�g�̍ŏ�Range

    [Header("UI�ݒ�")]
    public Image batteryImage; // �c�ʃQ�[�W�p��Image
    public Button batteryButton; // �d�r�񕜗p�̃{�^��   // ��

    private float currentBattery;

    void Start()
    {
        currentBattery = maxBattery; // ������
        UpdateBatteryUI();

        if (batteryButton != null)
        {
            // �{�^���ɃN���b�N�C�x���g��o�^
            batteryButton.onClick.AddListener(() => AddBattery(10f));   // �o�b�e���[�̉񕜗� ����10�ɐݒ�
        }
    }

    // �^�[�����i�񂾂Ƃ��ɌĂяo�����
    public void OnTurnAdvanced()
    {
        // �d�r�������
        currentBattery -= batteryDrainPerTurn;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

        // �c�ʂ��Ȃ��Ȃ����烉�C�g������
        if (currentBattery <= 0)
        {
            flashlight.enabled = false;
        }
        else
        {
            // �d�r�c�ʂɉ����ă��C�g��Range���X�V
            UpdateFlashlightRange();
        }

        UpdateBatteryUI();
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
            UpdateFlashlightRange();
        }

        UpdateBatteryUI();
    }

    private void UpdateBatteryUI()
    {
        if (batteryImage != null)
        {
            // �h��Ԃ�������ݒ�
            batteryImage.fillAmount = currentBattery / maxBattery;
        }
    }

    private void UpdateFlashlightRange()
    {
        // �d�r�c�ʂ̊������v�Z����Range��ݒ�
        float range = Mathf.Lerp(minRange, maxRange, currentBattery / maxBattery);
        flashlight.range = range;
    }
}
