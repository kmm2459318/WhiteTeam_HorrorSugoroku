using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject OptionCanvas;//オプション画面

    public Slider VolumeSlider;//音量バー
    public int Volume = 50;//音量
    public Slider SensitivitySlider;//マウス感度バー
    public int Sensitivity = 2;// 感度///

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OptionCanvas.SetActive(false);

        //音量、感度を初期化
        VolumeSlider.value = Volume;
        SensitivitySlider.value = Sensitivity;
        Debug.Log("音量:" + VolumeSlider.value);
        Debug.Log("感度///:" + SensitivitySlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        //バーの値を反映させる
        Volume = (int)VolumeSlider.value;
        Sensitivity = (int)SensitivitySlider.value;
        VolumeSlider.value = Volume;
        SensitivitySlider.value = Sensitivity;

        Debug.Log("変更音量:" + VolumeSlider.value);
        Debug.Log("変更感度:" + SensitivitySlider.value);

    }

    //オプションボタンを押したら表示するようにする
    public void OpenOption()
    {
        OptionCanvas.SetActive(true);
    }

}
