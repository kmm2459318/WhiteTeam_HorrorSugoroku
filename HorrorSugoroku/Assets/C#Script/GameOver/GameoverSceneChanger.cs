using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.AI;

public class SceneChanger3D : MonoBehaviour
{
    //public JumpScareAnimation jumpScareAnimation;

    [SerializeField] private List<GameObject> enemies; // 敵オブジェクトのリスト
    [SerializeField] private Image cutInImage; // カットイン画像
    [SerializeField] private float cutInDuration = 2.0f; // カットインの表示時間（秒）
    [SerializeField] private AudioClip gameOverSound; // ゲームオーバー時のサウンド
    public SubstitutedollController substitutedollController;
    private AudioSource audioSource; // 音声再生用のAudioSource
    private GameObject atackEnemy;
    private Vector3 ResetPos = new Vector3(0f, 0f, 0.1016667f);

    [SerializeField] private float volume = 1.0f; // 音量 (デフォルトは最大)

    private bool isGameOver = false;    // 重複処理防止用フラグ
   /* private bool isCurseGauga = false; */ // 重複処理防止用フラグ
    public static bool hasSubstituteDoll = false; // 身代わり人形の使用フラグ

    public CurseSlider curseslider;

    [SerializeField] private GameObject substituteEffect; // 身代わり人形のエフェクトオブジェクト

    [SerializeField] private AudioClip substituteSound; // 身代わり人形のエフェクト音
    private AudioSource effectAudioSource;
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
        effectAudioSource = GetComponent<AudioSource>();
        if (effectAudioSource == null)
        {
            effectAudioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceを追加
        }

    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("AAAAAAAAAAAAAA");
    //    if (!isGameOver && enemies.Contains(collision.gameObject) && (curseslider.CountGauge == 3))
    //    {
    //        HandleGameOver();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    { 
        
        if (!isGameOver && enemies.Contains(other.gameObject))
        {
            atackEnemy = other.gameObject;
            Debug.Log(atackEnemy);
            HandleGameOver();
        }

        else if(enemies.Contains(other.gameObject) && (curseslider.CountGauge < 2))
        {
            CurseGaugeUP();
        }
    }


    // ゲームオーバー処理を判定するメソッド
    public void HandleGameOver()
    {

        if (substitutedollController.useCount > 0)
        {
            hasSubstituteDoll = false;
            Debug.Log("身代わり人形が発動！ゲームオーバーを回避！");
            substitutedollController.useCount--;

            // **メソッドが存在するか確認しつつ呼び出し**
            StartCoroutine(PlaySubstituteEffect());

            atackEnemy.GetComponent<NavMeshAgent>().Warp(ResetPos);
            atackEnemy.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            StartCoroutine(ShowCutInAndGoToGameover());
        }
    }

    // カットイン画像を表示してからゲームオーバーシーンに遷移する処理
    private IEnumerator ShowCutInAndGoToGameover()
    {
        isGameOver = true; // 重複処理防止用フラグ

        SceneManager.LoadScene("Jump Scare");

        //jumpScareAnimation.StartAnimation();
        // 他のUI要素（テキストなど）を非表示にする
        HideAllUI(); // UI非表示処理を実行

        // カットイン画像を表示
        //if (cutInImage != null)
        //{
        //    cutInImage.gameObject.SetActive(true); // 画像を表示
        //}
                                                                 
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
            // cutInImage.gameObject.SetActive(false); // 画像を非表示
        }

        // ゲームオーバーシーンへ遷移
        SceneManager.LoadScene("Gameover");
    }

    public void CurseGaugeUP()
    {
        if (hasSubstituteDoll)
        {
            // 身代わり人形がある場合は回避
            hasSubstituteDoll = false; // 身代わり人形を消費
            Debug.Log("身代わり人形が発動！ゲームオーバーを回避！");
        }
        else
        {
            StartCoroutine(ShowCutInAndGoToCurseGaugeUP());
            
        }
    }
    private IEnumerator ShowCutInAndGoToCurseGaugeUP()
    {
        if (curseslider.dashPoint < 100)
        {
            curseslider.dashPoint = 0;
            curseslider.dashPoint += 100;
        }
        yield return new WaitForSeconds(2.0f); // 1秒待機（例）
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
    private IEnumerator PlaySubstituteEffect()
    {
        if (substituteEffect != null)
        {
            substituteEffect.SetActive(true); // エフェクト表示

            // **音を再生**
            if (substituteSound != null && effectAudioSource != null)
            {
                effectAudioSource.clip = substituteSound;
                effectAudioSource.Play();
            }

            yield return new WaitForSeconds(2.0f); // 2秒待機

            substituteEffect.SetActive(false); // エフェクト非表示
        }
    }
}