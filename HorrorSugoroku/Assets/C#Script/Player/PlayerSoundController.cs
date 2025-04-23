using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource frontAudioSource; // �O���R���C�_�[�pAudioSource
    [SerializeField] private AudioSource backAudioSource;  // ����R���C�_�[�pAudioSource
    [SerializeField] private AudioSource leftAudioSource;  // �����R���C�_�[�pAudioSource
    [SerializeField] private AudioSource rightAudioSource; // �E���R���C�_�[�pAudioSource
    [SerializeField] private GameObject[] targetEnemies;   // �w�肷��G�l�~�[�I�u�W�F�N�g
    private int activeEnemyContacts = 0;                  // �ڐG���̃G�l�~�[����ǐ�

    private void Start()
    {
        // �Q�[���J�n���ɉ������~
        if (frontAudioSource != null) frontAudioSource.Stop();
        if (backAudioSource != null) backAudioSource.Stop();
        if (leftAudioSource != null) leftAudioSource.Stop();
        if (rightAudioSource != null) rightAudioSource.Stop();

        Debug.Log("�Q�[���J�n���ɂ��ׂẲ������~���܂����B");
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var enemy in targetEnemies)
        {
            if (other.gameObject == enemy)
            {
                activeEnemyContacts++;
                PlayAudioForCollider(this.gameObject.name);
                Debug.Log($"�G�l�~�[ {enemy.name} �ɐڐG���܂����I");
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var enemy in targetEnemies)
        {
            if (other.gameObject == enemy)
            {
                activeEnemyContacts--;
                if (activeEnemyContacts <= 0)
                {
                    StopAudioForCollider(this.gameObject.name);
                }
                Debug.Log($"�G�l�~�[ {enemy.name} ���痣��܂����I");
                break;
            }
        }
    }

    private void PlayAudioForCollider(string colliderName)
    {
        Debug.Log($"�����Đ������J�n: {colliderName}");
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
        Debug.Log($"������~�����J�n: {colliderName}");
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