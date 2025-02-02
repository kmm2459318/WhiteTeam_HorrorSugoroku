using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger3D : MonoBehaviour
{
    [SerializeField] private GameObject enemy; // 敵オブジェクトの名前
    [SerializeField] private Image cutInImage; // カットイン画像
    [SerializeField] private float cutInDuration = 2.0f; // カットインの表示時間（秒）
    [SerializeField] private AudioClip gameOverSound; // ゲームオーバー時のサウンド
    private AudioSource audioSource; // 音声再生用のAudioSource

    [SerializeField] private float volume = 1.0f; // 音量 (デフォルトは最大)

    private bool isGameOver = false; // 重複処理防止用フラグ
    public static bool hasSubstituteDoll = false; // 身代わり人形の使用フラグ

    private void Start()
    {
        // AudioSourceの初期化
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceがアタッチされていない場合は追加
        }

        // 音量の設定
        audioSource.volume = volume;

        // 最初に音が鳴らないように、音を再生しない
        audioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGameOver && collision.gameObject == enemy)
        {
            HandleGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver && other.gameObject == enemy)
        {
            HandleGameOver();
        }
    }

    // ゲームオーバー処理を判定するメソッド
    public void HandleGameOver()
    {
        if (hasSubstituteDoll)
        {
            // 身代わり人形がある場合は回避
            hasSubstituteDoll = false; // 身代わり人形を消費
            Debug.Log("身代わり人形が発動！ゲームオーバーを回避！");
        }
        else
        {
            StartCoroutine(ShowCutInAndGoToGameover()); // ゲームオーバー処理を実行
        }
    }

    // カットイン画像を表示してからゲームオーバーシーンに遷移する処理
    private IEnumerator ShowCutInAndGoToGameover()
    {
        isGameOver = true; // 重複処理防止用フラグ

        // 他のUI要素（テキストなど）を非表示にする
        HideAllUI(); // UI非表示処理を実行

        // カットイン画像を表示
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(true); // 画像を表示
        }

        // ゲームオーバーサウンドを再生
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.clip = gameOverSound; // サウンドを設定
            audioSource.Play(); // 音を鳴らす
        }

        // 指定された時間だけ待機
        yield return new WaitForSeconds(cutInDuration);

        // カットイン画像を非表示にする
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(false); // 画像を非表示
        }

        // ゲームオーバーシーンへ遷移
        SceneManager.LoadScene("Gameover");
    }

    // UIの他の要素（テキストやその他の画像）を非表示にするメソッド
    private void HideAllUI()
    {
        // 他のUI要素があれば非表示にします。例えば、テキストやボタンなど。
        // ここでテキストやボタンを非表示にする処理を追加してください。
        // 例:
        // if (someText != null) someText.gameObject.SetActive(false);
        // if (someButton != null) someButton.gameObject.SetActive(false);
    }
}
