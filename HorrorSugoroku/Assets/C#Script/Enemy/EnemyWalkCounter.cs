using UnityEngine;

public class EnemyWalkCounter : MonoBehaviour
{
    public int walkMasu = 0;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "masu")
        {
            walkMasu++;
            Debug.Log("ï‡Ç¢ÇΩÉ}ÉXÅF" + walkMasu);
        }
    }
}
