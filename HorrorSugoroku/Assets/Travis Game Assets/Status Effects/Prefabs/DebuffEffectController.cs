using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

using UnityEngine.UI;
public class DebuffEffectController : MonoBehaviour
{
    private Volume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    public string cellEffect = "Normal";

    void Start()
    {
        postProcessVolume = FindObjectOfType<Volume>(); // �|�X�g�v���Z�X Volume ���擾
        postProcessVolume.profile.TryGet(out chromaticAberration); // �F������K�p�ł��邩�m�F
    }

    void Update()
    {
        if (cellEffect == "Debuff")
        {
            chromaticAberration.intensity.value = 0.5f; // �f�o�t���ɋ���
        }
        else
        {
            chromaticAberration.intensity.value = 0f; // �ʏ펞�ɃI�t
        }
    }
}