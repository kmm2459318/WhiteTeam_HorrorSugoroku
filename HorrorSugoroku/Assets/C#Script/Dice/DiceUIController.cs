using UnityEngine;

public class DiceUIController : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasBeenThrown = false;
    private float timeSinceThrown = 0f;
    public int result = 0;
    public Transform cameraTransform;
    public PlayerSaikoro player;

    [SerializeField] private Transform[] faces;
    [SerializeField] private float stopCheckDelay = 1f;
    [SerializeField] private float stopThreshold = 0.05f;
    [SerializeField] private float throwForceMultiplier = 2f;
    [SerializeField] private float gravityStrength = 9.8f;
    [SerializeField] private GameObject groundPrefab;

    private GameObject ground;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CreateDynamicGround();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasBeenThrown)
        {
            ThrowDice();
        }

        if (hasBeenThrown)
        {
            timeSinceThrown += Time.deltaTime;
            if (timeSinceThrown >= stopCheckDelay && rb.linearVelocity.magnitude < stopThreshold && rb.angularVelocity.magnitude < stopThreshold)
            {
                result = GetLowestFace();
                if (result != -1)
                {
                    Debug.Log($"出た目: {result}");
                    player.DiceAfter(result);
                    ResetDiceState();
                }
            }
        }

        // カメラの向きに合わせて重力を変更
        Physics.gravity = cameraTransform.forward * gravityStrength;

        // カメラの向きに合わせて床を再配置
        if (ground)
        {
            ground.transform.position = cameraTransform.position + cameraTransform.forward * 2f;
            ground.transform.up = -cameraTransform.forward;
        }
    }

    private void ThrowDice()
    {
        hasBeenThrown = true;
        timeSinceThrown = 0f;
        rb.isKinematic = false;
        Vector3 throwForce = cameraTransform.forward * throwForceMultiplier + Random.insideUnitSphere;
        rb.AddForce(throwForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
    }

    private int GetLowestFace()
    {
        if (faces == null || faces.Length == 0) return -1;

        Transform lowestFace = faces[0];
        float minDistance = Vector3.Dot(faces[0].position - transform.position, cameraTransform.forward);

        foreach (Transform face in faces)
        {
            float distance = Vector3.Dot(face.position - transform.position, cameraTransform.forward);
            if (distance < minDistance)
            {
                minDistance = distance;
                lowestFace = face;
            }
        }

        return int.TryParse(lowestFace.name, out int faceValue) ? faceValue : -1;
    }

    private void ResetDiceState()
    {
        hasBeenThrown = false;
    }

    private void CreateDynamicGround()
    {
        ground = Instantiate(groundPrefab);
        ground.transform.position = cameraTransform.position + cameraTransform.forward * 2f;
        ground.transform.up = -cameraTransform.forward;
    }
}
