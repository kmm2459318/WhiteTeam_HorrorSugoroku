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
    public PlayerSaikoro player;
    [SerializeField] private Transform[] faces;
    private float stopCheckDelay = 1f;
    private float stopThreshold = 0.05f;
    private float throwForceMultiplier = 0.8f;
    [SerializeField] SmoothTransform smo;

    public DiceRangeManager diceRangeManager;
    private Transform parentTransform;
    private Vector3 initialPosition;

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
        // サイコロ投げる処理
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
                        player.DiceAfter(result);

                        // **DiceRotationを有効化**
                        DiceRotation diceRotation = GetComponent<DiceRotation>();
                        if (diceRotation != null)
                        {
                            diceRotation.StartRotation();
                        }
                    }
                }
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
