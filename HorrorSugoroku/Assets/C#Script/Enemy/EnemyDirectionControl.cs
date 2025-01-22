using UnityEngine;

public class EnemyDirectionControl : MonoBehaviour
{
    public GameObject enemyWest;
    public GameObject enemyEast;
    public GameObject enemySouth;
    public GameObject enemyNorth;

    private bool canMoveWest = true;
    private bool canMoveEast = true;
    private bool canMoveSouth = true;
    private bool canMoveNorth = true;

    void Update()
    {
        // 各方向の移動可能状態をチェック
        if (canMoveWest)
        {
            // 西方向に移動する処理
        }
        if (canMoveEast)
        {
            // 東方向に移動する処理
        }
        if (canMoveSouth)
        {
            // 南方向に移動する処理
        }
        if (canMoveNorth)
        {
            // 北方向に移動する処理
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (other.gameObject == enemyWest)
            {
                canMoveWest = false;
            }
            else if (other.gameObject == enemyEast)
            {
                canMoveEast = false;
            }
            else if (other.gameObject == enemySouth)
            {
                canMoveSouth = false;
            }
            else if (other.gameObject == enemyNorth)
            {
                canMoveNorth = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (other.gameObject == enemyWest)
            {
                canMoveWest = true;
            }
            else if (other.gameObject == enemyEast)
            {
                canMoveEast = true;
            }
            else if (other.gameObject == enemySouth)
            {
                canMoveSouth = true;
            }
            else if (other.gameObject == enemyNorth)
            {
                canMoveNorth = true;
            }
        }
    }
}