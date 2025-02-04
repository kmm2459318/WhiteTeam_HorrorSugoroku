using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro�p�̃^�[�����\��
    public TMP_Text turnIndicatorText; // �V�����^�[���\���p�̃e�L�X�g
    public bool isPlayerTurn = true; // �v���C���[�̃^�[�����ǂ����������t���O

    public PlayerSaikoro playerSaikoro;
    public EnemySaikoro enemySaikoro;

    public GameObject currentEnemyModel; // ���݂̃G�l�~�[���f��
    public GameObject newEnemyPrefab; // �V�����G�l�~�[���f���̃v���t�@�u

    private int playerTurnCount = 0; // �v���C���[�̃^�[�������J�E���g����ϐ�

    private void Start()
    {
        UpdateTurnText(); // �����^�[���\��
        playerSaikoro.StartRolling(); // �v���C���[�̃^�[�����J�n
    }

    public void NextTurn()
    {
        isPlayerTurn = !isPlayerTurn; // �^�[����؂�ւ���
        UpdateTurnText(); // UI�̃e�L�X�g���X�V

        if (isPlayerTurn)
        {
            playerTurnCount++; // �v���C���[�̃^�[�������J�E���g
            Debug.Log("Player Turn Count: " + playerTurnCount); // �f�o�b�O���O

            // �v���C���[�̃^�[����5�^�[���ڂɂȂ�����G�l�~�[���f����ύX
            if (playerTurnCount == 5)
            {
                ChangeEnemyModel();
            }

            playerSaikoro.StartRolling();
        }
        else
        {
            StartCoroutine(enemySaikoro.EnemyTurn());
        }
    }

    private void UpdateTurnText()
    {
        if (turnIndicatorText != null)
        {
            if (isPlayerTurn)
            {
                turnIndicatorText.text = "PlayerTurn"; // �v���C���[�̃^�[���\��
            }
            else
            {
                turnIndicatorText.text = "EnemyTurn"; // �G�l�~�[�̃^�[���\��
            }
        }
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }

    private void ChangeEnemyModel()
    {
        if (currentEnemyModel != null && newEnemyPrefab != null)
        {
            // ���݂̃G�l�~�[�̈ʒu�Ɖ�]��ۑ�
            Vector3 currentEnemyPosition = currentEnemyModel.transform.position;
            Quaternion currentEnemyRotation = currentEnemyModel.transform.rotation;

            // �V�����G�l�~�[���f���̃C���X�^���X�𐶐�
            GameObject newEnemyModel = Instantiate(newEnemyPrefab, currentEnemyPosition, currentEnemyRotation);

            // �V�������f���̈ʒu�ƃ����_���[�̏�Ԃ��m�F
            Debug.Log("New Enemy Model Position: " + newEnemyModel.transform.position);
            Renderer[] renderers = newEnemyModel.GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0)
            {
                foreach (Renderer renderer in renderers)
                {
                    Debug.Log("Renderer " + renderer.name + " Enabled: " + renderer.enabled);
                    renderer.enabled = true; // �����_���[��L���ɂ���
                }
            }
            else
            {
                Debug.LogError("New Enemy Model does not have any Renderer components!");
            }

            // ���݂̃G�l�~�[���f�����폜
            Destroy(currentEnemyModel);

            Debug.Log("Enemy model has been changed and positioned.");
        }
        else
        {
            Debug.LogError("Enemy models are not assigned!");
        }
    }
}