using System.Collections;
using UnityEngine;

public class TurnCard : MonoBehaviour
{
    public GameObject spriteCardFront;
    public GameObject spriteCardBack;
    public GameObject spriteCardBackcard;
    public CurseSlider curseGauge;
    public CurseTextManager curseTextManager; // 呪い発動テキストマネージャー

    static float speed = 4.0f; // 裏返すスピード 裏返しには(2/speed)秒かかる

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // コルーチンの開始
    public void StartTurn(int n, int m)
    {
        if (n == 1)
        {
            spriteCardBack = curseGauge.Curse1Canvas;
            spriteCardBackcard = curseGauge.Curse1Card;
            Debug.Log("呪１：敵の最低移動数が増加");
            curseGauge.curse1_1 = true;
        }
        else if (n == 2)
        {
            spriteCardBack = curseGauge.Curse2Canvas;
            spriteCardBackcard = curseGauge.Curse2Card;
            Debug.Log("呪２：プレイヤーの歩数が減少");
            curseGauge.curse1_2 = true;
        }
        else if (n == 3)
        {
            spriteCardBack = curseGauge.Curse3Canvas;
            spriteCardBackcard = curseGauge.Curse3Card;
            Debug.Log("呪３：回復、無敵アイテムの取得不可");
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

            spriteCardFront.transform.localScale = localScale;

            yield return null;
        }

        // カードの画像(sprite)を変更する
        spriteCardBack.SetActive(true);
        spriteCardFront.SetActive(false);

        tick = 0f;

        // (1/speed)秒で中間から最後までひっくり返す
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(endScale, startScale, tick);

            spriteCardBackcard.transform.localScale = localScale;

            yield return null;
        }

        // カードの画像(sprite)を変更する
        spriteCardFront.SetActive(true);

        // デバッグログを追加
        Debug.Log("カードが裏返されました");

        curseGauge.endTurn = true;
    }

    public void CardReset()
    {
        spriteCardFront.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        spriteCardFront.SetActive(true);

        Debug.Log("カードがリセットされました");
    }
}
