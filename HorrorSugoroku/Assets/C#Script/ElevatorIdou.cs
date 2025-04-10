using SmoothigTransform;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ElevatorIdou : MonoBehaviour
{
    public GameObject Player;
    public GameObject masu1F;
    public GameObject masu2F;
    public GameObject masuB1F;
    public GameObject elevatorCanvas;
    public PlayerSaikoro playerSaikoro;
    public BreakerController breakerController;
    [SerializeField] SmoothTransform PSm;
    public bool playerOn = false;
    private bool idou = false;
    private bool idou1 = false;
    private Vector3 ikisaki = Vector3.zero;

    void Update()
    {
        if (idou)
        {
            PSm.PosFact = 0.3f;
            idou = false;
        }

        if (idou1)
        {
            idou1 = false;
            idou = true;
        }
    }

    public void Idou2F()
    {
        Debug.Log("。");
        if (Player.transform.position.y < 3f)
        {
            IdouSystem();
            Debug.Log("2Fへ参ります。");
            PSm.TargetPosition = masu2F.transform.position + new Vector3(0, 1.17f, 0);
            ikisaki = masu2F.transform.position + new Vector3(0, 1.17f, 0);
        }
    }

    public void Idou1F()
    {
        Debug.Log("。");
        if (Player.transform.position.y < 0f || Player.transform.position.y > 3f)
        {
            IdouSystem();
            Debug.Log("1Fへ参ります。");
            PSm.TargetPosition = masu1F.transform.position + new Vector3(0, 1.17f, 0);
            ikisaki = masu2F.transform.position + new Vector3(0, 1.17f, 0);
        }
    }

    public void IdouB1F()
    {
        Debug.Log("。");
        if (Player.transform.position.y > 0f)
        {
            IdouSystem();
            Debug.Log("B1Fへ参ります。");
            PSm.TargetPosition = masuB1F.transform.position + new Vector3(0, 1.17f, 0);
            ikisaki = masu2F.transform.position + new Vector3(0, 1.17f, 0);
        }
    }
    
    void IdouSystem()
    {
        PSm.PosFact = 0f;
        idou1 = true;
        elevatorCanvas.SetActive(false);
    }
}
