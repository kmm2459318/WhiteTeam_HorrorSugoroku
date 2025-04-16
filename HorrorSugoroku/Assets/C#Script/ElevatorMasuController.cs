using SmoothigTransform;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorMasuController : MonoBehaviour
{
    public GameObject Player;
    public GameObject elevatorCanvas;
    public GameObject Camera;
    public Image image2F;
    public Image image1F;
    public Image imageB1F;
    public TextMeshProUGUI text2F;
    public TextMeshProUGUI text1F;
    public TextMeshProUGUI textB1F;
    public BreakerController breakerController;
    public PlayerSaikoro playerSaikoro;
    public CameraController cameraController;
    public ElevatorIdou elevatorIdou;
    public Option option;
    public bool playerOn = false;
    Vector3 Rot;

    private void Start()
    {
        elevatorCanvas.SetActive(false);
    }

    private void Update()
    {
        Rot = Camera.transform.eulerAngles;

        if (breakerController.breaker && playerOn && playerSaikoro.idoutyu
            && ((Rot.y > 318f && Rot.y < 360f) || (Rot.y > 0f && Rot.y < 45f))
            && (Rot.x > -40f && Rot.x < 50f) && !option.isOptionOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("elevator‹N“®");
                elevatorIdou.elevatorPanelOn = true;
                elevatorCanvas.SetActive(true);
                cameraController.isMouseLocked = false;
                cameraController.SetOptionOpen(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                image2F.color = new Color(255, 255, 255, 255);
                text2F.color = new Color(255, 255, 255, 255);
                image1F.color = new Color(255, 255, 255, 255);
                text1F.color = new Color(255, 255, 255, 255);
                imageB1F.color = new Color(255, 255, 255, 255);
                textB1F.color = new Color(255, 255, 255, 255);

                if (Player.transform.position.y < 0f)
                {
                    Debug.Log("Ś»ŤÝB1F");
                    imageB1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                    textB1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                }
                else if (Player.transform.position.y < 3f)
                {
                    Debug.Log("Ś»ŤÝ1F");
                    image1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                    text1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                }
                else
                {
                    Debug.Log("Ś»ŤÝ2F");
                    image2F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                    text2F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
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