using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameManager gameManager;
    public CameraController cameraController;

    public GameObject OptionCanvas; // オプション画面

    public Slider VolumeSlider; // 音量バー
    public Image VolumeImg; // 音量の画像
    public Sprite VolumeSprite; // スピーカーの画像
    public Sprite VolumeMuteSprite; // ミュート画像
    public int Volume = 50; // 音量
    public Slider SensitivitySlider; // マウス感度バー
    public float Sensitivity = 250.0f; // 感度
    private bool isOptionOpen = false; // オプション画面の開閉状態

    void Start()
    {
        OptionCanvas.SetActive(false);

        // 音量を初期化
        VolumeSlider.value = Volume;
        AudioListener.volume = Volume / 100f;//ここの100fを消すと耳が死ぬ！(音が割れる)

        // カメラ感度を初期化
        SensitivitySlider.value = Sensitivity;
        cameraController.mouseSensitivity = Sensitivity;

        // スライダーのリスナーを追加
        VolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        SensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    void Update()
    {
        // ボリュームが 0 ならミュートアイコンに変更
        VolumeImg.sprite = (AudioListener.volume == 0) ? VolumeMuteSprite : VolumeSprite;
    }

    private void OnVolumeChanged(float value)
    {
        Volume = (int)value;
        AudioListener.volume = Volume / 100f;//ここの100fを消すと耳が死ぬ！(音が割れる)
    }

    private void OnSensitivityChanged(float value)
    {
        cameraController.mouseSensitivity = value;
    }

    // オプションボタンを押したら表示・非表示を切り替える
    public void OpenOption()
    {
        isOptionOpen = !isOptionOpen;
        OptionCanvas.SetActive(isOptionOpen);

        if (isOptionOpen)
        {
            // オプションを開く → マウスを表示し、カメラ操作を無効化
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cameraController.SetMouseLock(false);
        }
        else
        {
            // オプションを閉じる → マウスをロックし、カメラ操作を有効化
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameraController.SetMouseLock(true);
        }
    }

    // CameraController でオプションの開閉状態を取得するためのメソッド
    public bool IsOptionOpen()
    {
        return isOptionOpen;
    }
}
