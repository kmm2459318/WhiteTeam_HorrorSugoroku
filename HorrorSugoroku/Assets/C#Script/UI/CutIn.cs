using TMPro;
using UnityEngine;
using System.Collections;

public class CutIn : MonoBehaviour
{
    public GameManager gameManager;
    public CurseSlider curseSlider;

    [SerializeField] private TextMeshProUGUI TEXT;
    [SerializeField, Header("�t�F�C�h�C��")]
    public float FadeInTime = 1.0f;  // �t�F�[�h�C������
    [SerializeField, Header("�t�F�C�h�A�E�g")]
    public float FadeOutTime = 1.0f; // �t�F�[�h�A�E�g����
    [SerializeField, Header("�\������")]
    public float DisplayTime = 1.5f; // ���S�ɕ\�����鎞��

    public GameObject Text_CutIn;
    private bool previousTurnState = false; // �O��̃^�[�����
    private bool isExecutingFade = false;  // �t�F�[�h�������t���O
    private bool isTurnChangedDuringFade = false; // �t�F�[�h���Ƀ^�[�����ς������
    private bool needsNextFadeIn = false;  // ���̃t�F�[�h�C�����K�v��

    void Start()
    {
        Text_CutIn.SetActive(false);
        TEXT.text = "";
        TEXT.color = new Color(1.0f, 1.0f, 1.0f, 0.0f); // ���S�ɓ���
        previousTurnState = gameManager.isPlayerTurn; // ������Ԃ�ۑ�
    }

    void Update()
    {
        // �J�[�h���\�����Ȃ�ҋ@
        if (!isExecutingFade && !curseSlider.isCardCanvas1 && !curseSlider.isCardCanvas2)
        {
            if (gameManager.isPlayerTurn != previousTurnState)
            {
                isExecutingFade = true;
                StartCoroutine(ShowCutIn());
            }
        }
        else if (isExecutingFade && gameManager.isPlayerTurn != previousTurnState)
        {
            // �t�F�[�h���Ƀ^�[�����ς�����瑦���Ƀt�F�[�h�A�E�g���A�V�����^�[���̕\����\��
            isTurnChangedDuringFade = true;
            needsNextFadeIn = true;
        }

        previousTurnState = gameManager.isPlayerTurn;
    }

    // �J�[�h�L�����o�X������܂őҋ@���Ă���t�F�[�h�C�����J�n
    public IEnumerator ShowCutIn()
    {
        Debug.Log("ShowCutIn() �J�n: �J�[�h�L�����o�X�̏�Ԃ��m�F");

        // �J�[�h�L�����o�X����\���ɂȂ�̂�ҋ@
        yield return StartCoroutine(WaitForCardCanvasToClose());

        // Text_CutIn���A�N�e�B�u�ł��邱�Ƃ��m�F
        Text_CutIn.SetActive(true);

        Debug.Log("�J�[�h�L�����o�X�������̂�Text_CutIn��\��");

        // �v���C���[�^�[���ɉ����ĕ\������e�L�X�g��ݒ�
        TEXT.text = gameManager.isPlayerTurn ? "DICE TURN" : "SEARCH TURN";

        StartCoroutine(FadeIn());
    }

    // �t�F�[�h�C������
    public IEnumerator FadeIn()
    {
        Debug.Log("FadeIn �J�n: �\�����镶�� = " + TEXT.text);

        float elapsedTime = 0f;
        while (elapsedTime < FadeInTime)
        {
            if (isTurnChangedDuringFade)
            {
                isTurnChangedDuringFade = false;
                StartCoroutine(FadeOut());
                yield break;
            }

            float alpha = elapsedTime / FadeInTime; // 0 �� 1 �ɕω�
            TEXT.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TEXT.color = new Color(1, 1, 1, 1); // ���S�ɕ\��

        float displayTime = 0f;
        while (displayTime < DisplayTime)
        {
            if (isTurnChangedDuringFade)
            {
                isTurnChangedDuringFade = false;
                StartCoroutine(FadeOut());
                yield break;
            }
            displayTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadeOut());
    }

    // �t�F�[�h�A�E�g����
    IEnumerator FadeOut()
    {
        Debug.Log("FadeOut �J�n");

        float elapsedTime = 0f;

        while (elapsedTime < FadeOutTime)
        {
            float alpha = 1 - (elapsedTime / FadeOutTime); // 1 �� 0 �ɕω�
            TEXT.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TEXT.color = new Color(1, 1, 1, 0); // ���S�ɓ���
        TEXT.text = "";
        Text_CutIn.SetActive(false);

        isExecutingFade = false;

        Debug.Log("FadeOut ����");

        // �t�F�[�h�A�E�g��Ɏ��̃t�F�[�h�C�����K�v�Ȃ���s
        if (needsNextFadeIn)
        {
            needsNextFadeIn = false;
            isExecutingFade = true;
            StartCoroutine(ShowCutIn());
        }
    }

    // CardCanvas1 �܂��� CardCanvas2 ������܂őҋ@
    private IEnumerator WaitForCardCanvasToClose()
    {
        Debug.Log("WaitForCardCanvasToClose() �J�n");

        while (curseSlider.isCardCanvas1 || curseSlider.isCardCanvas2)
        {
            yield return null; // 1�t���[���ҋ@
        }

        Debug.Log("WaitForCardCanvasToClose() ����: �J�[�h�L�����o�X������");
    }
}
