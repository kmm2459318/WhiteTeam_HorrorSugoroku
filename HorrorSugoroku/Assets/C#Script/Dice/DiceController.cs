using UnityEngine;
using SmoothigTransform;

public class DiceController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    private bool isStopped = false;
    private bool hasBeenThrown = false;
    private float timeSinceThrown = 0f;
    public int result = 0;
    public GameManager gameManager; // GameManagerへの参照
    public PlayerSaikoro player;
    [SerializeField] private Transform[] faces;
    private float stopCheckDelay = 1f;
    private float stopThreshold = 0.05f;
    private float throwForceMultiplier = 0.8f;
    [SerializeField] SmoothTransform smo;

    public DiceRangeManager diceRangeManager;
    private Transform parentTransform;
    private Vector3 initialPosition;
    private Vector3 targetPosition; // サイコロ移動後の位置
    private bool moveToTarget = false; // 移動フラグ
    private float moveSpeed = 2f; // 移動速度

    private int minDiceValue = 1;
    private int maxDiceValue = 6;
    private bool legButtonEffect = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        diceRangeManager = FindObjectOfType<DiceRangeManager>();
        diceRangeManager.SetDiceRollRange(1, 6);
        diceRangeManager.EnableTransformRoll();
        parentTransform = transform.parent;
        initialPosition = transform.position;
        transform.position = new Vector3(parentTransform.position.x, initialPosition.y + 0.5f, parentTransform.position.z);
    }

    void Update()
    {
        if (player.saikorotyu)
        {
            if (Input.GetKey(KeyCode.Space) && !hasBeenThrown)
            {
                smo.enabled = true;
                smo.PosFact = 0.1f;
                isHeld = true;
                isStopped = false;
                hasBeenThrown = false;
                rb.isKinematic = true;
                transform.position = new Vector3(parentTransform.position.x, 5f, parentTransform.position.z);
            }

            if (Input.GetKeyUp(KeyCode.Space) && isHeld)
            {
                isHeld = false;
                hasBeenThrown = true;
                isStopped = false;
                timeSinceThrown = 0f;

                rb.isKinematic = false;
                smo.enabled = false;

                Vector3 throwForce = new Vector3(Random.Range(-2f, 2f), 10f, Random.Range(-2f, 2f)) * throwForceMultiplier;
                rb.AddForce(throwForce, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 500f);
            }

            if (isHeld)
            {
                transform.position = new Vector3(parentTransform.position.x, 5f, parentTransform.position.z);
                float rotationSpeed = 300f;
                transform.Rotate(Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime,
                                 Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime,
                                 Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime);
            }

            if (hasBeenThrown)
            {
                timeSinceThrown += Time.deltaTime;

                if (timeSinceThrown >= stopCheckDelay && !isStopped)
                {
                    if (rb.linearVelocity.magnitude < stopThreshold && rb.angularVelocity.magnitude < stopThreshold)
                    {
                        isStopped = true;
                        result = GetTopFace();
                        if (result != -1)
                        {
                            Debug.Log($"出た目: {result}");
                            ResetDiceState();
                            player.DiceAfter(result);

                            // **投げた後の移動処理開始**
                            targetPosition = parentTransform.position + new Vector3(-6.5f, 0f, 3f);
                            moveToTarget = true;
                        }
                    }
                }
            }
        }

        // サイコロを画面左上へ移動
        if (moveToTarget)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // ある程度近づいたら移動終了
            if (Mathf.Abs(targetPosition.x - transform.position.x) < 0.1f 
                && Mathf.Abs(targetPosition.z - transform.position.z) < 0.1f)
            {
                transform.position = targetPosition;
                moveToTarget = false;
            }
        }
    }

    private int GetTopFace()
    {
        if (faces == null || faces.Length == 0) return -1;

        Transform topFace = null;
        float maxY = float.MinValue;

        foreach (Transform face in faces)
        {
            if (face.position.y > maxY)
            {
                maxY = face.position.y;
                topFace = face;
            }
        }

        int faceValue = topFace != null ? int.Parse(topFace.name) : -1;

        if (legButtonEffect && faceValue > 3)
        {
            faceValue = Random.Range(1, 4);
        }

        return faceValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isStopped = false;
    }

    private void ResetDiceState()
    {
        hasBeenThrown = false;
        isHeld = false;
        isStopped = false;
    }

    public void SetDiceRollRange(int min, int max)
    {
        minDiceValue = min;
        maxDiceValue = max;
    }

    public void SetLegButtonEffect(bool isActive)
    {
        legButtonEffect = isActive;
    }
}
