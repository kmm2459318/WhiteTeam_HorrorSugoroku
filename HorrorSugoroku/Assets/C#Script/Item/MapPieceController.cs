using UnityEngine;

public class MapPieceController : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 17f/18f, 0));
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.MpPlus();
            Destroy(this.gameObject);
        }
    }
}