using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameManager gameManager;
    public CameraController cameraController;

    public GameObject OptionCanvas;//�I�v�V�������

    public Slider VolumeSlider;//���ʃo�[
    public Image VolumeImg;//���ʂ̉摜
    public Sprite VolumeSprite;//�X�s�[�J�[�̉摜
    public Sprite VolumeMuteSprite;//�~���[�g�摜
    public int Volume = 50;//����
    public Slider SensitivitySlider;//�}�E�X���x�o�[
    public float Sensitivity = 250f;// ���x
    private bool Setting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OptionCanvas.SetActive(false);

        //���ʂ�������
        VolumeSlider.value = Volume;
        //Debug.Log("��������:" + VolumeSlider.value);

        //�J�������x��������
        SensitivitySlider.value = Sensitivity;
        cameraController.mouseSensitivity = Sensitivity;
        Debug.Log("�������x///:" + SensitivitySlider.value);


        
    }

    // Update is called once per frame
    void Update()
    {
        //�o�[�̒l�𔽉f������
        Volume = (int)VolumeSlider.value;
        VolumeSlider.value = Volume;
        
        //�J�������x�ύX
        Sensitivity = SensitivitySlider.value;
        SensitivitySlider.value = Sensitivity;
        cameraController.mouseSensitivity = Sensitivity;


        Debug.Log("�ύX����:" + VolumeSlider.value);
        Debug.Log("�ύX���x:" + SensitivitySlider.value);

        //�{�����[����0�Ȃ�摜�ύX
        if(Volume == 0)
        {
            VolumeImg.sprite = VolumeMuteSprite;
        }
        else
        {
            VolumeImg.sprite = VolumeSprite;
        }

    }

    //�I�v�V�����{�^������������\������悤�ɂ���
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
