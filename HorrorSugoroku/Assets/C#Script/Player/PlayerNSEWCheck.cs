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
            //Debug.Log("�O�i�s��");
        }
        //masuCheck = false;
        //Debug.Log("no");
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "masu")
        {
            masuCheck = true;
            //Debug.Log("�O�i�\");
            masuPos = collision.gameObject.transform.position;
        }
        //masuCheck = true;
        //Debug.Log("OK");
    }
  
}
