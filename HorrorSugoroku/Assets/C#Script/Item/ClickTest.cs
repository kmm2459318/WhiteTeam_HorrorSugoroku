using UnityEngine;

public class ClickTest : MonoBehaviour
{
    bool n = false;

    void Update()
    {
        if (n)
        {
            Debug.Log("�G�ꂽ��I");
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == ("Object"))
        {
            Debug.Log("�G��Ă�I");
            if (Input.GetMouseButtonDown(0))
            {
                n = true;
            }
        }
    }
}
