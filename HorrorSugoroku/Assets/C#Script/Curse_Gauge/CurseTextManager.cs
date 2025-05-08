using TMPro;
using UnityEngine;

public class CurseTextManager : MonoBehaviour
{
    public TextMeshProUGUI curseText;
    public GameObject conditionObject; // テキスト表示の条件となるオブジェクト

    private void Start()
    {
        // シーンの開始時に呪い発動テキストを非表示にする
        if (curseText != null)
        {
            curseText.gameObject.SetActive(false);
            Debug.Log("シーンの開始時に呪い発動テキストを非表示にします");
        }
        else
        {
            Debug.LogError("curseTextが設定されていません");
        }
    }

    private void Update()
    {
        // 条件オブジェクトがアクティブかどうかを監視
        if (conditionObject != null && conditionObject.activeSelf)
        {
            ShowCurseText();
        }
        else
        {
            HideCurseText();
        }
    }

    public void ShowCurseText()
    {
        if (curseText != null && !curseText.gameObject.activeSelf)
        {
            curseText.gameObject.SetActive(true);
            Debug.Log("呪い発動テキストを表示します");
        }
    }

    public void HideCurseText()
    {
        if (curseText != null && curseText.gameObject.activeSelf)
        {
            curseText.gameObject.SetActive(false);
            Debug.Log("呪い発動テキストを非表示にします");
        }
    }
}
