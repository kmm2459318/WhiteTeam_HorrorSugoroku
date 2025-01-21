using UnityEngine;
using UnityEngine.UI;

public class PlayerSaikorocopy : MonoBehaviour
{
    private EnemySaikoro targetScript;
    private int sai = 1;
    private bool saikorotyu = false;
    private bool idoutyu = false;
    private float delta = 0;
    private int ii = 0;
    private int detame = 0;
    private bool PN = false; // 北に移動可能
    private bool PW = false; // 西に移動可能
    private bool PE = false; // 東に移動可能
    private bool PS = false; // 南に移動可能

    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    public GameObject saikoro;
    public GameObject Player;
    public GameObject PNorth;
    public GameObject PWest;
    public GameObject PEast;
    public GameObject PSouth;
    Image image;

    private float moveSpeed = 5f;
    private float rotationSpeed = 5f; // 回転スピード
    private Vector3 targetPosition;
    private bool isMoving = false;
    private Quaternion targetRotation; // 目標の回転
    private bool isRotating = false;
    private Vector3 currentForward = Vector3.forward; // 現在の正面方向

    [System.Obsolete]
    void Start()
    {
        targetScript = FindObjectOfType<EnemySaikoro>();
        image = saikoro.GetComponent<Image>();
        saikoro.SetActive(false);
        targetPosition = Player.transform.position;
        targetRotation = Player.transform.rotation;

        if (targetScript == null)
        {
            Debug.LogError("EnemySaikoro not found in the scene.");
        }
    }

    void Update()
    {
        Vector3 pos = Player.transform.position;
        PN = PNorth.GetComponent<PlayerNSEWCheck>().masuCheck;
        PW = PWest.GetComponent<PlayerNSEWCheck>().masuCheck;
        PE = PEast.GetComponent<PlayerNSEWCheck>().masuCheck;
        PS = PSouth.GetComponent<PlayerNSEWCheck>().masuCheck;

        switch (sai)
        {
            case 1: image.sprite = s1; break;
            case 2: image.sprite = s2; break;
            case 3: image.sprite = s3; break;
            case 4: image.sprite = s4; break;
            case 5: image.sprite = s5; break;
            case 6: image.sprite = s6; break;
        }

        if (Input.GetKeyDown(KeyCode.E) || saikorotyu)
        {
            saikorotyu = true;
            delta += Time.deltaTime;

            if (delta > 0.1f)
            {
                delta = 0f;

                if (ii < 7)
                {
                    sai = Random.Range(1, 7);
                    saikoro.SetActive(true);
                    ii++;
                }
                else
                {
                    sai = Random.Range(1, 7);
                    detame = sai;

                    targetScript.RollEnemyDice(sai);

                    ii = 0;
                    saikorotyu = false;
                    idoutyu = true;
                }
            }
        }

        if (idoutyu && !isMoving && !isRotating)
        {
            HandleRotation();
            HandleMovement();
        }

        SmoothMovement();
        SmoothRotation();
    }

    private void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotatePlayer(-90); // 左回転
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            RotatePlayer(90); // 右回転
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.W) && PN)
        {
            SetMoveTarget(currentForward);
        }
    }

    private void RotatePlayer(float angle)
    {
        currentForward = Quaternion.Euler(0, angle, 0) * currentForward; // 新しい正面方向を計算
        targetRotation = Quaternion.LookRotation(currentForward); // プレイヤーの目標回転を設定
        isRotating = true;
    }

    private void SetMoveTarget(Vector3 direction)
    {
        targetPosition = Player.transform.position + direction * 2.0f; // 2マス移動
        isMoving = true;
        sai--;

        if (sai < 1)
        {
            idoutyu = false;
            saikoro.SetActive(false);
        }
    }

    private void SmoothMovement()
    {
        if (isMoving)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(Player.transform.position, targetPosition) < 0.01f)
            {
                Player.transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private void SmoothRotation()
    {
        if (isRotating)
        {
            Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(Player.transform.rotation, targetRotation) < 1f)
            {
                Player.transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }
}
