using UnityEngine;
using TMPro; // TextMeshPro ���g�p���邽�߂̖��O���

public class GlobalLightController : MonoBehaviour
{
    [SerializeField] private bool turnOn = false; // �C���X�y�N�^�[�Ń��C�g�̃I���E�I�t��ݒ�
    [SerializeField] private Light[] alwaysOnLights; // ��ɃI���ɂ��������C�g
    [SerializeField] private GameObject outlineObject; // Outline.cs ���A�^�b�`���ꂽ�I�u�W�F�N�g
    [SerializeField] private Color outlineColor = Color.red; // �A�E�g���C���̐F���C���X�y�N�^�[�Őݒ�
    [SerializeField, Range(0f, 10f)] private float outlineWidth = 2f; // �A�E�g���C���̕�
    [SerializeField] private TextMeshProUGUI messageText; // �e�L�X�g�\���p�� TextMeshPro �R���|�[�l���g

    private Outline outline; // Outline �R���|�[�l���g���L���b�V��

    public void ApplyLightState()
    {
        Light[] allLights = FindObjectsOfType<Light>(); // �V�[�����̂��ׂẴ��C�g���擾
        foreach (Light light in allLights)
        {
            if (light != null && !IsAlwaysOn(light))
            {
                light.enabled = turnOn; // ���C�g�̃I���E�I�t��ݒ�
            }
        }

        // ��ɃI���̃��C�g�͈ێ�
        foreach (Light alwaysOnLight in alwaysOnLights)
        {
            if (alwaysOnLight != null)
            {
                alwaysOnLight.enabled = true;
            }
        }

        // ���C�g�̏�Ԃɉ����ăA�E�g���C���ƃe�L�X�g��؂�ւ�
        if (outline != null)
        {
            if (!turnOn) // ���C�g���I�t�̂Ƃ�
            {
                EnableOutline();
                ShowMessage("Go MachineRoom");
            }
            else // ���C�g���I���̂Ƃ�
            {
                DisableOutline();
                ShowMessage(""); // �e�L�X�g��\��
            }
        }
    }

    private bool IsAlwaysOn(Light light)
    {
        foreach (Light alwaysOnLight in alwaysOnLights)
        {
            if (alwaysOnLight == light)
            {
                return true; // ��ɃI���̃��C�g���X�g�Ɋ܂܂��ꍇ
            }
        }
        return false; // �܂܂�Ȃ��ꍇ
    }

    private void Start()
    {
        if (outlineObject != null)
        {
            // Outline �R���|�[�l���g���擾
            outline = outlineObject.GetComponent<Outline>();
            if (outline == null)
            {
                Debug.LogError("Outline �R���|�[�l���g���w�肳�ꂽ�I�u�W�F�N�g�Ɍ�����܂���ł����B");
                return;
            }

            // �����ݒ�
            outline.OutlineColor = outlineColor;
            outline.OutlineWidth = outlineWidth;
            outline.enabled = !turnOn; // ���C�g�̏�Ԃɉ����ăA�E�g���C����������
        }

        ApplyLightState(); // �X�^�[�g���ɓK�p
    }

    private void EnableOutline()
    {
        outline.OutlineColor = outlineColor; // �A�E�g���C���̐F��ݒ�
        outline.OutlineWidth = outlineWidth; // �A�E�g���C���̕���ݒ�
        outline.enabled = true; // �A�E�g���C����L����
    }

    private void DisableOutline()
    {
        outline.enabled = false; // �A�E�g���C���𖳌���
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message; // �w�肳�ꂽ�e�L�X�g��\��
        }
    }
}