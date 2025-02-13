using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameManager gameManager;
    public CameraController cameraController;

    public GameObject OptionCanvas;//オプション画面

    public Slider VolumeSlider;//音量バー
    public Image VolumeImg;//音量の画像
    public Sprite VolumeSprite;//スピーカーの画像
    public Sprite VolumeMuteSprite;//ミュート画像
    public int Volume = 50;//音量
    public Slider SensitivitySlider;//マウス感度バー
    public float Sensitivity = 250f;// 感度
    private bool Setting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OptionCanvas.SetActive(false);

        // 音量を初期化
        VolumeSlider.value = Volume;
        AudioListener.volume = Volume / 100f;//ここの100fを消すと音割れが起きる

        // カメラ感度を初期化
        SensitivitySlider.value = Sensitivity;
        cameraController.mouseSensitivity = Sensitivity;
        Debug.Log("初期感度///:" + SensitivitySlider.value);

        // スライダーのリスナーを追加
        VolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        SensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    void Update()
    {
        // ボリュームが 0 ならミュートアイコンに変更
        if (AudioListener.volume == 0)
        {
            VolumeImg.sprite = VolumeMuteSprite;
        }
        else
        {
            VolumeImg.sprite = VolumeSprite;
        }
    }

    private void OnVolumeChanged(float value)
    {
        Volume = (int)value;
        AudioListener.volume = Volume / 100f;//ここの100fを消すと音割れが起きる
    }

    // カメラ感度スライダーの値変更時の処理
    private void OnSensitivityChanged(float value)
    {
        cameraController.mouseSensitivity = value;

    }

    // オプションボタンを押したら表示・非表示を切り替える
    public void OpenOption()
    {
        Setting = !Setting;
        OptionCanvas.SetActive(Setting);
    }

}
