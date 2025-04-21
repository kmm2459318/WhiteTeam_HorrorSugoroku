using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnCard : MonoBehaviour
{
    public GameObject spriteCardFront;
    public GameObject spriteCardBack;

    static bool isFront = true;  // �J�[�h�̕\��
    static float speed = 4.0f;  // ���Ԃ��X�s�[�h ���Ԃ��ɂ�(2/speed)�b������

    RectTransform rectTransform;
    Image cardImage;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cardImage = GetComponent<Image>();
    }

    // �����ݒ�
    /*private void Start()
    {
        if (isFront)
        {
            //cardImage.sprite = spriteCardFront;
        }
        else
        {
            //cardImage.sprite = spriteCardBack;
        }
    }
    */

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
        if (isFront)
        {
            spriteCardBack.SetActive(true);
            spriteCardFront.SetActive(false);
        }
        else
        {
            spriteCardBack.SetActive(false);
            spriteCardFront.SetActive(true);
        }

        // �\����ύX
        isFront = !isFront;

        tick = 0f;

        // (1/speed)�b�Œ��Ԃ���Ō�܂łЂ�����Ԃ�
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(endScale, startScale, tick);

            rectTransform.localScale = localScale;

            yield return null;
        }
    }
}
