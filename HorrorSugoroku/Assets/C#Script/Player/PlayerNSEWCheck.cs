using UnityEngine;

public class PlayerNSEWCheck : MonoBehaviour
{
    public bool masuCheck = false;
    public Vector3 masuPos;

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
            //Debug.Log("前進不可");
        }
        //masuCheck = false;
        //Debug.Log("no");
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "masu")
        {
            masuCheck = true;
            //Debug.Log("前進可能");
            masuPos = collision.gameObject.transform.position;
        }
        //masuCheck = true;
        //Debug.Log("OK");
    }
  
}
