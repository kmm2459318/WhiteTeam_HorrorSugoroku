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
    public AudioSource elevatorSound; // ★ 追加
    Vector3 Rot;

    private void Start()
    {
        elevatorCanvas.SetActive(false);
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