using SmoothigTransform;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    public AudioSource elevatorSound; // ★ 追加
    Vector3 Rot;

    private void Start()
    {
        elevatorCanvas.SetActive(false);
    }
    private void Update()
    {
        Rot = Camera.transform.eulerAngles;

        if (breakerController.breaker && elevatorIdou.playerOn && playerSaikoro.idoutyu
            && ((Rot.y > 318f && Rot.y < 360f) || (Rot.y > 0f && Rot.y < 45f))
            && (Rot.x > -40f && Rot.x < 50f) && !option.isOptionOpen && !elevatorIdou.elevatorPanelOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("elevatorPanelOn");
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
                    Debug.Log("現在B1F");
                    imageB1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                    textB1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                }
                else if (Player.transform.position.y < 3f)
                {
                    Debug.Log("現在1F");
                    image1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                    text1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                }
                else
                {
                    Debug.Log("現在2F");
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
            elevatorIdou.playerOn = true;

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