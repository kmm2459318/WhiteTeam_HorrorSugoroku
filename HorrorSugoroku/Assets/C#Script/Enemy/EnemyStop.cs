using UnityEngine;

public class EnemyStop : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject ENorth;
    public GameObject EWest;
    public GameObject EEast;
    public GameObject ESouth;
    private bool EN = false; // ìGÇÃìåêºìÏñk
    private bool EW = false;
    private bool EE = false;
    private bool ES = false;
    private Transform Smasu;
    private Transform Nmasu;
    private Transform Wmasu;
    private Transform Emasu;
    Vector3 ms = GameObject.Find("masu").transform.position;
    public bool rideMasu = false;

    void Start()
    {
        
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
            Debug.Log("É}ÉXÇ…èÊÇ¡ÇΩ");
            rideMasu = true;
        }
        else
        {
            rideMasu = false;
        }
    }
}
