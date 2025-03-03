using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // マスに触れたとき
        if (other.CompareTag("masu"))
        {
            // マスのレンダラーを表示
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // マスから離れたとき
        if (other.CompareTag("masu"))
        {
            // マスのレンダラーを非表示
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
    }
}