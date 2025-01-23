using UnityEngine;

public class EnemyNSEWCheck : MonoBehaviour
{
    public bool masuCheck = false;

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "masu")
        {
            masuCheck = false;
            Debug.Log("Enemy exited masu area");
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "masu")
        {
            masuCheck = true;
            Debug.Log("Enemy entered masu area");
        }
    }
}