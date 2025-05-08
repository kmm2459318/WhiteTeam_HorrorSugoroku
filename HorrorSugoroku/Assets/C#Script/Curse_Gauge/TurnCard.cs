using System.Collections;
using UnityEngine;

public class TurnCard : MonoBehaviour
{
    public GameObject spriteCardFront;
    public GameObject spriteCardBack;
    public CurseSlider curseGauge;
    public CurseTextManager curseTextManager; // �􂢔����e�L�X�g�}�l�[�W���[

    static float speed = 4.0f; // ���Ԃ��X�s�[�h ���Ԃ��ɂ�(2/speed)�b������

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // �R���[�`���̊J�n
    public void StartTurn()
    {
        StartCoroutine(Turn());
    }

    // ���Ԃ�
    IEnumerator Turn()
    {
        float tick = 0f;

        Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f); // �J�n���̃X�P�[��
        Vector3 endScale = new Vector3(0f, 1.0f, 1.0f); // ���Ԓn�_�̃X�P�[�� (x = 0)

        Vector3 localScale = new Vector3();

        // (1/speed)�b�Œ��Ԓn�_�܂łЂ�����Ԃ�
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(startScale, endScale, tick); // ���`���

            rectTransform.localScale = localScale;

            yield return null;
        }

        // �J�[�h�̉摜(sprite)��ύX����
        spriteCardBack.SetActive(true);
        spriteCardFront.SetActive(false);

        tick = 0f;

        // (1/speed)�b�Œ��Ԃ���Ō�܂łЂ�����Ԃ�
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(endScale, startScale, tick);

            rectTransform.localScale = localScale;

            yield return null;
        }

        // �J�[�h�̉摜(sprite)��ύX����
        spriteCardFront.SetActive(true);

        // �f�o�b�O���O��ǉ�
        Debug.Log("�J�[�h�����Ԃ���܂���");

        curseGauge.endTurn = true;
    }

    public void CardReset()
    {
        spriteCardBack.SetActive(false);
        spriteCardFront.SetActive(true);

        Debug.Log("�J�[�h�����Z�b�g����܂���");
    }
}
