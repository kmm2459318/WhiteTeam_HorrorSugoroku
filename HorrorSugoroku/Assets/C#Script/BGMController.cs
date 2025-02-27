using UnityEngine;
public class BGMController : MonoBehaviour
{
    public AudioClip undetectedBGM;
    public AudioClip discoveryBGM;
    //public EnemySaikoro enemySaikoro;
    private AudioSource audioSource; // ‰¹ºÄ¶—p‚ÌAudioSource
    private bool undet = false;
    private bool dis = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource‚ª‚È‚¯‚ê‚Î’Ç‰Á
        }
        audioSource.loop = true;

        audioSource.clip = undetectedBGM;
        audioSource.Play(); // –¢”­Œ©‚ÌBGM‚ğÄ¶
    }
    void Update()
    {
        /*if (!enemySaikoro.discovery && !undet)
        {
            audioSource.Stop(); // Œ»İ‚ÌBGM‚ğ’â~
            audioSource.clip = undetectedBGM;
            audioSource.Play(); // –¢”­Œ©‚ÌBGM‚ğÄ¶
            undet = true;
            dis = false;
        }
        else if (enemySaikoro.discovery && !dis)
        {
            audioSource.Stop(); // Œ»İ‚ÌBGM‚ğ’â~
            audioSource.clip = discoveryBGM;
            audioSource.Play(); // ”­Œ©‚ÌBGM‚ğÄ¶
            undet = false;
            dis = true;
        }*/
    }
}
