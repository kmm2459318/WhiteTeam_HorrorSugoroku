using UnityEngine;

public class LightColorChanger : MonoBehaviour
{
    
    public Color[] colors; // �ύX�������F�̔z��
    private int currentColorIndex = 0;
    public string excludedTag = "ExcludeLight"; // ���O���������C�g�̃^�O
  
    void Update()
    {
        // D�L�[�������ꂽ�Ƃ��ɐF��ύX
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLightColors();
        }
    }

    void ChangeLightColors()
    {
        // ���݂̐F���C���N�������g���Ď��̐F��I��
        currentColorIndex = (currentColorIndex + 1) % colors.Length;

        // �V�[�����̑S�Ẵ��C�g���擾���A�F��ύX
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            // ���C�g�����O�^�O�������Ă���ꍇ�̓X�L�b�v
            if (light.CompareTag(excludedTag))
            {
                continue;
            }
            light.color = colors[currentColorIndex];
        }
    }
   
}