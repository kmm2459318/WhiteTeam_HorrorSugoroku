using UnityEngine;
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
    private float Sensitivity = 250.0f; // ���x
    private bool isOptionOpen = false; // �I�v�V������ʂ̊J���

    void Start()
    {
        OptionCanvas.SetActive(false);

        // ���ʂ�������
        VolumeImg.sprite = VolumeSprite;
        VolumeSlider.value = Volume;
        AudioListener.volume = Volume / 100f;

        // �J�������x��������
        SensitivitySlider.minValue = 100;
        SensitivitySlider.maxValue = 300;
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

        // ESC�L�[�Őݒ��ʂ��J��
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
            cameraController.isMouseLocked = false;
            cameraController.SetOptionOpen(true); // Alt�L�[������
            Time.timeScale = 0;
        }
        else
        {
            // �I�v�V��������� �� �J�[�\�����m���ɔ�\���ɂ���
            cameraController.isMouseLocked = true;
            cameraController.SetOptionOpen(false); // Alt�L�[�L����

            // �܂��}�E�X�����b�N����
            Cursor.lockState = CursorLockMode.Locked;

            // �����x��ăJ�[�\�����\���ɂ���i�m���ɏ�����悤�Ɂj
            Invoke(nameof(HideCursor), 0.02f);

            Time.timeScale = 1;
        }
    }

    private void HideCursor()
    {
        Cursor.visible = false;
    }

    // CameraController �ŃI�v�V�����̊J��Ԃ��擾���邽�߂̃��\�b�h
    public bool IsOptionOpen()
    {
        return isOptionOpen;
    }
}
