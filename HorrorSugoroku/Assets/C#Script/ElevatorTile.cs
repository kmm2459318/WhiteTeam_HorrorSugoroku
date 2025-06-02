using System.Collections;  // �� �ǉ�
using SmoothigTransform;
using UnityEngine;

public class ElevatorTile : MonoBehaviour
{
    public SmoothTransform smoothTransform;
    public ElevatorController controller;
    public GameObject ElevatorMasu;

    [Header("�G���x�[�^�[�̏��i���������j")]
    public Transform elevatorPlatform;

    [Header("�Ώۃv���C���[")]
    public Transform player;

    public AudioSource elevatorSound; // �� �ǉ�

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

            // �� �������Đ�
            if (elevatorSound != null)
            {
                elevatorSound.Play();
                // �� 7�b��ɉ����~�߂�R���[�`���J�n
                soundCoroutine = StartCoroutine(StopSoundAfterTime(7f));
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerOnThisTile = false;

            // �� �v���C���[�����ꂽ�特�𑦒�~
            if (elevatorSound != null)
            {
                elevatorSound.Stop();
            }

            // �� �R���[�`�����L�����Z��
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