using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SceneChanger3D : MonoBehaviour
{
    public JumpScareAnimation jumpScareAnimation;

    [SerializeField] private List<GameObject> enemies; // �G�I�u�W�F�N�g�̃��X�g
    [SerializeField] private Image cutInImage; // �J�b�g�C���摜
    [SerializeField] private float cutInDuration = 2.0f; // �J�b�g�C���̕\�����ԁi�b�j
    [SerializeField] private AudioClip gameOverSound; // �Q�[���I�[�o�[���̃T�E���h
    public SubstitutedollController substitutedollController;
    private AudioSource audioSource; // �����Đ��p��AudioSource
    private GameObject atackEnemy;

    [SerializeField] private float volume = 1.0f; // ���� (�f�t�H���g�͍ő�)
    private bool isGameOver = false; // �d�������h�~�p�t���O
    public static bool hasSubstituteDoll = false; // �g����l�`�̎g�p�t���O
    public CurseSlider curseslider;

    [SerializeField] private Camera mainCamera; // ���C���J����
    [SerializeField] private Camera jumpScareCamera; // �W�����v�X�P�A�p�̃J����

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

        // �ŏ��̓W�����v�X�P�A�J�����𖳌��ɂ��Ă���
        if (jumpScareCamera != null)
        {
            jumpScareCamera.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver && enemies.Contains(other.gameObject))
        {
            atackEnemy = other.gameObject;
            HandleGameOver();
        }
        else if (enemies.Contains(other.gameObject) && (curseslider.CountGauge < 2))
        {
            CurseGaugeUP();
        }
    }

    public void HandleGameOver()
    {
        if (substitutedollController.itemCount > 0)
        {
            // �g����l�`������ꍇ�͉��
            hasSubstituteDoll = false; // �g����l�`������
            Debug.Log("�g����l�`�������I�Q�[���I�[�o�[������I");
            substitutedollController.itemCount--;
            atackEnemy.transform.position = new Vector3(0f, 0f, 0.1016667f);
        }
        else
        {
            StartCoroutine(ShowCutInAndGoToGameover());
        }
    }

    private IEnumerator ShowCutInAndGoToGameover()
    {
        isGameOver = true; // �d�������h�~�p�t���O

        // �W�����v�X�P�A�J�����ɐ؂�ւ���
        if (jumpScareCamera != null && mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            jumpScareCamera.gameObject.SetActive(true);
        }

        jumpScareAnimation.StartAnimation();

        // ����UI�v�f���\��
        HideAllUI();

        // �Q�[���I�[�o�[�T�E���h���Đ�
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.clip = gameOverSound;
            audioSource.Play();
        }

        // �w�肳�ꂽ���ԑҋ@
        yield return new WaitForSeconds(cutInDuration);

        // �Q�[���I�[�o�[�V�[���֑J��
        SceneManager.LoadScene("Gameover");
    }

    public void CurseGaugeUP()
    {
        if (hasSubstituteDoll)
        {
            hasSubstituteDoll = false;
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
        yield return new WaitForSeconds(2.0f);
    }

    private void HideAllUI()
    {
        // �����ő���UI�v�f���\���ɂ��鏈����ǉ�
    }
}
