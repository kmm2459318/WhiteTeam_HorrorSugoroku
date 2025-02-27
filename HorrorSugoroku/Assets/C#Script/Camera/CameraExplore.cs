using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CameraExplore : MonoBehaviour
{
    public List<Transform> exploreTargets = new List<Transform>();
    public Transform playerCamera;
    public TextMeshProUGUI exploreText;
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
        exploreText.gameObject.SetActive(false);
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

        if (closestTarget != null)
        {
            exploreText.gameObject.SetActive(true);
            currentExploreTarget = closestTarget;
            exploreText.text = "[Qで探索]";
        }
        else
        {
            exploreText.gameObject.SetActive(false);
            currentExploreTarget = null;
        }
    }

    void MoveCameraToTarget()
    {
        if (currentExploreTarget != null)
        {
            playerCamera.position = Vector3.Lerp(playerCamera.position, currentExploreTarget.position, Time.deltaTime * moveSpeed);
            playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, currentExploreTarget.rotation, Time.deltaTime * moveSpeed);
        }
    }

    void ToggleExplore()
    {
        if (isExploring)
        {
            isExploring = false;
            exploreText.gameObject.SetActive(false);

            if (returnCoroutine != null)
            {
                StopCoroutine(returnCoroutine);
            }
            returnCoroutine = StartCoroutine(ReturnCameraSmooth());
        }
        else
        {
            if (currentExploreTarget != null)
            {
                isExploring = true;
                originalCameraPosition = playerCamera.position; // 探索前のカメラ位置を保存
                originalCameraRotation = playerCamera.rotation; // 探索前のカメラ向きを保存

                exploreText.gameObject.SetActive(true);
                exploreText.text = "[Qで探索をやめる]";
            }
        }
    }

    IEnumerator ReturnCameraSmooth()
    {
        float elapsed = 0;
        Vector3 startPos = playerCamera.position; // 現在のカメラ位置を取得
        Quaternion startRot = playerCamera.rotation; // 現在のカメラ向きを取得

        while (elapsed < returnDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / returnDuration;
            playerCamera.position = Vector3.Lerp(startPos, originalCameraPosition, t); // 元の位置に補間
            playerCamera.rotation = Quaternion.Slerp(startRot, originalCameraRotation, t); // 元の向きに補間
            yield return null;
        }

        playerCamera.position = originalCameraPosition; // 最終的に正確な位置に戻す
        playerCamera.rotation = originalCameraRotation; // 最終的に正確な向きに戻す
    }
}
