using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalMasuController : MonoBehaviour
{
    public GameManager gameManager;
    private bool playerStay = false;

    void Update()
    {
        if (gameManager.mapPiece >= 5 && playerStay)
        {
            Debug.Log("�N���A����B");
            SceneManager.LoadScene("Gameclear");
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStay = true;
        }
        else
        {
            playerStay = false;
        }
    }
}
