using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameManager gameManager;
    public CameraController cameraController;
    public ElevatorIdou elevatorIdou;
    public ClickObject clickObject;

    //public GameObject rollDiceRawImage;
    public GameObject OptionCanvas; // オプション画面

    public Slider VolumeSlider; // 音量バー
    public Image VolumeImg; // 音量の画像
    public Sprite VolumeSprite; // スピーカーの画像
    public Sprite VolumeMuteSprite; // ミュート画像
    public int Volume = 50; // 音量
    public Slider SensitivitySlider; // マウス感度バー
    private float Sensitivity = 250.0f; // 感度
    public bool isOptionOpen = false; // オプション画面の開閉状態

    void Start()
    {
        OptionCanvas.SetActive(false);

        // 音量を初期化
        VolumeImg.sprite = VolumeSprite;
        VolumeSlider.value = Volume;
        AudioListener.volume = Volume / 100f;

        // カメラ感度を初期化
        SensitivitySlider.minValue = 100;
        SensitivitySlider.maxValue = 300;
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

        // ESCキーで設定画面を開閉
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenOption();
        }
    }

    private void OnVolumeChanged(float value)
    {
        Volume = (int)value;
        AudioListener.volume = Volume / 100f;
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
            cameraController.isMouseLocked = false;
            cameraController.SetOptionOpen(true);
            clickObject.enabled = false;
            //rollDiceRawImage.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            if (!elevatorIdou.elevatorPanelOn)
            {
                // オプションを閉じる → カーソルを確実に非表示にする
                cameraController.isMouseLocked = true;
                cameraController.SetOptionOpen(false);

                // Altキーの影響をリセット
                ResetAltKeyState();

                // マウスをロックし、確実にカーソルを非表示にする
                Cursor.lockState = CursorLockMode.Locked;

                clickObject.enabled = true;
                //rollDiceRawImage.SetActive(true);
                Invoke(nameof(HideCursor), 0.02f);  // 0.02秒遅延して確実にカーソルを非表示
            }
                Time.timeScale = 1;
        }
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    private void ResetAltKeyState()
    {
        Input.ResetInputAxes();
    }

    public bool IsOptionOpen()
    {
        return isOptionOpen;
    }
}
