using UnityEngine;
using System.Collections;

public class IsRendered : MonoBehaviour
{
    public Camera targetCamera;

    //���C���J�����ɕt���Ă���^�O��
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    //�J�����ɕ\������Ă��邩
    private bool _isRendered = false;

    private void Update()
    {

        if (_isRendered)
        {
            //Debug.Log("�J�����ɉf���Ă��");
        }
        else
        {
            //Debug.Log("�J�����ɉf���Ă��Ȃ���");
        }

        //�J�����Ɏʂ��Ă���Γ��蔻��L��
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = _isRendered;
        }

        _isRendered = false;
    }

    //�J�����ɉf���Ă�ԂɌĂ΂��
    private void OnWillRenderObject()
    {
        //���C���J�����ɉf����������_isRendered��L����
        if (Camera.current == targetCamera)
        {
            _isRendered = true;
        }
    }

}