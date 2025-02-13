using SmoothigTransform;
using UnityEngine;

public class EnemyTransferManager : MonoBehaviour
{
    public GameObject currentEnemyModel; // 現在のエネミーモデル
    public GameObject newEnemyModelPrefab; // 新しいエネミーモデルのプレハブ
    public GameManager gameManager; // GameManagerへの参照

    public void TransferEnemySettings()
    {
        if (currentEnemyModel == null)
        {
            Debug.LogError("currentEnemyModel is not assigned.");
            return;
        }

        if (newEnemyModelPrefab == null)
        {
            Debug.LogError("newEnemyModelPrefab is not assigned.");
            return;
        }

        // 新しいエネミーモデルをインスタンス化
        GameObject newEnemyModel = Instantiate(newEnemyModelPrefab, currentEnemyModel.transform.position, currentEnemyModel.transform.rotation);
        newEnemyModel.SetActive(true);

        // 元のエネミーから新しいエネミーに設定を移行
        EnemySaikoro oldEnemySaikoro = currentEnemyModel.GetComponent<EnemySaikoro>();
        EnemySaikoro newEnemySaikoro = newEnemyModel.GetComponent<EnemySaikoro>();
        if (newEnemySaikoro != null)
        {
            newEnemySaikoro.player = oldEnemySaikoro.player;
            newEnemySaikoro.wallLayer = oldEnemySaikoro.wallLayer;
            newEnemySaikoro.discoveryBGM = oldEnemySaikoro.discoveryBGM;
            newEnemySaikoro.undetectedBGM = oldEnemySaikoro.undetectedBGM;
            newEnemySaikoro.footstepSound = oldEnemySaikoro.footstepSound;
            newEnemySaikoro.enemyController = oldEnemySaikoro.enemyController;
            newEnemySaikoro.gameManager = oldEnemySaikoro.gameManager;
            newEnemySaikoro.enemyLookAtPlayer = oldEnemySaikoro.enemyLookAtPlayer;
            newEnemySaikoro.playerCloseMirror = oldEnemySaikoro.playerCloseMirror;
            newEnemySaikoro.mokushi = oldEnemySaikoro.mokushi;
            newEnemySaikoro.idoukagen = oldEnemySaikoro.idoukagen;
            newEnemySaikoro.skill1 = oldEnemySaikoro.skill1;
            newEnemySaikoro.skill2 = oldEnemySaikoro.skill2;
            newEnemySaikoro.isTrapped = oldEnemySaikoro.isTrapped;

            // アニメーションや状態を移行
            newEnemySaikoro.SetAnimationState(oldEnemySaikoro.GetCurrentAnimationState());
        }

        // EnemyControllerの設定を移行
        EnemyController oldEnemyController = currentEnemyModel.GetComponent<EnemyController>();
        EnemyController newEnemyController = newEnemyModel.GetComponent<EnemyController>();
        if (newEnemyController != null)
        {
            newEnemyController.gameManager = oldEnemyController.gameManager;
            newEnemyController.enemySaikoro = newEnemySaikoro;
        }

        // EnemyLookAtPlayerの設定を移行
        EnemyLookAtPlayer oldEnemyLookAtPlayer = currentEnemyModel.GetComponent<EnemyLookAtPlayer>();
        EnemyLookAtPlayer newEnemyLookAtPlayer = newEnemyModel.GetComponent<EnemyLookAtPlayer>();
        if (newEnemyLookAtPlayer != null)
        {
            newEnemyLookAtPlayer.player = oldEnemyLookAtPlayer.player;
            newEnemyLookAtPlayer.wallLayer = oldEnemyLookAtPlayer.wallLayer;
        }

        // SmoothTransformの設定を移行
        SmoothTransform oldSmoothTransform = currentEnemyModel.GetComponent<SmoothTransform>();
        SmoothTransform newSmoothTransform = newEnemyModel.GetComponent<SmoothTransform>();
        if (newSmoothTransform != null)
        {
            newSmoothTransform.TargetPosition = oldSmoothTransform.TargetPosition;
            newSmoothTransform.TargetRotation = oldSmoothTransform.TargetRotation;
            newSmoothTransform.PosFact = oldSmoothTransform.PosFact;
            newSmoothTransform.RotFact = oldSmoothTransform.RotFact;
        }

        // currentEnemyModelを新しいエネミーモデルに更新
        currentEnemyModel = newEnemyModel;

        // GameManagerのenemySaikoro参照を更新
        gameManager.enemySaikoro = newEnemySaikoro;

        // 元のエネミーモデルを削除
        Destroy(oldEnemySaikoro.gameObject);

        Debug.Log("New enemy model setup complete and old enemy model destroyed.");
    }
}