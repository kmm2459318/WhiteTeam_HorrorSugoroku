using SmoothigTransform;
using UnityEngine;

public class EnemyTransferManager : MonoBehaviour
{
    public GameObject currentEnemyModel; // ���݂̃G�l�~�[���f��
    public GameObject newEnemyModelPrefab; // �V�����G�l�~�[���f���̃v���n�u
    public GameManager gameManager; // GameManager�ւ̎Q��

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

        // �V�����G�l�~�[���f�����C���X�^���X��
        GameObject newEnemyModel = Instantiate(newEnemyModelPrefab, currentEnemyModel.transform.position, currentEnemyModel.transform.rotation);
        newEnemyModel.SetActive(true);

        // ���̃G�l�~�[����V�����G�l�~�[�ɐݒ���ڍs
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

            // �A�j���[�V�������Ԃ��ڍs
            newEnemySaikoro.SetAnimationState(oldEnemySaikoro.GetCurrentAnimationState());
        }

        // EnemyController�̐ݒ���ڍs
        EnemyController oldEnemyController = currentEnemyModel.GetComponent<EnemyController>();
        EnemyController newEnemyController = newEnemyModel.GetComponent<EnemyController>();
        if (newEnemyController != null)
        {
            newEnemyController.gameManager = oldEnemyController.gameManager;
            newEnemyController.enemySaikoro = newEnemySaikoro;
        }

        // EnemyLookAtPlayer�̐ݒ���ڍs
        EnemyLookAtPlayer oldEnemyLookAtPlayer = currentEnemyModel.GetComponent<EnemyLookAtPlayer>();
        EnemyLookAtPlayer newEnemyLookAtPlayer = newEnemyModel.GetComponent<EnemyLookAtPlayer>();
        if (newEnemyLookAtPlayer != null)
        {
            newEnemyLookAtPlayer.player = oldEnemyLookAtPlayer.player;
            newEnemyLookAtPlayer.wallLayer = oldEnemyLookAtPlayer.wallLayer;
        }

        // SmoothTransform�̐ݒ���ڍs
        SmoothTransform oldSmoothTransform = currentEnemyModel.GetComponent<SmoothTransform>();
        SmoothTransform newSmoothTransform = newEnemyModel.GetComponent<SmoothTransform>();
        if (newSmoothTransform != null)
        {
            newSmoothTransform.TargetPosition = oldSmoothTransform.TargetPosition;
            newSmoothTransform.TargetRotation = oldSmoothTransform.TargetRotation;
            newSmoothTransform.PosFact = oldSmoothTransform.PosFact;
            newSmoothTransform.RotFact = oldSmoothTransform.RotFact;
        }

        // currentEnemyModel��V�����G�l�~�[���f���ɍX�V
        currentEnemyModel = newEnemyModel;

        // GameManager��enemySaikoro�Q�Ƃ��X�V
        gameManager.enemySaikoro = newEnemySaikoro;

        // ���̃G�l�~�[���f�����폜
        Destroy(oldEnemySaikoro.gameObject);

        Debug.Log("New enemy model setup complete and old enemy model destroyed.");
    }
}