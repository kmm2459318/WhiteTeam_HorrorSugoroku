using TMPro;
using UnityEngine;
using System.Collections;

public class CutIn : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] private TextMeshProUGUI TEXT;
    [SerializeField, Header("�t�F�C�h�C��")]
    public float FadeInTime = 1.0f;  // �t�F�[�h�C������
    [SerializeField, Header("�t�F�C�h�A�E�g")]
    public float FadeOutTime = 1.0f; // �t�F�[�h�A�E�g����
    [SerializeField, Header("�\������")]
    public float DisplayTime = 1.5f; // ���S�ɕ\�����鎞��

    private bool previousTurnState = false; // �O��̃^�[�����

    void Start()
    {
        TEXT.text = "";
        TEXT.color = new Color(1.0f, 1.0f, 1.0f, 0.0f); // ���S�ɓ���
        previousTurnState = gameManager.isPlayerTurn; // ������Ԃ�ۑ�
    }

    void Update()
    {
        // `isPlayerTurn` �� true �ɕς�����Ƃ��� 1 �񂾂� `NextTurn()` �����s
        if (gameManager.isPlayerTurn && !previousTurnState)
        {
            StartCoroutine(FadeIn());
        }

        // ���݂̏�Ԃ��X�V
        previousTurnState = gameManager.isPlayerTurn;
    }

    // �t�F�C�h�C���̏���
    IEnumerator FadeIn()
    {
        TEXT.text = "PLAYER TURN";
        float elapsedTime = 0f;

        while (elapsedTime < FadeInTime)
        {
            float alpha = elapsedTime / FadeInTime; // 0 �� 1 �ɕω�
            TEXT.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TEXT.color = new Color(1, 1, 1, 1); // ���S�ɕ\��
        yield return new WaitForSeconds(DisplayTime); // �\�����ԑҋ@

        StartCoroutine(FadeOut());
    }

    // �t�F�C�h�A�E�g�̏���
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < FadeOutTime)
        {
            float alpha = 1 - (elapsedTime / FadeOutTime); // 1 �� 0 �ɕω�
            TEXT.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TEXT.color = new Color(1, 1, 1, 0); // ���S�ɓ���
        TEXT.text = ""; // �t�F�[�h�A�E�g��e�L�X�g������
    }
}
