using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnCard : MonoBehaviour
{
    public GameObject spriteCardFront;
    public GameObject spriteCardBack;

    static bool isFront = true;  // カードの表裏
    static float speed = 4.0f;  // 裏返すスピード 裏返しには(2/speed)秒かかる

    RectTransform rectTransform;
    Image cardImage;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cardImage = GetComponent<Image>();
    }

    // 初期設定
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

    // コルーチンの開始
    public void StartTurn()
    {
        StartCoroutine(Turn());
    }

    // 裏返す
    IEnumerator Turn()
    {
        float tick = 0f;

        Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f); // 開始時のスケール
        Vector3 endScale = new Vector3(0f, 1.0f, 1.0f); // 中間地点のスケール (x = 0)

        Vector3 localScale = new Vector3();


        // (1/speed)秒で中間地点までひっくり返す
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(startScale, endScale, tick); // 線形補間

            rectTransform.localScale = localScale;

            yield return null;
        }

        // カードの画像(sprite)を変更する
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

        // 表裏を変更
        isFront = !isFront;

        tick = 0f;

        // (1/speed)秒で中間から最後までひっくり返す
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(endScale, startScale, tick);

            rectTransform.localScale = localScale;

            yield return null;
        }
    }
}
