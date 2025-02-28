using UnityEngine;

public class EnemyStop : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject ENorth;
    public GameObject EWest;
    public GameObject EEast;
    public GameObject ESouth;
    private bool EN = false; // �G�̓�����k
    private bool EW = false;
    private bool EE = false;
    private bool ES = false;
    private Transform Smasu;
    private Transform Nmasu;
    private Transform Wmasu;
    private Transform Emasu;
    Vector3 ms = GameObject.Find("masu").transform.position;
    public bool rideMasu = false;
    private Animator animator; // Animator�R���|�[�l���g

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator�R���|�[�l���g���擾
    }

    void Update()
    {
        if (ENorth == null) Debug.LogError("ENorth is not assigned.");
        if (EWest == null) Debug.LogError("EWest is not assigned.");
        if (EEast == null) Debug.LogError("EEast is not assigned.");
        if (ESouth == null) Debug.LogError("ESouth is not assigned.");

        EN = ENorth.GetComponent<PlayerNSEWCheck>().masuCheck;
        EW = EWest.GetComponent<PlayerNSEWCheck>().masuCheck;
        EE = EEast.GetComponent<PlayerNSEWCheck>().masuCheck;
        ES = ESouth.GetComponent<PlayerNSEWCheck>().masuCheck;
        Nmasu = ENorth.GetComponent<PlayerCloseMass>().GetClosestObject();
        Wmasu = EWest.GetComponent<PlayerCloseMass>().GetClosestObject();
        Emasu = EEast.GetComponent<PlayerCloseMass>().GetClosestObject();
        Smasu = ESouth.GetComponent<PlayerCloseMass>().GetClosestObject();

        if (!gameManager.isPlayerTurn)
        {

        }

        if (((ms.x + 0.1f > this.transform.position.x && ms.x - 0.1f < this.transform.position.x) &&
            (ms.z + 0.1f > this.transform.position.z && ms.z - 0.1f < this.transform.position.z)))
        {
            Debug.Log("�}�X�ɏ����");
            rideMasu = true;
            animator.SetBool("isIdle", true); // Idle�A�j���[�V�������Đ�
            animator.SetBool("is Running", false); // Run�A�j���[�V�������~
        }
        else
        {
            rideMasu = false;
            animator.SetBool("isIdle", false); // Idle�A�j���[�V�������~
            animator.SetBool("is Running", true); // Run�A�j���[�V�������Đ�
        }
    }
}