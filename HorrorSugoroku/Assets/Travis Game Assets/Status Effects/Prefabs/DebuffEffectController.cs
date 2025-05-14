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
        postProcessVolume = FindObjectOfType<Volume>(); // ポストプロセス Volume を取得
        postProcessVolume.profile.TryGet(out chromaticAberration); // 色収差を適用できるか確認
    }

    void Update()
    {
        if (cellEffect == "Debuff")
        {
            chromaticAberration.intensity.value = 0.5f; // デバフ時に強調
        }
        else
        {
            chromaticAberration.intensity.value = 0f; // 通常時にオフ
        }
    }
}