using UnityEngine;

public class ClickTest : MonoBehaviour
{
    bool n = false;

    void Update()
    {
        if (n)
        {
            Debug.Log("触れたよ！");
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == ("Object"))
        {
            Debug.Log("触れてる！");
            if (Input.GetMouseButtonDown(0))
            {
                n = true;
            }
        }
    }
}
