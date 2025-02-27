using UnityEngine;
public class BGMController : MonoBehaviour
{
    public AudioClip undetectedBGM;
    public AudioClip discoveryBGM;
    //public EnemySaikoro enemySaikoro;
    private AudioSource audioSource; // �����Đ��p��AudioSource
    private bool undet = false;
    private bool dis = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource���Ȃ���Βǉ�
        }
        audioSource.loop = true;

        audioSource.clip = undetectedBGM;
        audioSource.Play(); // ����������BGM���Đ�
    }
    void Update()
    {
        /*if (!enemySaikoro.discovery && !undet)
        {
            audioSource.Stop(); // ���݂�BGM���~
            audioSource.clip = undetectedBGM;
            audioSource.Play(); // ����������BGM���Đ�
            undet = true;
            dis = false;
        }
        else if (enemySaikoro.discovery && !dis)
        {
            audioSource.Stop(); // ���݂�BGM���~
            audioSource.clip = discoveryBGM;
            audioSource.Play(); // ��������BGM���Đ�
            undet = false;
            dis = true;
        }*/
    }
}
