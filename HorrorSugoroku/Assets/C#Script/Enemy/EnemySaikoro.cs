using SmoothigTransform;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmoothigTransform;
using UnityEngine.SceneManagement;

public class EnemySaikoro : MonoBehaviour
{
    [SerializeField] SmoothTransform enemySmooth;
    [SerializeField] SmoothTransform enemyBodySmooth;
    [SerializeField] SmoothTransform enemyBodySmoothsin;
    [SerializeField] private AudioSource footstepSound; // 足音のAudioSource

    public GameObject player;
    public GameObject ENorth;
    public GameObject EWest;
    public GameObject EEast;
    public GameObject ESouth;
    private bool EN = false; // 敵の東西南北
    private bool EW = false;
    private bool EE = false;
    private bool ES = false;
    private Transform Smasu;
    private Transform Nmasu;
    private Transform Wmasu;
    private Transform Emasu;
    public LayerMask wallLayer; // 壁のレイヤー
    private int steps; // サイコロの目の数
    public bool discovery = false;
    private bool dis = false;
    private bool dis2 = false;
    public bool enemyidoutyu = false;
    Image image;
    //public AudioClip discoveryBGM; // 発見時のBGM
    //public AudioClip undetectedBGM; // 未発見時のBGM
    private AudioSource audioSource; // 音声再生用のAudioSource
    public float idouspanTime;
    Vector3 goToPos = new Vector3(0f, 0, -1.65f);
    private int goToMass = 2;
    public EnemyController enemyController;
    public GameManager gameManager; // GameManagerの参照
    public EnemyLookAtPlayer enemyLookAtPlayer; // EnemyLookAtPlayerの参照
    public PlayerSaikoro playerSaikoro;
    public PlayerCloseMirror playerCloseMirror;
    public EnemyCloseMasu enemyCloseMasu;
    public float mokushi = 3.0f;
    public int idoukagen = 1;
    public bool skill1 = false;
    public bool skill2 = false;
    public bool isTrapped = false; // トラバサミにかかっているかどうかを示すフラグ
    private bool isMoving = false; // エネミーが移動中かどうかを示すフラグ

    public float footstepVolume = 1.0f;
    public Animator animator;
    
    void Start()
    {
        // 初期化コード
        animator = GetComponent<Animator>();
        enemyController = this.GetComponent<EnemyController>();
        gameManager = FindObjectOfType<GameManager>(); // GameManagerの参照を取得
        enemyLookAtPlayer = this.GetComponent<EnemyLookAtPlayer>(); // EnemyLookAtPlayerの参照を取得

        if (enemyLookAtPlayer == null)
        {
            Debug.LogError("EnemyLookAtPlayer component is not assigned or found on the enemy object.");
        }
        /*else
        {
            enemyLookAtPlayer.northTransform = ENorth.transform; // NorthオブジェクトのTransformを設定
        }*/

        // AudioSourceの取得
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceがなければ追加
        }

        // デバッグログの追加
        Debug.Log("EnemySaikoro Start method called.");
        Debug.Log("Player: " + player);
        Debug.Log("ENorth: " + ENorth);
        Debug.Log("EWest: " + EWest);
        Debug.Log("EEast: " + EEast);
        Debug.Log("ESouth: " + ESouth);
        Debug.Log("enemySmooth: " + enemySmooth);
        Debug.Log("enemyBodySmooth: " + enemyBodySmooth);

        ES = true;
    }
    void Update()
    {
        if (ENorth == null) Debug.LogError("ENorth is not assigned.");
        if (EWest == null) Debug.LogError("EWest is not assigned.");
        if (EEast == null) Debug.LogError("EEast is not assigned.");
        if (ESouth == null) Debug.LogError("ESouth is not assigned.");

        EN = ENorth.GetComponent<PlayerNSEWCheck>().masuCheck;
        EW = EWest.GetComponent<PlayerNSEWCheck>().masuCheck;
        EE = EEast.GetComponent<PlayerNSEWCheck>().masuCheck;
        ES = ESouth.GetComponent<PlayerNSEWCheck>().masuCheck;
        Nmasu = ENorth.GetComponent<PlayerCloseMass>().GetClosestObject();
        Wmasu = EWest.GetComponent<PlayerCloseMass>().GetClosestObject();
        Emasu = EEast.GetComponent<PlayerCloseMass>().GetClosestObject();
        Smasu = ESouth.GetComponent<PlayerCloseMass>().GetClosestObject();

        if (gameManager.IsPlayerTurn())
        {
            // プレイヤーのターン中はエネミーをIdle状態に保つ
            if (animator != null)
            {
                animator.SetBool("isRunning", false);
            }
            return;
        }

        // プレイヤーが発見されたかをチェック
        if (Vector3.Distance(this.transform.position, player.transform.position) < mokushi)
        {
            // 発見時のBGMを流す
            /*if (discoveryBGM != null && audioSource.clip != discoveryBGM)
            {
                audioSource.Stop(); // 現在のBGMを停止
                audioSource.clip = discoveryBGM;
                audioSource.Play(); // 発見時のBGMを再生
            }*/
            discovery = true;
            enemyLookAtPlayer.SetDiscovery(true); // エネミーの体をプレイヤーの方向に向ける
        }
        else
        {
            // 未発見時のBGMを流す
            /*if (undetectedBGM != null && audioSource.clip != undetectedBGM)
            {
                audioSource.Stop(); // 現在のBGMを停止
                audioSource.clip = undetectedBGM;
                audioSource.Play(); // 未発見時のBGMを再生
            }*/
            discovery = false;
            dis = false;
            enemyLookAtPlayer.SetDiscovery(false); // エネミーの体をプレイヤーの方向に向けない
        }

        if (playerSaikoro.exploring)
        {
            this.idouspanTime += Time.deltaTime;
            if (idouspanTime > 2.0f)
            {
                idouspanTime = 0f;
            }
        }
        else
        {
            idouspanTime = 0f;
        }

        if (discovery || dis2)
        {
            GameObject masu;
            dis2 = true;

            if (!discovery)
            {
                masu = enemyCloseMasu.FindClosestMasu();

                goToPos.x = masu.transform.position.x;
                goToPos.z = masu.transform.position.z;
                GoToMassChange(goToMass);
                dis2 = false;
            }
        }

        if (((goToPos.x + 0.1f > this.transform.position.x && goToPos.x - 0.1f < this.transform.position.x) &&
            (goToPos.z + 0.1f > this.transform.position.z && goToPos.z - 0.1f < this.transform.position.z)) || (discovery && !dis))
        {
            Debug.Log("行先変更");
            Debug.Log("Current Position: " + this.transform.position);
            Debug.Log("Target Position: " + goToPos);
            Debug.Log("Discovery: " + discovery);
            Debug.Log("Dis: " + dis);
            dis = true;
            GoToMassChange(goToMass);

            // Northが進む方向に向くように設定
            /*Vector3 direction = (goToPos - this.transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                ENorth.transform.localPosition = new Vector3(18, 3.53f, -40); // Northの位置を固定
                transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0); // エネミーのモデルのY軸回転を設定
                Debug.Log("ENorth rotation set to: " + lookRotation.eulerAngles);
            }*/
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
                goToPos = Emasu.transform.position;
                goToMass = 3; break;
            case 2:
                goToPos = Nmasu.transform.position;
                goToMass = 4; break;
            case 3:
                goToPos = Wmasu.transform.position;
                goToMass = 1; break;
            case 4:
                goToPos = Smasu.transform.position;
                goToMass = 2; break;
        }
        Debug.Log("gotopos" + goToPos);
    }

    public IEnumerator RollEnemyDice()
    {
        bool speedidou = false;
        bool mirror = false;
        enemyidoutyu = true;
        if (5 == Random.Range(1, 6) && skill1)
        {
            Debug.Log("ーーーーーー高速移動発動ーーーーーー");
            speedidou = true;
        }
        else if (5 == Random.Range(1, 6) && skill2)
        {
            Debug.Log("ーーーーーーー鏡移動発動ーーーーーーー");
            mirror = true;
            enemySmooth.PosFact = 0f;
        }

        if (!mirror)
        {
            /*for (int i = 0; i < 10; i++) // 10回ランダムに目を表示
            {
                steps = Random.Range(idoukagen, 7);
                yield return new WaitForSeconds(0.1f); // 0.1秒ごとに目を変更
            }*/
            steps = 1;

            if (steps <= 3)
            {
                enemySmooth.PosFact = 0.9f;
            }
            else
            {
                enemySmooth.PosFact = 0.2f;
            }

            //Debug.Log("Enemy rolled: " + steps);
        }
        StartCoroutine(MoveTowardsPlayer(speedidou, mirror));
        yield return 0;
    }

    private IEnumerator MoveTowardsPlayer(bool s1, bool s2)
    {
        Debug.Log("MoveTowardsPlayer called");
        isMoving = true;
        enemyLookAtPlayer.SetIsMoving(true);
        enemyController.SetMovement(true);

        if (footstepSound != null)
        {
            footstepSound.Play();
        }


        int initialSteps = steps;
        AudioClip currentBGM = audioSource.clip;
        bool isFootstepPlaying = false;
        Vector3 lastDire = new Vector3(0, 0, 0);
        bool s1n = false;
        GameObject mirror;

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        if (!s2)
        {

            while (steps > 0)
            {

                // トラバサミにかかっている場合は移動しない
                if (isTrapped)
                {
                    Debug.Log("Enemy is trapped and cannot move.");
                    yield return new WaitForSeconds(0.5f); // 0.5秒待つだけ
                    steps = 0;
                    break;
                }

                Vector3 direction;
                if (discovery)
                {
                    direction = (player.transform.position - this.transform.position).normalized;
                    direction = GetValidDirection(direction); // 壁を避ける方向を計算
                }
                else
                {
                    direction = (goToPos - this.transform.position);
                    direction = GetValidDirection(direction);
                }

                Debug.Log(direction);
                Debug.Log(lastDire);
                if (direction != lastDire)
                {
                    if (direction == new Vector3(0, 0, 2.0f))
                    {
                        enemyBodySmooth.TargetRotation = Quaternion.Euler(-90, 0, 0);
                        enemyBodySmoothsin.TargetRotation = Quaternion.Euler(0, 0, 0);
                        goToMass = 4;
                    }
                    else if (direction == new Vector3(0, 0, -2.0f))
                    {
                        enemyBodySmooth.TargetRotation = Quaternion.Euler(-90, 180, 0);
                        enemyBodySmoothsin.TargetRotation = Quaternion.Euler(0, 180, 0);
                        goToMass = 2;
                    }
                    else if (direction == new Vector3(2.0f, 0, 0))
                    {
                        enemyBodySmooth.TargetRotation = Quaternion.Euler(-90, 90, 0);
                        enemyBodySmoothsin.TargetRotation = Quaternion.Euler(0, 90, 0);
                        goToMass = 3;
                    }
                    else if (direction == new Vector3(-2.0f, 0, 0))
                    {
                        enemyBodySmooth.TargetRotation = Quaternion.Euler(-90, -90, 0);
                        enemyBodySmoothsin.TargetRotation = Quaternion.Euler(0, -90, 0);
                        goToMass = 1;
                    }
                    yield return new WaitForSeconds(0.5f);

                }
                Debug.Log(goToMass);

                switch (goToMass)
                {
                    case 4:
                        enemySmooth.TargetPosition = Nmasu.transform.position; break;
                    case 2:
                        enemySmooth.TargetPosition = Smasu.transform.position; break;
                    case 3:
                        enemySmooth.TargetPosition = Emasu.transform.position; break;
                    case 1:
                        enemySmooth.TargetPosition = Wmasu.transform.position; break;
                }


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


                // エネミーの移動方向を設定
                enemyLookAtPlayer.SetMoveDirection(direction);

                // Northが進む方向に向くように設定
                /*if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    ENorth.transform.rotation = lookRotation;
                    Debug.Log("ENorth rotation set to: " + lookRotation.eulerAngles);
                }*/

                Debug.Log("Enemy moved towards player. Steps remaining: " + steps);


                // プレイヤーが発見されたかをチェック
                if (Vector3.Distance(this.transform.position, player.transform.position) < mokushi)
                {
                    /*if (discoveryBGM != null && !audioSource.isPlaying) // 発見時のBGMを流す
                    {
                        audioSource.clip = discoveryBGM;
                        audioSource.Play();
                    }*/
                    discovery = true;
                    Debug.Log("発見！");
                }

                enemyidoutyu = false;
                lastDire = direction;

                if (enemySmooth.PosFact == 0.2f)
                {
                    yield return new WaitForSeconds(0.4f); // 移動の間隔を待つ
                }
                else
                {
                    yield return new WaitForSeconds(1.0f);
                }
            }
            isTrapped = false;
        }
        else
        {
            Debug.Log("ミラーワーーーーーーーーーーーーープ！！！！");

            mirror = playerCloseMirror.FindClosestMirror();
            enemySmooth.TargetPosition.x = mirror.transform.position.x * 1.0f;
            enemySmooth.TargetPosition.z = mirror.transform.position.z;
            Debug.Log(mirror.transform.position);
        }

        enemyController.SetMovement(false); // エネミーの移動が終了したらisMovingをfalseに設定
     
        // 移動が終了したら、再度BGMを再開
        if (currentBGM != null && !audioSource.isPlaying)
        {
            audioSource.clip = currentBGM;
            audioSource.Play(); // BGMを再開
        }

        Debug.Log("Enemy moved a total of " + initialSteps + " steps.");

        yield return 0;
        /*if (!gameManager.EnemyCopyOn)
        {
            FindObjectOfType<GameManager>().NextTurn(); // 次のターンに進む
        }
        else
        {
            gameManager.enemyTurnFinCount++;
        }*/

    }

    private Vector3 GetValidDirection(Vector3 targetDirection)
    {
        Vector3[] directions = new Vector3[]
        {
        new Vector3(2.0f, 0, 0),   // 東
        new Vector3(-2.0f, 0, 0),  // 西
        new Vector3(0, 0, 2.0f),   // 北
        new Vector3(0, 0, -2.0f)   // 南
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

        return bestDirection != Vector3.zero ? bestDirection : targetDirection; // 有効な方向があればそれを返す
    }


    void LateUpdate()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        // センサー機能: Rayが何かに当たった場合にログ出力
        if (Physics.Raycast(ray, out hit, 3f)) // 3mの範囲
        {
            //Debug.Log("発見");
        }
    }

    void OnDrawGizmosSelected()
    {
        // センサーの範囲を赤い線で表示
        Gizmos.color = Color.red;
        Vector3 direction = this.transform.position + this.transform.forward * 3f;
        Gizmos.DrawLine(this.transform.position, direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("敵がトラばさみに引っ掛かった！！");
        if (other.tag == ("Beartrap"))
        {
            isTrapped = true;
            Debug.Log("敵がトラばさみに引っ掛かった！！");
        }
    }
    public void SetIdle()
    {
        if (animator != null)
        {
            animator.SetBool("is Running", false);
        }
    }
    public void SetRun()
    {
        if (animator != null)
        {
            animator.SetBool("is Running", true);
        }
    }

    // 現在のアニメーション状態を取得するメソッド
    public string GetCurrentAnimationState()
    {
        if (animator != null)
        {
            if (animator.GetBool("is Running"))
            {
                return "Run";
            }
            else
            {
                return "Idle";
            }
        }
        return "Idle";
    }

    // アニメーション状態を設定するメソッド
    public void SetAnimationState(string state)
    {
        if (animator != null)
        {
            if (state == "Run")
            {
                animator.SetBool("is Running", true);
            }
            else
            {
                animator.SetBool("is Running", false);
            }
        }
    }
}