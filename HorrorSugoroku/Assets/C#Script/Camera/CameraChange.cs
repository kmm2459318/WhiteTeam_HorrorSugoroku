using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject diceCamera;
    void Start()
    {
        // サブカメラはデフォルトで無効にしておく
        diceCamera.SetActive(false);
    }

    void Update()
    {

    }

    public void Change()
    {
        // 各カメラオブジェクトの有効フラグを逆転(true→false,false→true)させる
        mainCamera.SetActive(!mainCamera.activeSelf);
        diceCamera.SetActive(!diceCamera.activeSelf);
    }
}
