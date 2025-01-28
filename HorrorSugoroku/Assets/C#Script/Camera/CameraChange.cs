using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject diceCamera;
    void Start()
    {
        // �T�u�J�����̓f�t�H���g�Ŗ����ɂ��Ă���
        diceCamera.SetActive(false);
    }

    void Update()
    {

    }

    public void Change()
    {
        // �e�J�����I�u�W�F�N�g�̗L���t���O���t�](true��false,false��true)������
        mainCamera.SetActive(!mainCamera.activeSelf);
        diceCamera.SetActive(!diceCamera.activeSelf);
    }
}
