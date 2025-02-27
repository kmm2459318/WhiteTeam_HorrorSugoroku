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
    public float returnDuration = 0.5f;

    private bool isExploring = false;
    private Transform currentExploreTarget;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation; // �T���O�̃J�����̌�����ۑ�
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
            exploreText.text = "[Q�ŒT��]";
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
                originalCameraRotation = playerCamera.rotation; // �T���O�̃J�����̌�����ۑ�

                exploreText.gameObject.SetActive(true);
                exploreText.text = "[Q�ŒT������߂�]";
            }
        }
    }

    IEnumerator ReturnCameraSmooth()
    {
        float elapsed = 0;
        Quaternion startRot = playerCamera.rotation; // ���̃J�����̌�����ۑ�

        while (elapsed < returnDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / returnDuration;
            playerCamera.rotation = Quaternion.Slerp(startRot, originalCameraRotation, t); // �T���O�̌����ɖ߂�
            yield return null;
        }

        playerCamera.rotation = originalCameraRotation; // �Ō�ɂ������茳�̌����ɖ߂�
    }
}
