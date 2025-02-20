using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SceneChanger3D : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies; // �G�I�u�W�F�N�g�̃��X�g
    [SerializeField] private Image cutInImage; // �J�b�g�C���摜
    [SerializeField] private float cutInDuration = 2.0f; // �J�b�g�C���̕\�����ԁi�b�j
    [SerializeField] private AudioClip gameOverSound; // �Q�[���I�[�o�[���̃T�E���h
    private AudioSource audioSource; // �����Đ��p��AudioSource

    [SerializeField] private float volume = 1.0f; // ���� (�f�t�H���g�͍ő�)

    private bool isGameOver = false;    // �d�������h�~�p�t���O
   /* private bool isCurseGauga = false; */ // �d�������h�~�p�t���O
    public static bool hasSubstituteDoll = false; // �g����l�`�̎g�p�t���O

    public CurseSlider curseslider;
    private void Start()
    {
        // AudioSource�̏�����
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource���A�^�b�`����Ă��Ȃ��ꍇ�͒ǉ�
        }

        // ���ʂ̐ݒ�
        audioSource.volume = volume;

        // �ŏ��ɉ�����Ȃ��悤�ɁA�����Đ����Ȃ�
        audioSource.Stop();
    }

    void Update()
    {
        HandleGameOver2();
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("AAAAAAAAAAAAAA");
    //    if (!isGameOver && enemies.Contains(collision.gameObject) && (curseslider.CountGauge == 3))
    //    {
    //        HandleGameOver();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    { 
        
        if (!isGameOver && enemies.Contains(other.gameObject) && (curseslider.CountGauge >= 2))
        {
            HandleGameOver();

        }

        else if(enemies.Contains(other.gameObject) && (curseslider.CountGauge < 2))
        {
            CurseGaugeUP();
        }
    }
    public void HandleGameOver2()
    {
        Debug.Log("CountGauge: " + curseslider.CountGauge);
        if (!isGameOver && (curseslider.CountGauge >= 2))
        {
            Debug.Log("CountGauge: " + curseslider.CountGauge);
            HandleGameOver();
        }
    }

    // �Q�[���I�[�o�[�����𔻒肷�郁�\�b�h
    public void HandleGameOver()
    {
        if (hasSubstituteDoll)
        {
            // �g����l�`������ꍇ�͉��
            hasSubstituteDoll = false; // �g����l�`������
            Debug.Log("�g����l�`�������I�Q�[���I�[�o�[������I");
        }
        else
        {
            StartCoroutine(ShowCutInAndGoToGameover()); // �Q�[���I�[�o�[���������s
        }
    }

    // �J�b�g�C���摜��\�����Ă���Q�[���I�[�o�[�V�[���ɑJ�ڂ��鏈��
    private IEnumerator ShowCutInAndGoToGameover()
    {
        isGameOver = true; // �d�������h�~�p�t���O

        // ����UI�v�f�i�e�L�X�g�Ȃǁj���\���ɂ���
        HideAllUI(); // UI��\�����������s

        // �J�b�g�C���摜��\��
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(true); // �摜��\��
        }

        // �Q�[���I�[�o�[�T�E���h���Đ�
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.clip = gameOverSound; // �T�E���h��ݒ�
            audioSource.Play(); // ����炷
        }

        // �w�肳�ꂽ���Ԃ����ҋ@
        yield return new WaitForSeconds(cutInDuration);

        // �J�b�g�C���摜���\���ɂ���
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(false); // �摜���\��
        }

        // �Q�[���I�[�o�[�V�[���֑J��
        SceneManager.LoadScene("Gameover");
    }

    public void CurseGaugeUP()
    {
        if (hasSubstituteDoll)
        {
            // �g����l�`������ꍇ�͉��
            hasSubstituteDoll = false; // �g����l�`������
            Debug.Log("�g����l�`�������I�Q�[���I�[�o�[������I");
        }
        else
        {
            StartCoroutine(ShowCutInAndGoToCurseGaugeUP());
            
        }
    }
    private IEnumerator ShowCutInAndGoToCurseGaugeUP()
    {
        if (curseslider.dashPoint < 100)
        {
            curseslider.dashPoint = 0;
            curseslider.dashPoint += 100;
        }
        yield return new WaitForSeconds(2.0f); // 1�b�ҋ@�i��j
    }
    // UI�̑��̗v�f�i�e�L�X�g�₻�̑��̉摜�j���\���ɂ��郁�\�b�h
    private void HideAllUI()
    {
        // ����UI�v�f������Δ�\���ɂ��܂��B�Ⴆ�΁A�e�L�X�g��{�^���ȂǁB
        // �����Ńe�L�X�g��{�^�����\���ɂ��鏈����ǉ����Ă��������B
        // ��:
        // if (someText != null) someText.gameObject.SetActive(false);
        // if (someButton != null) someButton.gameObject.SetActive(false);
    }
}