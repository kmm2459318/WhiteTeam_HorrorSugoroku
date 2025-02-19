using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    private bool isGameOverScene = false;
    [SerializeField] private TextMeshProUGUI pressSpaceText; // TextMeshProのテキストオブジェクトを参照
    [SerializeField] private float fadeDuration = 1.0f; // フェードインの時間

    void Start()
    {
        // 現在のシーンがGameOverシーンかどうかを確認
        if (SceneManager.GetActiveScene().name == "Gameover")
        {
            isGameOverScene = true;
            // テキストオブジェクトを非表示にする
            if (pressSpaceText != null)
            {
                pressSpaceText.gameObject.SetActive(false);
            }
            // 5秒後にフェードインを開始
            StartCoroutine(ShowTextWithFadeIn());
        }
    }

    void Update()
    {
        // GameOverシーンでスペースキーが押されたら
        if (isGameOverScene && Input.GetKeyDown(KeyCode.Space))
        {
            // Titleシーンに移動
            SceneManager.LoadScene("Title");
        }
    }

    private IEnumerator ShowTextWithFadeIn()
    {
        // 5秒待機
        yield return new WaitForSeconds(3.0f);

        // テキストオブジェクトを表示
        if (pressSpaceText != null)
        {
            pressSpaceText.gameObject.SetActive(true);
            pressSpaceText.alpha = 0.0f; // 初期透明度を0に設定
            StartCoroutine(FadeInText());
        }
    }

    private IEnumerator FadeInText()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            pressSpaceText.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }
}