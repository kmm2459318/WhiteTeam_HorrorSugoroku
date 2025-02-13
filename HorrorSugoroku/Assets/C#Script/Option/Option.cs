using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
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

        //音量を初期化
        VolumeSlider.value = Volume;
        //Debug.Log("初期音量:" + VolumeSlider.value);

        //カメラ感度を初期化
        SensitivitySlider.value = Sensitivity;
        cameraController.mouseSensitivity = Sensitivity;
        Debug.Log("初期感度///:" + SensitivitySlider.value);


        
    }

    // Update is called once per frame
    void Update()
    {
        //バーの値を反映させる
        Volume = (int)VolumeSlider.value;
        VolumeSlider.value = Volume;
        
        //カメラ感度変更
        Sensitivity = SensitivitySlider.value;
        SensitivitySlider.value = Sensitivity;
        cameraController.mouseSensitivity = Sensitivity;


        Debug.Log("変更音量:" + VolumeSlider.value);
        Debug.Log("変更感度:" + SensitivitySlider.value);

        //ボリュームが0なら画像変更
        if(Volume == 0)
        {
            VolumeImg.sprite = VolumeMuteSprite;
        }
        else
        {
            VolumeImg.sprite = VolumeSprite;
        }

    }

    //オプションボタンを押したら表示するようにする
    public void OpenOption()
    {
        if(Setting == false)
        {
            OptionCanvas.SetActive(true);
            Setting = true;
        }
        else
        {
            OptionCanvas.SetActive(false);
            Setting = false;
        }
        
    }

}
