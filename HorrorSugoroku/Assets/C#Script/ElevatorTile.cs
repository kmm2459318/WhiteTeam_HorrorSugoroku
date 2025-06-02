using System.Collections;  // ★ 追加
using SmoothigTransform;
using UnityEngine;

public class ElevatorTile : MonoBehaviour
{
    public SmoothTransform smoothTransform;
    public ElevatorController controller;
    public GameObject ElevatorMasu;

    [Header("エレベーターの床（動く部分）")]
    public Transform elevatorPlatform;

    [Header("対象プレイヤー")]
    public Transform player;

    public AudioSource elevatorSound; // ★ 追加

    private bool isPlayerOnThisTile = false;
    private Coroutine soundCoroutine;

    private void Update()
    {
        if (isPlayerOnThisTile && smoothTransform != null)
        {
            smoothTransform.SetTargetY(elevatorPlatform.position.y);
        }

        if (controller.isMoving)
        {
            smoothTransform.TargetPosition = ElevatorMasu.transform.position + new Vector3(0, 1.15f, 0);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerOnThisTile = true;

            // ★ 音声を再生
            if (elevatorSound != null)
            {
                elevatorSound.Play();
                // ★ 7秒後に音を止めるコルーチン開始
                soundCoroutine = StartCoroutine(StopSoundAfterTime(7f));
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerOnThisTile = false;

            // ★ プレイヤーが離れたら音を即停止
            if (elevatorSound != null)
            {
                elevatorSound.Stop();
            }

            // ★ コルーチンをキャンセル
            if (soundCoroutine != null)
            {
                StopCoroutine(soundCoroutine);
                soundCoroutine = null;
            }
        }
    }

    private IEnumerator StopSoundAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (isPlayerOnThisTile && elevatorSound.isPlaying)
        {
            elevatorSound.Stop();
        }
    }
}