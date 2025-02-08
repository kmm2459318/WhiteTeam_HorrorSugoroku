using SmoothigTransform;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmoothigTransform;

public class EnemySaikoroNakamura : MonoBehaviour
{
    [SerializeField] SmoothTransform enemySmooth;
    [SerializeField] SmoothTransform enemyBodySmooth;
    public GameObject player;
    public GameObject saikoro; // �T�C�R���̃Q�[���I�u�W�F�N�g
    public GameObject ENorth;
    public GameObject EWest;
    public GameObject EEast;
    public GameObject ESouth;
    private bool EN = false; // �G�̓�����k
    private bool EW = false;
    private bool EE = false;
    private bool ES = false;
    public LayerMask wallLayer; // �ǂ̃��C���[
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    private int steps; // �T�C�R���̖ڂ̐�
    private bool discovery = false;
    private bool dis = false;
    Image image;
    //public Text discoveryText; // �V����Text�ϐ���ǉ�
    public AudioClip discoveryBGM; // ��������BGM
    public AudioClip undetectedBGM; // ����������BGM
    private AudioSource audioSource; // �����Đ��p��AudioSource
    public AudioClip footstepSound; // ������AudioClip
    Vector3 goToPos = new Vector3(0, 0, 0);
    private int goToMass = 1;
    private EnemyController enemyController;
    private GameManager gameManager; // GameManager�̎Q��
    private EnemyLookAtPlayer enemyLookAtPlayer; // EnemyLookAtPlayer�̎Q��
    public PlayerCloseMirror playerCloseMirror;
    public float mokushi = 3.0f;
    public int idoukagen = 1;
    public bool skill1 = false;
    public bool skill2 = false;
    private bool isTrapped = false; // �g���o�T�~�ɂ������Ă��邩�ǂ����������t���O
    private bool isMoving = false; // �G�l�~�[���ړ������ǂ����������t���O

    void Start()
    {
        // �������R�[�h
        enemyController = this.GetComponent<EnemyController>();
        gameManager = FindObjectOfType<GameManager>(); // GameManager�̎Q�Ƃ��擾
        enemyLookAtPlayer = this.GetComponent<EnemyLookAtPlayer>(); // EnemyLookAtPlayer�̎Q�Ƃ��擾

        if (enemyLookAtPlayer == null)
        {
            Debug.LogError("EnemyLookAtPlayer component is not assigned or found on the enemy object.");
        }

        if (saikoro != null)
        {
            saikoro.SetActive(false);
        }
        else
        {
            Debug.LogError("Saikoro GameObject is not assigned in the Inspector.");
        }

        // �T�C�R����Image��ێ�
        image = saikoro.GetComponent<Image>();

        // �e�L�X�g�̏�����
        //if (discoveryText != null)
        //{
        //    discoveryText.text = "������"; // ������Ԃ͖�����
        //}
        //else
        //{
        //    Debug.LogError("Discovery Text is not assigned in the Inspector.");
        //}

        // AudioSource�̎擾
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource���Ȃ���Βǉ�
        }
    }
    void Update()
    {
        EN = ENorth.GetComponent<PlayerNSEWCheck>().masuCheck;
        EW = EWest.GetComponent<PlayerNSEWCheck>().masuCheck;
        EE = EEast.GetComponent<PlayerNSEWCheck>().masuCheck;
        ES = ESouth.GetComponent<PlayerNSEWCheck>().masuCheck;

        if (gameManager.IsPlayerTurn())
        {
            return;
        }

        // �T�C�R���̖ڂɉ����ăX�v���C�g��ύX
        switch (steps)
        {
            case 1:
                image.sprite = s1; break;
            case 2:
                image.sprite = s2; break;
            case 3:
                image.sprite = s3; break;
            case 4:
                image.sprite = s4; break;
            case 5:
                image.sprite = s5; break;
            case 6:
                image.sprite = s6; break;
        }

        // �v���C���[���������ꂽ�����`�F�b�N
        if (Vector3.Distance(this.transform.position, player.transform.position) < mokushi)
        {
            //if (discoveryText != null)
            //{
            //    Debug.Log("�����I");
            //    discoveryText.text = "�����I"; // �v���C���[���߂��ꍇ�A�u�����I�v��\��
            //}

            // ��������BGM�𗬂�
            if (discoveryBGM != null && audioSource.clip != discoveryBGM)
            {
                audioSource.Stop(); // ���݂�BGM���~
                audioSource.clip = discoveryBGM;
                audioSource.Play(); // ��������BGM���Đ�
            }
            discovery = true;
            enemyLookAtPlayer.SetDiscovery(true); // �G�l�~�[�̑̂��v���C���[�̕����Ɍ�����

            //Debug.Log("�����I");
        }
        else
        {
            //if (discoveryText != null)
            //{
            //    discoveryText.text = "������"; // �v���C���[�������ꍇ�A�u�������v��\��
            //}

            // ����������BGM�𗬂�
            if (undetectedBGM != null && audioSource.clip != undetectedBGM)
            {
                audioSource.Stop(); // ���݂�BGM���~
                audioSource.clip = undetectedBGM;
                audioSource.Play(); // ����������BGM���Đ�
            }
            discovery = false;
            dis = false;
            enemyLookAtPlayer.SetDiscovery(false); // �G�l�~�[�̑̂��v���C���[�̕����Ɍ����Ȃ�

            //Debug.Log("������");
        }

        if (((goToPos.x + 0.1f > this.transform.position.x && goToPos.x - 0.1f < this.transform.position.x) &&
            (goToPos.z + 0.1f > this.transform.position.z && goToPos.z - 0.1f < this.transform.position.z)) || (discovery && !dis))
        {
            Debug.Log("�s��ύX");
            dis = true;
            GoToMassChange(goToMass);
        }
    }

    void GoToMassChange(int m)
    {
        int a;
        do
        {
            a = Random.Range(1, 5);
        } while (a == m || (a == 1 && !EE) || (a == 2 && !EN) || (a == 3 && !EW) || (a == 4 && !ES));

        switch (a)
        {
            case 1:
                goToPos += new Vector3(10f, 0, 0);
                goToMass = 3; break;
            case 2:
                goToPos += new Vector3(0, 0, 10f);
                goToMass = 4; break;
            case 3:
                goToPos += new Vector3(-10f, 0, 0);
                goToMass = 1; break;
            case 4:
                goToPos += new Vector3(0, 0, -10f);
                goToMass = 2; break;
        }
        Debug.Log(goToPos);
    }

    public IEnumerator RollEnemyDice()
    {
        bool speedidou = false;
        bool mirror = false;
        if (5 == Random.Range(1, 6) && skill1)
        {
            Debug.Log("�[�[�[�[�[�[�����ړ������[�[�[�[�[�[");
            speedidou = true;
        }
        else if (5 == Random.Range(1, 6) && skill2)
        {
            Debug.Log("�[�[�[�[�[�[�[���ړ������[�[�[�[�[�[�[");
            mirror = true;
            enemySmooth.PosFact = 0f;
        }

        if (!mirror)
        {
            saikoro.SetActive(true);
            for (int i = 0; i < 10; i++) // 10�񃉃��_���ɖڂ�\��
            {
                steps = Random.Range(idoukagen, 7);
                yield return new WaitForSeconds(0.1f); // 0.1�b���Ƃɖڂ�ύX
            }

            if (steps <= 3)
            {
                enemySmooth.PosFact = 0.9f;
            }
            else
            {
                enemySmooth.PosFact = 0.2f;
            }

            Debug.Log("Enemy rolled: " + steps);
        }
        StartCoroutine(MoveTowardsPlayer(speedidou, mirror));
    }

    private IEnumerator MoveTowardsPlayer(bool s1, bool s2)
    {
        isMoving = true; // �ړ��J�n
        enemyLookAtPlayer.SetIsMoving(true); // �G�l�~�[�̈ړ���Ԃ�ݒ�
        int initialSteps = steps;
        AudioClip currentBGM = audioSource.clip;
        bool isFootstepPlaying = false;
        Vector3 lastDire = new Vector3(0, 0, 0);
        bool s1n = false;
        GameObject mirror;
        Debug.Log(goToPos);

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        enemyController.SetMovement(true); // �G�l�~�[�������n�߂���isMoving��true�ɐݒ�

        if (!s2)
        {
            while (steps > 0)
            {
                // �g���o�T�~�ɂ������Ă���ꍇ�͈ړ����Ȃ�
                if (isTrapped)
                {
                    Debug.Log("Enemy is trapped and cannot move.");
                    yield return new WaitForSeconds(0.5f); // 0.5�b�҂���
                    steps = 0;
                    break;
                }

                Vector3 direction;
                if (discovery)
                {
                    direction = (player.transform.position - this.transform.position).normalized;
                    direction = GetValidDirection(direction); // �ǂ������������v�Z
                }
                else
                {
                    direction = (goToPos - this.transform.position);
                    direction = GetValidDirection(direction);
                }

                if (direction != lastDire)
                {
                    if (direction == new Vector3(0, 0, 2.0f))
                    {
                        enemyBodySmooth.TargetRotation = Quaternion.Euler(-90, 90, 0);
                    }
                    else if (direction == new Vector3(0, 0, -2.0f))
                    {
                        enemyBodySmooth.TargetRotation = Quaternion.Euler(-90, -90, 0);
                    }
                    else if (direction == new Vector3(2.0f, 0, 0))
                    {
                        enemyBodySmooth.TargetRotation = Quaternion.Euler(-90, 180, 0);
                    }
                    else if (direction == new Vector3(-2.0f, 0, 0))
                    {
                        enemyBodySmooth.TargetRotation = Quaternion.Euler(-90, 0, 0);
                    }
                    yield return new WaitForSeconds(0.5f);
                }

                enemySmooth.TargetPosition += direction * 1.0f; // 2.0f�P�ʂňړ�

                if (s1)
                {
                    if (s1n)
                    {
                        steps--;
                        s1n = false;
                    }
                    else
                    {
                        s1n = true;
                    }
                }
                else
                {
                    steps--;
                }

                // ���������Ă��Ȃ��ꍇ�A�炷
                if (footstepSound != null && !isFootstepPlaying)
                {
                    audioSource.PlayOneShot(footstepSound); // ������炷
                    isFootstepPlaying = true; // �����Đ��t���O�𗧂Ă�
                }

                // �G�l�~�[�̈ړ�������ݒ�
                enemyLookAtPlayer.SetMoveDirection(direction);

                Debug.Log("Enemy moved towards player. Steps remaining: " + steps);

                // �v���C���[���������ꂽ�����`�F�b�N
                if (Vector3.Distance(this.transform.position, player.transform.position) < mokushi)
                {
                    //if (discoveryText != null)
                    //{
                    //    discoveryText.text = "�����I"; // �v���C���[���߂���΁u�����I�v�ƕ\��
                    //}
                    if (discoveryBGM != null && !audioSource.isPlaying) // ��������BGM�𗬂�
                    {
                        audioSource.clip = discoveryBGM;
                        audioSource.Play();
                    }
                    discovery = true;
                    Debug.Log("�����I");
                }
                lastDire = direction;

                if (enemySmooth.PosFact == 0.2f)
                {
                    yield return new WaitForSeconds(0.4f); // �ړ��̊Ԋu��҂�
                }
                else {
                    yield return new WaitForSeconds(1.0f);
                }
            }
            isTrapped = false;
        }
        else
        {
            Debug.Log("�~���[���[�[�[�[�[�[�[�[�[�[�[�[�[�v�I�I�I�I");

            mirror = playerCloseMirror.FindClosestMirror();
            enemySmooth.TargetPosition.x = mirror.transform.position.x * 1.0f;
            enemySmooth.TargetPosition.z = mirror.transform.position.z;
            Debug.Log(mirror.transform.position);
        }

        enemyController.SetMovement(false); // �G�l�~�[�̈ړ����I��������isMoving��false�ɐݒ�

        // �ړ����I��������A�ēxBGM���ĊJ
        if (currentBGM != null && !audioSource.isPlaying)
        {
            audioSource.clip = currentBGM;
            audioSource.Play(); // BGM���ĊJ
        }

        saikoro.SetActive(false); // �T�C�R�����\���ɂ���

        Debug.Log("Enemy moved a total of " + initialSteps + " steps.");

        if (!gameManager.EnemyCopyOn)
        {
            FindObjectOfType<GameManager>().NextTurn(); // ���̃^�[���ɐi��
        }
        else
        {
            gameManager.enemyTurnFinCount++;
        }
    }

    private Vector3 GetValidDirection(Vector3 targetDirection)
    {
        Vector3[] directions = new Vector3[]
        {
        new Vector3(2.0f, 0, 0),   // ��
        new Vector3(-2.0f, 0, 0),  // ��
        new Vector3(0, 0, 2.0f),   // �k
        new Vector3(0, 0, -2.0f)   // ��
        };

        Vector3 bestDirection = Vector3.zero;
        float closestDistance = float.MaxValue;

        foreach (Vector3 direction in directions)
        {
            Vector3 potentialPosition = this.transform.position + direction;
            if (!Physics.CheckSphere(potentialPosition, 0.5f, wallLayer))
            {
                if (discovery)
                {
                    float distanceToPlayer = Vector3.Distance(potentialPosition, player.transform.position);
                    if (distanceToPlayer < closestDistance)
                    {
                        closestDistance = distanceToPlayer;
                        bestDirection = direction;
                    }
                }
                else
                {
                    float distanceToPlayer = Vector3.Distance(potentialPosition, goToPos);
                    if (distanceToPlayer < closestDistance)
                    {
                        closestDistance = distanceToPlayer;
                        bestDirection = direction;
                    }
                }
            }
        }

        return bestDirection != Vector3.zero ? bestDirection : targetDirection; // �L���ȕ���������΂����Ԃ�
    }

    public IEnumerator EnemyTurn()
    {
        yield return StartCoroutine(RollEnemyDice());
    }

    void LateUpdate()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        // �Z���T�[�@�\: Ray�������ɓ��������ꍇ�Ƀ��O�o��
        if (Physics.Raycast(ray, out hit, 3f)) // 3m�͈̔�
        {
            //Debug.Log("����");
        }
    }

    void OnDrawGizmosSelected()
    {
        // �Z���T�[�͈̔͂�Ԃ����ŕ\��
        Gizmos.color = Color.red;
        Vector3 direction = this.transform.position + this.transform.forward * 3f;
        Gizmos.DrawLine(this.transform.position, direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("�G���g���΂��݂Ɉ����|�������I�I");
        if (other.tag == ("Beartrap"))
        {
            isTrapped = true;
            Debug.Log("�G���g���΂��݂Ɉ����|�������I�I");
        }
    }
}