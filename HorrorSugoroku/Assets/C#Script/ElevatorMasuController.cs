using UnityEngine;

public class ElevatorMasuController : MonoBehaviour
{
    public bool playerOn = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = false;
        }
    }
}
