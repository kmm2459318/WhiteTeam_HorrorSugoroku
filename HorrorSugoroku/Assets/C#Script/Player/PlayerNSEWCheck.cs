using UnityEngine;

public class PlayerNSEWCheck : MonoBehaviour
{
    public bool masuCheck = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "masu")
        {
            masuCheck = true;
            //Debug.Log("OK");
        }
        //masuCheck = true;
        //Debug.Log("OK");
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "masu")
        {
            masuCheck = false;
            //Debug.Log("no");
        }
        //masuCheck = false;
        //Debug.Log("no");
    }
}
