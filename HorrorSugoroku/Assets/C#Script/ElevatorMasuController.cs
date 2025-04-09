using SmoothigTransform;
using UnityEngine;

public class ElevatorMasuController : MonoBehaviour
{
    public GameObject Pleyer;
    public GameObject masu1F;
    public GameObject masu2F;
    public PlayerSaikoro playerSaikoro;
    [SerializeField] SmoothTransform PSm;
    public bool playerOn = false;
    private bool idou = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (idou)
        {
            PSm.PosFact = 0.3f;
            idou = false;
        }

        if (playerOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PSm.PosFact = 0f;
                idou = true;
                playerSaikoro.sai--;

                if (Pleyer.transform.position.y < 3f)
                {
                    Debug.Log("上へ参ります。");
                    PSm.TargetPosition.y = masu2F.transform.position.y + 1.17f;
                }
                else
                {
                    Debug.Log("下へ参ります。");
                    PSm.TargetPosition.y = masu1F.transform.position.y + 1.17f;
                }
            }
        }
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
