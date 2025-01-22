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

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "masu")
        {
            masuCheck = false;
            Debug.Log("no");
        }
        //masuCheck = false;
        //Debug.Log("no");
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "masu")
        {
            masuCheck = true;
            Debug.Log("OK");
        }
        //masuCheck = true;
        //Debug.Log("OK");
    }
  
}
