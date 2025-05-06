using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource frontAudioSource; // �O���R���C�_�[�pAudioSource
    [SerializeField] private AudioSource backAudioSource;  // ����R���C�_�[�pAudioSource
    [SerializeField] private AudioSource leftAudioSource;  // �����R���C�_�[�pAudioSource
    [SerializeField] private AudioSource rightAudioSource; // �E���R���C�_�[�pAudioSource
    [SerializeField] private GameObject[] targetEnemies;   // �w�肷��G�l�~�[�I�u�W�F�N�g

    private int activeEnemyContacts = 0; // �ڐG���̃G�l�~�[����ǐ�

    public void OnColliderEnter(string colliderName, Collider other)
    {
        foreach (var enemy in targetEnemies)
        {
            if (other.gameObject == enemy)
            {
                activeEnemyContacts++;
                PlayAudioForCollider(colliderName);
                Debug.Log($"�G�l�~�[ {enemy.name} �ɐڐG���܂����I�i�R���C�_�[: {colliderName}�j");
                break;
            }
        }
    }

    public void OnColliderExit(string colliderName, Collider other)
    {
        foreach (var enemy in targetEnemies)
        {
            if (other.gameObject == enemy)
            {
                activeEnemyContacts--;
                if (activeEnemyContacts <= 0)
                {
                    StopAudioForCollider(colliderName);
                }
                Debug.Log($"�G�l�~�[ {enemy.name} ���痣��܂����I�i�R���C�_�[: {colliderName}�j");
                break;
            }
        }
    }

    private void PlayAudioForCollider(string colliderName)
    {
        switch (colliderName)
        {
            case "FrontCollider":
                if (frontAudioSource != null && !frontAudioSource.isPlaying) frontAudioSource.Play();
                break;

            case "BackCollider":
                if (backAudioSource != null && !backAudioSource.isPlaying) backAudioSource.Play();
                break;

            case "LeftCollider":
                if (leftAudioSource != null && !leftAudioSource.isPlaying) leftAudioSource.Play();
                break;

            case "RightCollider":
                if (rightAudioSource != null && !rightAudioSource.isPlaying) rightAudioSource.Play();
                break;
        }
    }

    private void StopAudioForCollider(string colliderName)
    {
        switch (colliderName)
        {
            case "FrontCollider":
                if (frontAudioSource != null && frontAudioSource.isPlaying) frontAudioSource.Stop();
                break;

            case "BackCollider":
                if (backAudioSource != null && backAudioSource.isPlaying) backAudioSource.Stop();
                break;

            case "LeftCollider":
                if (leftAudioSource != null && leftAudioSource.isPlaying) leftAudioSource.Stop();
                break;

            case "RightCollider":
                if (rightAudioSource != null && rightAudioSource.isPlaying) rightAudioSource.Stop();
                break;
        }
    }
}