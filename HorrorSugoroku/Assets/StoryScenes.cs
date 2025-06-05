using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class StorySceneController : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;
    [SerializeField] private float delay = 0.05f;

    [SerializeField] private string nextSceneName = "GameScene";

    private string[] storyMessages = new string[]
    {
        "お父さんとお母さんが誕生日プレゼントに少し古びた人形の家を買ってくれた。",
        "骨董屋さんで買ってきたらしい。",
        "人形の家の中にはサイコロと人形が入っていて、\n人形はお世辞にも可愛いとは、言えるものではなかった。",
        "また、このサイコロは何に使うのか見当もつかない。",
        "少し気味が悪かったが両親が買ってきてくれたものなので、\nいらないとは言えなかった。",
        "夜、両親が笑いながら部屋をのぞいてくるのが、\n気味悪かったが、気づいたら寝てしまっていた。",
        "",
        "目が覚めるとそこは知らない屋敷の中だった。",
        "どこからともなく声が聞こえる\n「人形を集めろ」...だって？",
        "よくわからないけど今はやるしかない。"
    };

    private void Start()
    {
        StartCoroutine(PlayStory());
    }

    IEnumerator PlayStory()
    {
        foreach (string message in storyMessages)
        {
            if (!string.IsNullOrEmpty(message))
            {
                audioSource.clip = typingSound;
                audioSource.loop = true;
                audioSource.Play();
            }

            yield return StartCoroutine(ShowText(message));

            audioSource.Stop();
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator ShowText(string text)
    {
        uiText.text = "";
        for (int i = 1; i <= text.Length; i++)
        {
            string currentChar = text.Substring(i - 1, 1);
            uiText.text = text.Substring(0, i);

            // 音の重なりを避ける：PlayOneShotは使わず Play() のみ
            // ただしタイピング感を出すために子音/母音系のみ音を鳴らす想定なら下記で分岐可能
         
            yield return new WaitForSeconds(delay);
        }
    }
}
