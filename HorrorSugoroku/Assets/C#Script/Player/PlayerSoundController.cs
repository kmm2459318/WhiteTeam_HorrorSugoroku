using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource frontAudioSource; // 前方コライダー用AudioSource
    [SerializeField] private AudioSource backAudioSource;  // 後方コライダー用AudioSource
    [SerializeField] private AudioSource leftAudioSource;  // 左側コライダー用AudioSource
    [SerializeField] private AudioSource rightAudioSource; // 右側コライダー用AudioSource
    [SerializeField] private GameObject[] targetEnemies;   // 指定するエネミーオブジェクト

    private int activeEnemyContacts = 0; // 接触中のエネミー数を追跡

    public void OnColliderEnter(string colliderName, Collider other)
    {
        foreach (var enemy in targetEnemies)
        {
            if (other.gameObject == enemy)
            {
                activeEnemyContacts++;
                PlayAudioForCollider(colliderName);
                Debug.Log($"エネミー {enemy.name} に接触しました！（コライダー: {colliderName}）");
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
                Debug.Log($"エネミー {enemy.name} から離れました！（コライダー: {colliderName}）");
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