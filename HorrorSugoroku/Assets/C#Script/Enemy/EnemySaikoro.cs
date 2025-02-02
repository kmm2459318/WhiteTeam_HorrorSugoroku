﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmoothigTransform;

public class EnemySaikoro : MonoBehaviour
{
    [SerializeField] SmoothTransform enemySmooth;
    public GameObject enemy;
    public GameObject player;
    public GameObject saikoro; // サイコロのゲームオブジェクト
    public LayerMask wallLayer; // 壁のレイヤー
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    private int steps; // サイコロの目の数
    private bool discovery = false;
    Image image;
    public AudioClip discoveryBGM; // 発見時のBGM
    public AudioClip undetectedBGM; // 未発見時のBGM
    private AudioSource audioSource; // 音声再生用のAudioSource
    public AudioClip footstepSound; // 足音のAudioClip
    Vector3 random;
    private EnemyController enemyController;
    private GameManager gameManager; // GameManagerの参照
    private EnemyLookAtPlayer enemyLookAtPlayer; // EnemyLookAtPlayerの参照


    void Start()
    {
        // 初期化コード
        enemyController = enemy.GetComponent<EnemyController>();
        gameManager = FindObjectOfType<GameManager>(); // GameManagerの参照を取得
        enemyLookAtPlayer = enemy.GetComponent<EnemyLookAtPlayer>(); // EnemyLookAtPlayerの参照を取得

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

        // サイコロのImageを保持
        image = saikoro.GetComponent<Image>();
        

        // AudioSourceの取得
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceがなければ追加
        }
    }
    void Update()
    {
        if (gameManager.IsPlayerTurn())
        {
            return;
        }

        // サイコロの目に応じてスプライトを変更
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

        // プレイヤーが発見されたかをチェック
        if (Vector3.Distance(enemy.transform.position, player.transform.position) < 5f)
        {

            // 発見時のBGMを流す
            if (discoveryBGM != null && audioSource.clip != discoveryBGM)
            {
                audioSource.Stop(); // 現在のBGMを停止
                audioSource.clip = discoveryBGM;
                audioSource.Play(); // 発見時のBGMを再生
            }
            discovery = true;
            enemyLookAtPlayer.SetDiscovery(true); // エネミーの体をプレイヤーの方向に向ける

            Debug.Log("発見！");
        }
        else
        {
            // 未発見時のBGMを流す
            if (undetectedBGM != null && audioSource.clip != undetectedBGM)
            {
                audioSource.Stop(); // 現在のBGMを停止
                audioSource.clip = undetectedBGM;
                audioSource.Play(); // 未発見時のBGMを再生
            }
            discovery = false;
            enemyLookAtPlayer.SetDiscovery(false); // エネミーの体をプレイヤーの方向に向けない

            Debug.Log("未発見");
        }
    }


    public IEnumerator RollEnemyDice()
    {
        saikoro.SetActive(true);
        for (int i = 0; i < 10; i++) // 10回ランダムに目を表示
        {
            steps = Random.Range(1, 7);
            yield return new WaitForSeconds(0.1f); // 0.1秒ごとに目を変更
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
        StartCoroutine(MoveTowardsPlayer());
    }

    private IEnumerator MoveTowardsPlayer()
    {
        int initialSteps = steps;
        AudioClip currentBGM = audioSource.clip;
        bool isFootstepPlaying = false;

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        int dix = Random.Range(1, 3);
        int diz = Random.Range(1, 3);
        random = new Vector3((dix == 1 ? Random.Range(-40, -20) : Random.Range(20, 40)), 0, (diz == 1 ? Random.Range(-40, -20) : Random.Range(20, 40)));

        enemyController.SetMovement(true); // エネミーが動き始めたらisMovingをtrueに設定

        while (steps > 0)
        {
            Vector3 direction;
            if (discovery)
            {
                direction = (player.transform.position - enemy.transform.position).normalized;
                direction = GetValidDirection(direction); // 壁を避ける方向を計算
            }
            else
            {
                direction = (random - enemy.transform.position);
                direction = GetValidDirection(direction);
            }

            enemySmooth.TargetPosition += direction * 1.0f; // 2.0f単位で移動
            steps--;

            // 足音が鳴っていない場合、鳴らす
            if (footstepSound != null && !isFootstepPlaying)
            {
                audioSource.PlayOneShot(footstepSound); // 足音を鳴らす
                isFootstepPlaying = true; // 足音再生フラグを立てる
            }

            // エネミーの移動方向を設定
            enemyLookAtPlayer.SetMoveDirection(direction);

            Debug.Log("Enemy moved towards player. Steps remaining: " + steps);

            // プレイヤーが発見されたかをチェック
            if (Vector3.Distance(enemy.transform.position, player.transform.position) < 5f)
            {
                if (discoveryBGM != null && !audioSource.isPlaying) // 発見時のBGMを流す
                {
                    audioSource.clip = discoveryBGM;
                    audioSource.Play();
                }
                discovery = true;
                Debug.Log("発見！");
                break;
            }

            yield return new WaitForSeconds(0.5f); // 移動の間隔を待つ
        }

        enemyController.SetMovement(false); // エネミーの移動が終了したらisMovingをfalseに設定

        // 移動が終了したら、再度BGMを再開
        if (currentBGM != null && !audioSource.isPlaying)
        {
            audioSource.clip = currentBGM;
            audioSource.Play(); // BGMを再開
        }

        saikoro.SetActive(false); // サイコロを非表示にする

        Debug.Log("Enemy moved a total of " + initialSteps + " steps.");
        FindObjectOfType<GameManager>().NextTurn(); // 次のターンに進む
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
            Vector3 potentialPosition = enemy.transform.position + direction;
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
                    float distanceToPlayer = Vector3.Distance(potentialPosition, random);
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

    public IEnumerator EnemyTurn()
    {
        yield return StartCoroutine(RollEnemyDice());
    }

    void LateUpdate()
    {
        Ray ray = new Ray(enemy.transform.position, enemy.transform.forward);
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
        Vector3 direction = enemy.transform.position + enemy.transform.forward * 3f;
        Gizmos.DrawLine(enemy.transform.position, direction);
    }

}
