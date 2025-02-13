using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;
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

        // ���ʂ�������
        VolumeSlider.value = Volume;
        AudioListener.volume = Volume / 100f;//������100f�������Ɖ����ꂪ�N����

        // �J�������x��������
        SensitivitySlider.value = Sensitivity;
        cameraController.mouseSensitivity = Sensitivity;
        Debug.Log("�������x///:" + SensitivitySlider.value);

        // �X���C�_�[�̃��X�i�[��ǉ�
        VolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        SensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    void Update()
    {
        // �{�����[���� 0 �Ȃ�~���[�g�A�C�R���ɕύX
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
        AudioListener.volume = Volume / 100f;//������100f�������Ɖ����ꂪ�N����
    }

    // �J�������x�X���C�_�[�̒l�ύX���̏���
    private void OnSensitivityChanged(float value)
    {
        cameraController.mouseSensitivity = value;

    }

    // �I�v�V�����{�^������������\���E��\����؂�ւ���
    public void OpenOption()
    {
        Setting = !Setting;
        OptionCanvas.SetActive(Setting);
    }

}
