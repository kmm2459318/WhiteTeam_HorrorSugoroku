using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText; // TextMeshPro�p�̃^�[�����\��
    public TMP_Text turnIndicatorText; // �V�����^�[���\���p�̃e�L�X�g
    public bool isPlayerTurn = true; // �v���C���[�̃^�[�����ǂ����������t���O
    public bool EnemyCopyOn = false;
    public int enemyTurnFinCount = 0;
    public int mapPiece = 0;

    public PlayerSaikoro playerSaikoro; 
    public EnemySaikoro enemySaikoro;
    public EnemySaikoro enemyCopySaikoro;

    public GameObject currentEnemyModel; // ���݂̃G�l�~�[���f��
    public GameObject newEnemyPrefab; // �V�����G�l�~�[���f���̃v���t�@�u
    public GameObject EnemyCopy;

    private int playerTurnCount = 0; // �v���C���[�̃^�[�������J�E���g����ϐ�

    private void Start()
    {
        UpdateTurnText(); // �����^�[���\��
        playerSaikoro.StartRolling(); // �v���C���[�̃^�[�����J�n
    }

    private void Update()
    {
        if (mapPiece >= 10)
        {
            Debug.Log("�N���A����B");
            SceneManager.LoadScene("Gameclear");
        }

        //�n�}�̂����牼
        if (Input.GetKeyDown(KeyCode.R))
        {
            MpPlus();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            EnemyCopy.SetActive(true);
            EnemyCopyOn = true;
        }

        if (enemyTurnFinCount == 2)
        {
            enemyTurnFinCount = 0;
            NextTurn();
        }
    }
    public void MpPlus()
    {
        mapPiece++;
        Debug.Log(mapPiece);
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
                //ChangeEnemyModel();
            }

            playerSaikoro.StartRolling();
        }
        else
        {
            StartCoroutine(enemySaikoro.EnemyTurn());

            if (EnemyCopyOn)
            {
                StartCoroutine(enemyCopySaikoro.EnemyTurn());
            }
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


            // ���݂̃G�l�~�[���f�����폜
            Destroy(currentEnemyModel);

            // �V�����G�l�~�[���f�������݂̃G�l�~�[���f���Ƃ��Đݒ�
            currentEnemyModel = newEnemyModel;

            Debug.Log("Enemy model has been changed and positioned.");
        }
        else
        {
            Debug.LogError("Enemy models are not assigned!");
        }
    }
}