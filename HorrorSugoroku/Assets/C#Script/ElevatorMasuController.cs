using UnityEngine;

public class ElevatorMasuController : MonoBehaviour
{
    public GameObject Player;
    public GameObject elevatorCanvas;
    public GameObject Camera;
    public BreakerController breakerController;
    public PlayerSaikoro playerSaikoro;
    public CameraController cameraController;
    public ElevatorIdou elevatorIdou;
    public Option option;
    public AudioSource elevatorSound; // ★ 追加
    public int Floor = 0;

    private void Start()
    {
        elevatorCanvas.SetActive(false);
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            elevatorIdou.playerOn = true;
            elevatorIdou.elevatorFloor = Floor;

            // ★ 音声を再生
            if (elevatorSound != null)
            {
                elevatorSound.Play();
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            elevatorIdou.playerOn = false;
        }
    }
}