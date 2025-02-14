using SmoothigTransform;
using UnityEngine;

public class EnemyTransferManager : MonoBehaviour
{
    public GameObject currentEnemyModel; // ���݂̃G�l�~�[���f��
    public GameObject newEnemyModelPrefab; // �V�����G�l�~�[���f���̃v���n�u
    public GameManager gameManager; // GameManager�ւ̎Q��

    public void TransferEnemySettings()
    {
        currentEnemyModel.SetActive(false);
        newEnemyModelPrefab.SetActive(true);
    }
}