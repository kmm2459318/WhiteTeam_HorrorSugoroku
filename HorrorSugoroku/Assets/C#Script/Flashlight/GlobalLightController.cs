using UnityEngine;
using TMPro; // TextMeshPro ���g�p���邽�߂̖��O���
using System.Collections; // IEnumerator ���g�p

public class GlobalLightController : MonoBehaviour
{
    [SerializeField] private bool turnOn = false; // ���C�g�̏�����Ԃ̓I�t
    [SerializeField] private Light[] alwaysOnLights; // ��ɃI���ɂ��������C�g
    [SerializeField] private GameObject[] outlineObjects; // ������Outline�I�u�W�F�N�g
    [SerializeField] private Color outlineColor = Color.red; // �A�E�g���C���̐F
    [SerializeField, Range(0f, 10f)] private float outlineWidth = 2f; // �A�E�g���C���̕�
    [SerializeField] private TextMeshProUGUI messageText; // TextMeshPro �Ńe�L�X�g�\��
    [SerializeField] private Animator objectAnimator; // �A�j���[�^�[��ǉ�
    [SerializeField] private float fadeDuration = 2f; // ���C�g�̃t�F�[�h�C������
    [SerializeField] private float maxIntensity = 2f; // ���C�g�̍ő�P�x

    private Outline[] outlines; // Outline �R���|�[�l���g�̔z��

    void Update()
    {
        // A�L�[�������ꂽ�烉�C�g���I���ɂ��A�A�j���[�V������Trigger�𔭉�
        if (Input.GetKeyDown(KeyCode.A))
        {
            turnOn = true;
            StartCoroutine(FadeInLights()); // �R���[�`�����J�n
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

            yield return null; // ���̃t���[���܂őҋ@
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
                if (!turnOn) light.intensity = 0; // ���C�g���I�t�̂Ƃ��͋P�x��0��
            }
        }

        foreach (Light alwaysOnLight in alwaysOnLights)
        {
            if (alwaysOnLight != null)
            {
                alwaysOnLight.enabled = true;
            }
        }

        // �����̃A�E�g���C���I�u�W�F�N�g�ɑΉ�
        if (outlines != null)
        {
            foreach (var outline in outlines)
            {
                if (outline != null)
                {
                    outline.enabled = !turnOn; // ���C�g���I�t�Ȃ�A�E�g���C����\��
                    if (!turnOn) // �I�t�̂Ƃ��̂ݐF�ƕ���ݒ�
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
                        Debug.LogError($"Outline �R���|�[�l���g���I�u�W�F�N�g {outlineObjects[i].name} �Ɍ�����܂���ł����B");
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