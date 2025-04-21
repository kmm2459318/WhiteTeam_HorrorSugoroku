using UnityEngine;

public class AlwaysBrightLight : MonoBehaviour
{
    [SerializeField] private Light[] alwaysBrightLights; // ��ɖ��邭���郉�C�g
    [SerializeField] private float maxIntensity = 2f; // �ő�̖��邳

    private void Start()
    {
        // �w�肵�����C�g����ɖ��邭�ݒ�
        foreach (Light light in alwaysBrightLights)
        {
            if (light != null)
            {
                light.enabled = true; // ���C�g���I���ɂ���
                light.intensity = maxIntensity; // ���邳���ő�ɂ���
                Debug.Log($"���C�g '{light.name}' ����ɖ��邢��Ԃɐݒ肵�܂���");
            }
        }
    }
}