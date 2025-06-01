using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CameraExplore : MonoBehaviour
{
    public List<Transform> exploreTargets = new List<Transform>();
    public Transform playerCamera;
    public float moveSpeed = 3f;
    public float exploreRadius = 2f;
    public float returnDuration = 0.5f; // 元の位置・向きに戻る時間

    private bool isExploring = false;
    private Transform currentExploreTarget;
    private Vector3 originalCameraPosition; // 探索前のカメラ位置を保存
    private Quaternion originalCameraRotation; // 探索前のカメラ向きを保存
    private Coroutine returnCoroutine;

    void Start()
    {

        // カメラの初期向きを保存
        originalCameraRotation = playerCamera.rotation;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ExploreTarget"))
        {
            exploreTargets.Add(obj.transform);
        }
    }

    void Update()
    {
        if (!isExploring)
        {
            CheckForExploreTarget();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleExplore();
        }

        // 探索中だけカメラをターゲットに動かす
        if (isExploring)
        {
            MoveCameraToTarget();
        }
    }

    void CheckForExploreTarget()
    {
        float minDistance = exploreRadius;
        Transform closestTarget = null;

        foreach (var target in exploreTargets)
        {
            float distance = Vector3.Distance(playerCamera.position, target.position);
            if (distance <= exploreRadius)
            {
                Vector3 directionToTarget = (target.position - playerCamera.position).normalized;
                float dot = Vector3.Dot(playerCamera.forward, directionToTarget);

                if (dot > 0.8f)
                {
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestTarget = target;
                    }
                }
            }
        }

    }

    void MoveCameraToTarget()
    {
        if (currentExploreTarget != null)
        {
            // 位置だけ補間する（回転はCameraControllerに任せる）
            playerCamera.position = Vector3.Lerp(playerCamera.position, currentExploreTarget.position, Time.deltaTime * moveSpeed);
        }
    }

    void ToggleExplore()
    {
        if (isExploring)
        {
            isExploring = false;

            // もし補間が動いていたら停止
            if (returnCoroutine != null)
            {
                StopCoroutine(returnCoroutine);
                returnCoroutine = null;
            }

            returnCoroutine = StartCoroutine(ReturnCameraSmooth());
        }
        else
        {
            if (currentExploreTarget != null)
            {
                isExploring = true;
                originalCameraPosition = playerCamera.position;
                originalCameraRotation = playerCamera.rotation;

            }
        }
    }

    IEnumerator ReturnCameraSmooth()
    {
        float elapsed = 0;
        Vector3 startPos = playerCamera.position;
        Quaternion startRot = playerCamera.rotation;

        while (elapsed < returnDuration)
        {
            // 途中でプレイヤーがカメラを動かしたら補間を止める
            if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                break;
            }

            elapsed += Time.deltaTime;
            float t = elapsed / returnDuration;
            playerCamera.position = Vector3.Lerp(startPos, originalCameraPosition, t);
            playerCamera.rotation = Quaternion.Slerp(startRot, originalCameraRotation, t);
            yield return null;
        }

        // 最終的な位置・向きをセット
        playerCamera.position = originalCameraPosition;
        playerCamera.rotation = originalCameraRotation;
    }
}
