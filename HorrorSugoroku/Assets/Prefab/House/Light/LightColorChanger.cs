using UnityEngine;

public class LightColorChanger : MonoBehaviour
{
    
    public Color[] colors; // 変更したい色の配列
    private int currentColorIndex = 0;
    public string excludedTag = "ExcludeLight"; // 除外したいライトのタグ
  
    void Update()
    {
        // Dキーが押されたときに色を変更
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLightColors();
        }
    }

    void ChangeLightColors()
    {
        // 現在の色をインクリメントして次の色を選ぶ
        currentColorIndex = (currentColorIndex + 1) % colors.Length;

        // シーン内の全てのライトを取得し、色を変更
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            // ライトが除外タグを持っている場合はスキップ
            if (light.CompareTag(excludedTag))
            {
                continue;
            }
            light.color = colors[currentColorIndex];
        }
    }
   
}