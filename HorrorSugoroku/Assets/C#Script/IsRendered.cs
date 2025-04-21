using UnityEngine;
using System.Collections;

public class IsRendered : MonoBehaviour
{
    public Camera targetCamera;

    //メインカメラに付いているタグ名
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    //カメラに表示されているか
    private bool _isRendered = false;

    private void Update()
    {

        if (_isRendered)
        {
            //Debug.Log("カメラに映ってるよ");
        }
        else
        {
            //Debug.Log("カメラに映っていないよ");
        }

        //カメラに写っていれば当り判定有効
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = _isRendered;
        }

        _isRendered = false;
    }

    //カメラに映ってる間に呼ばれる
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ_isRenderedを有効に
        if (Camera.current == targetCamera)
        {
            _isRendered = true;
        }
    }

}