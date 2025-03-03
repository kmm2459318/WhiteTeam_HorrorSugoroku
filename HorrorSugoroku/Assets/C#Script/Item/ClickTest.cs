using UnityEngine;

public class ClickTest : MonoBehaviour
{
    bool n = false;

    void Update()
    {
        if (n)
        {
            Debug.Log("触れたよ！");
            n = false; // フラグをリセット
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("オブジェクトがクリックされました！");
        n = true;
    }
}