using SmoothigTransform;
using UnityEngine;

public class EnemyTransferManager : MonoBehaviour
{
    public GameObject currentEnemyModel; // 現在のエネミーモデル
    public GameObject newEnemyModelPrefab; // 新しいエネミーモデルのプレハブ
    public GameManager gameManager; // GameManagerへの参照

    public void TransferEnemySettings()
    {
        currentEnemyModel.SetActive(false);
        newEnemyModelPrefab.SetActive(true);
    }
}