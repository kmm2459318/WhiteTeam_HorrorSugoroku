using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameManager gameManager;
    public CameraController cameraController;

    public GameObject OptionCanvas; // �I�v�V�������

    public Slider VolumeSlider; // ���ʃo�[
    public Image VolumeImg; // ���ʂ̉摜
    public Sprite VolumeSprite; // �X�s�[�J�[�̉摜
    public Sprite VolumeMuteSprite; // �~���[�g�摜
    public int Volume = 50; // ����
    public Slider SensitivitySlider; // �}�E�X���x�o�[
    public float Sensitivity = 250.0f; // ���x
    private bool isOptionOpen = false; // �I�v�V������ʂ̊J���

    void Start()
    {
        OptionCanvas.SetActive(false);

        // ���ʂ�������
        VolumeSlider.value = Volume;
        AudioListener.volume = Volume / 100f;//������100f�������Ǝ������ʁI(���������)

        // �J�������x��������
        SensitivitySlider.value = Sensitivity;
        cameraController.mouseSensitivity = Sensitivity;

        // �X���C�_�[�̃��X�i�[��ǉ�
        VolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        SensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    void Update()
    {
        // �{�����[���� 0 �Ȃ�~���[�g�A�C�R���ɕύX
        VolumeImg.sprite = (AudioListener.volume == 0) ? VolumeMuteSprite : VolumeSprite;
    }

    private void OnVolumeChanged(float value)
    {
        Volume = (int)value;
        AudioListener.volume = Volume / 100f;//������100f�������Ǝ������ʁI(���������)
    }

    private void OnSensitivityChanged(float value)
    {
        cameraController.mouseSensitivity = value;
    }

    // �I�v�V�����{�^������������\���E��\����؂�ւ���
    public void OpenOption()
    {
        isOptionOpen = !isOptionOpen;
        OptionCanvas.SetActive(isOptionOpen);

        if (isOptionOpen)
        {
            // �I�v�V�������J�� �� �}�E�X��\�����A�J��������𖳌���
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cameraController.SetMouseLock(false);
        }
        else
        {
            // �I�v�V��������� �� �}�E�X�����b�N���A�J���������L����
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameraController.SetMouseLock(true);
        }
    }

    // CameraController �ŃI�v�V�����̊J��Ԃ��擾���邽�߂̃��\�b�h
    public bool IsOptionOpen()
    {
        return isOptionOpen;
    }
}
