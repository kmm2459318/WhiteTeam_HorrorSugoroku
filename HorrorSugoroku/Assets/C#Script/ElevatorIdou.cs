using SmoothigTransform;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ElevatorIdou : MonoBehaviour
{
    public GameObject Player;
    public GameObject masu1F;
    public GameObject masu2F;
    public GameObject masuB1F;
    public GameObject masuElevator;
    public GameObject elevatorCanvas;
    public PlayerSaikoro playerSaikoro;
    public BreakerController breakerController;
    public CameraController cameraController;
    public ElevatorController elevatorController;
    public Option option;
    [SerializeField] SmoothTransform PSm;
    public Image image2F;
    public Image image1F;
    public Image imageB1F;
    public TextMeshProUGUI text2F;
    public TextMeshProUGUI text1F;
    public TextMeshProUGUI textB1F;
    public bool elevatorPanelOn = false;
    public bool playerOn = false;
    public bool idou = false;
    public int elevatorFloor = 1;

    void Update()
    {
        if (idou)
        {
            playerOn = false;
            Player.GetComponent<CapsuleCollider>().enabled = false;
        }

        if (elevatorController.isMoving)
        {
            PSm.TargetPosition = masuElevator.transform.position + new Vector3(0, 1.15f, 0);
        }
    }

    public void IdouHantei()
    {
        if (breakerController.breaker && playerOn && playerSaikoro.idoutyu
            && !option.isOptionOpen && !elevatorPanelOn)
        {
            Debug.Log("elevatorPanelOn");
            elevatorPanelOn = true;
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

            if (elevatorFloor == 0)
            {
                Debug.Log("現在B1F");
                imageB1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
                textB1F.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255);
            }
            else if (elevatorFloor == 1)
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

    public void NotIdou()
    {
        elevatorPanelOn = false;
        elevatorCanvas.SetActive(false);
        cameraController.isMouseLocked = true;
        cameraController.SetOptionOpen(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Idou2F()
    {
        Debug.Log("。");
        if (Player.transform.position.y < 3f)
        {
            Debug.Log("2Fへ参ります。");
            IdouSystem();
            StartCoroutine(ElevatorMove(2));
            //ikisaki = masu2F.transform.position + new Vector3(0, 1.17f, 0);
        }
    }

    public void Idou1F()
    {
        Debug.Log("。");
        if (Player.transform.position.y < 0f || Player.transform.position.y > 3f)
        {
            Debug.Log("1Fへ参ります。");
            IdouSystem();
            StartCoroutine(ElevatorMove(1));
            //ikisaki = masu2F.transform.position + new Vector3(0, 1.17f, 0);
        }
    }

    public void IdouB1F()
    {
        Debug.Log("。");
        if (Player.transform.position.y > 0f)
        {
            Debug.Log("B1Fへ参ります。");
            IdouSystem();
            StartCoroutine(ElevatorMove(0));
            //ikisaki = masu2F.transform.position + new Vector3(0, 1.17f, 0);
        }
    }

    IEnumerator ElevatorMove(int n)
    {
        //Debug.Log("llllll");
        StartCoroutine(elevatorController.ToggleDoors());
        yield return new WaitForSeconds(0.7f);
        PSm.TargetPosition = masuElevator.transform.position + new Vector3(0, 1.15f, 0);
        yield return new WaitForSeconds(0.3f);

        if (n == 2)
        {
            StartCoroutine(elevatorController.MoveToFloor(2));
            yield return new WaitWhile(() => elevatorController.isMoving == true);
            PSm.TargetPosition = masu2F.transform.position + new Vector3(0, 1.17f, 0);
            playerSaikoro.nextDarkMasu = masu2F.transform;
        }
        else if (n == 1)
        {
            StartCoroutine(elevatorController.MoveToFloor(1));
            yield return new WaitWhile(() => elevatorController.isMoving == true);
            PSm.TargetPosition = masu1F.transform.position + new Vector3(0, 1.17f, 0);
            playerSaikoro.nextDarkMasu = masu1F.transform;
        }
        else if (n == 0)
        {
            StartCoroutine(elevatorController.MoveToFloor(0));
            yield return new WaitWhile(() => elevatorController.isMoving == true);
            PSm.TargetPosition = masuB1F.transform.position + new Vector3(0, 1.17f, 0);
            playerSaikoro.nextDarkMasu = masuB1F.transform;
        }

        yield return new WaitForSeconds(1f);
        Player.GetComponent<CapsuleCollider>().enabled = true;
        idou = false;
        playerOn = true;
        StartCoroutine(elevatorController.ToggleDoors());
    }

    void IdouSystem()
    {
        //PSm.PosFact = 0f;
        idou = true;
        elevatorPanelOn = false;
        elevatorCanvas.SetActive(false);
        cameraController.isMouseLocked = true;
        cameraController.SetOptionOpen(false);
        Cursor.lockState = CursorLockMode.Locked;
        //Invoke(nameof(option.HideCursor), 0.02f);  // 0.02秒遅延して確実にカーソルを非表示
    }
}
