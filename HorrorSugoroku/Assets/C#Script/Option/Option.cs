using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject OptionCanvas;//�I�v�V�������

    public Slider VolumeSlider;//���ʃo�[
    public int Volume = 50;//����
    public Slider SensitivitySlider;//�}�E�X���x�o�[
    public int Sensitivity = 2;// ���x///

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OptionCanvas.SetActive(false);

        //���ʁA���x��������
        VolumeSlider.value = Volume;
        SensitivitySlider.value = Sensitivity;
        Debug.Log("����:" + VolumeSlider.value);
        Debug.Log("���x///:" + SensitivitySlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        //�o�[�̒l�𔽉f������
        Volume = (int)VolumeSlider.value;
        Sensitivity = (int)SensitivitySlider.value;
        VolumeSlider.value = Volume;
        SensitivitySlider.value = Sensitivity;

        Debug.Log("�ύX����:" + VolumeSlider.value);
        Debug.Log("�ύX���x:" + SensitivitySlider.value);

    }

    //�I�v�V�����{�^������������\������悤�ɂ���
    public void OpenOption()
    {
        OptionCanvas.SetActive(true);
    }

}
