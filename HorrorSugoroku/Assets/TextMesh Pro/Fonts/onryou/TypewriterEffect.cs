using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffectUI : MonoBehaviour
{
    public enum GameResult { Clear, Over } // ゲーム結果のEnum
    [SerializeField] private GameResult gameResult; // インスペクターで設定

    [SerializeField] private Text uiText;
    [SerializeField] private Text gameResultText; // GAME CLEAR / GAME OVER のテキスト
    [SerializeField] private AudioSource audioSource; // 音声再生用
    [SerializeField] private AudioClip typingSound; // タイプ音
    [SerializeField] private AudioClip gameClearSound; // ゲームクリア用音
    [SerializeField] private AudioClip gameOverSound; // ゲームオーバー用音
    [SerializeField] private float delay = 0.1f;

    private string[] clearMessages = {
       "気が付くと病院の中だった。",
       "パパとママがとても心配してくれている。\nあれから、3日ほどたっていたんだって。",
       "久しぶりにパパとママの顔を見れてうれしかった。",
       "あの日のことを二人に聞いてみたけど、\n覚えていなかったみたい。",
       "おうちに帰って、人形を探したけど...",
       "見つからなかった",
       "", // 空白部分では音を流さない
       "あれはいったいなんだったの。"
    };

    private string[] overMessages = {
       "気が付くと病院の中だった。",
       "パパとママがとても心配してくれている。\nあれから、3日ほどたっていたんだって。",
       "久しぶりにふたりに会えてうれしい。",
       "あの日のことをふたりに聞いてみたけど\nおぼえていなかったみたい。",
       "ママ？泣いているの？わたしはだいじょうぶ。",
       "みえないし、うごけないけど生きてるから...",
       "", // 空白部分では音を流さない
       "あの%/&?$は何だったのかな、"
    };

    void Start()
    {
        gameResultText.gameObject.SetActive(false); // 最初は非表示
        StartCoroutine(DisplayMessages());
    }

    IEnumerator DisplayMessages()
    {
        string[] messages = (gameResult == GameResult.Clear) ? clearMessages : overMessages; // 状態に応じてメッセージ変更

        foreach (string message in messages)
        {
            if (!string.IsNullOrEmpty(message)) // 空白なら音なし
            {
                audioSource.clip = typingSound;
                audioSource.Play();
            }

            yield return StartCoroutine(ShowText(message));

            audioSource.Stop(); // メッセージが終わったら音を止める
            yield return new WaitForSeconds(1f); // メッセージ間の待機時間
        }

        yield return new WaitForSeconds(1f);
        uiText.gameObject.SetActive(false); // メッセージを非表示
        gameResultText.gameObject.SetActive(true); // ゲーム結果を表示

        // ゲームクリアかゲームオーバーかで音声を切り替え
        if (gameResult == GameResult.Clear)
        {
            gameResultText.text = "GAME CLEAR";
            audioSource.clip = gameClearSound;
        }
        else
        {
            gameResultText.text = "GAME OVER";
            audioSource.clip = gameOverSound;
        }

        audioSource.Play(); // 選択された結果の音を再生
    }

    IEnumerator ShowText(string text)
    {
        uiText.text = "";
        for (int i = 0; i <= text.Length; i++)
        {
            uiText.text = text.Substring(0, i);
            audioSource.PlayOneShot(typingSound); // 文字表示時のタイプ音
            yield return new WaitForSeconds(delay);
        }
    }
}