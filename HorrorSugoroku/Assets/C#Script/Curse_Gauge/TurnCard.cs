using System.Collections;
using UnityEngine;

public class TurnCard : MonoBehaviour
{
    public GameObject spriteCardFront;
    public GameObject spriteCardBack;
    public GameObject spriteCardBackcard;
    public CurseSlider curseGauge;
    public CurseTextManager curseTextManager; // �􂢔����e�L�X�g�}�l�[�W���[

    static float speed = 4.0f; // ���Ԃ��X�s�[�h ���Ԃ��ɂ�(2/speed)�b������

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // �R���[�`���̊J�n
    public void StartTurn(int n, int m)
    {
        if (n == 1)
        {
            spriteCardBack = curseGauge.Curse1Canvas;
            spriteCardBackcard = curseGauge.Curse1Card;
            Debug.Log("��P�F�G�̍Œ�ړ���������");
            curseGauge.curse1_1 = true;
        }
        else if (n == 2)
        {
            spriteCardBack = curseGauge.Curse2Canvas;
            spriteCardBackcard = curseGauge.Curse2Card;
            Debug.Log("��Q�F�v���C���[�̕���������");
            curseGauge.curse1_2 = true;
        }
        else if (n == 3)
        {
            spriteCardBack = curseGauge.Curse3Canvas;
            spriteCardBackcard = curseGauge.Curse3Card;
            Debug.Log("��R�F�񕜁A���G�A�C�e���̎擾�s��");
            curseGauge.curse1_3 = true;
        }

        if (m == 12)
        {
            spriteCardBack.GetComponent<RectTransform>().position = new Vector3(445f, 540);
            spriteCardFront = curseGauge.Card12;
        }
        else if (m == 34)
        {
            spriteCardBack.GetComponent<RectTransform>().position = new Vector3(960, 540);
            spriteCardFront = curseGauge.Card34;
        }
        else if (m == 56)
        {
            spriteCardBack.GetComponent<RectTransform>().position = new Vector3(1475f, 540);
            spriteCardFront = curseGauge.Card56;
        }

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

            spriteCardFront.transform.localScale = localScale;

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

            spriteCardBackcard.transform.localScale = localScale;

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
        spriteCardFront.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        spriteCardFront.SetActive(true);

        Debug.Log("�J�[�h�����Z�b�g����܂���");
    }
}
