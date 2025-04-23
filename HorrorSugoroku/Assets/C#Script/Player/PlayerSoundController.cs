using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource frontAudioSource; // 前方コライダー用AudioSource
    [SerializeField] private AudioSource backAudioSource;  // 後方コライダー用AudioSource
    [SerializeField] private AudioSource leftAudioSource;  // 左側コライダー用AudioSource
    [SerializeField] private AudioSource rightAudioSource; // 右側コライダー用AudioSource
    [SerializeField] private GameObject[] targetEnemies;   // 指定するエネミーオブジェクト
    private int activeEnemyContacts = 0;                  // 接触中のエネミー数を追跡

    private void Start()
    {
        // ゲーム開始時に音声を停止
        if (frontAudioSource != null) frontAudioSource.Stop();
        if (backAudioSource != null) backAudioSource.Stop();
        if (leftAudioSource != null) leftAudioSource.Stop();
        if (rightAudioSource != null) rightAudioSource.Stop();

        Debug.Log("ゲーム開始時にすべての音声を停止しました。");
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var enemy in targetEnemies)
        {
            if (other.gameObject == enemy)
            {
                activeEnemyContacts++;
                PlayAudioForCollider(this.gameObject.name);
                Debug.Log($"エネミー {enemy.name} に接触しました！");
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
                Debug.Log($"エネミー {enemy.name} から離れました！");
                break;
            }
        }
    }

    private void PlayAudioForCollider(string colliderName)
    {
        Debug.Log($"音声再生処理開始: {colliderName}");
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
        Debug.Log($"音声停止処理開始: {colliderName}");
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