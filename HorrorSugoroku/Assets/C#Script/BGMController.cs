using UnityEngine;
public class BGMController : MonoBehaviour
{
    public AudioClip undetectedBGM;
    public AudioClip discoveryBGM;
    //public EnemySaikoro enemySaikoro;
    private AudioSource audioSource; // 音声再生用のAudioSource
    private bool undet = false;
    private bool dis = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceがなければ追加
        }
        audioSource.loop = true;

        audioSource.clip = undetectedBGM;
        audioSource.Play(); // 未発見時のBGMを再生
    }
    void Update()
    {
        /*if (!enemySaikoro.discovery && !undet)
        {
            audioSource.Stop(); // 現在のBGMを停止
            audioSource.clip = undetectedBGM;
            audioSource.Play(); // 未発見時のBGMを再生
            undet = true;
            dis = false;
        }
        else if (enemySaikoro.discovery && !dis)
        {
            audioSource.Stop(); // 現在のBGMを停止
            audioSource.clip = discoveryBGM;
            audioSource.Play(); // 発見時のBGMを再生
            undet = false;
            dis = true;
        }*/
    }
}
