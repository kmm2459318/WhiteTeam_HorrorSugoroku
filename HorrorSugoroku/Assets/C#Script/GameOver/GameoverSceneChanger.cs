using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SceneChanger3D : MonoBehaviour
{
    public JumpScareAnimation jumpScareAnimation;

    [SerializeField] private List<GameObject> enemies; // 敵オブジェクトのリスト
    [SerializeField] private Image cutInImage; // カットイン画像
    [SerializeField] private float cutInDuration = 2.0f; // カットインの表示時間（秒）
    [SerializeField] private AudioClip gameOverSound; // ゲームオーバー時のサウンド
    public SubstitutedollController substitutedollController;
    private AudioSource audioSource; // 音声再生用のAudioSource
    private GameObject atackEnemy;

    [SerializeField] private float volume = 1.0f; // 音量 (デフォルトは最大)
    private bool isGameOver = false; // 重複処理防止用フラグ
    public static bool hasSubstituteDoll = false; // 身代わり人形の使用フラグ
    public CurseSlider curseslider;

    [SerializeField] private Camera mainCamera; // メインカメラ
    [SerializeField] private Camera jumpScareCamera; // ジャンプスケア用のカメラ

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

        // 最初はジャンプスケアカメラを無効にしておく
        if (jumpScareCamera != null)
        {
            jumpScareCamera.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver && enemies.Contains(other.gameObject))
        {
            atackEnemy = other.gameObject;
            HandleGameOver();
        }
        else if (enemies.Contains(other.gameObject) && (curseslider.CountGauge < 2))
        {
            CurseGaugeUP();
        }
    }

    public void HandleGameOver()
    {
        if (substitutedollController.itemCount > 0)
        {
            // 身代わり人形がある場合は回避
            hasSubstituteDoll = false; // 身代わり人形を消費
            Debug.Log("身代わり人形が発動！ゲームオーバーを回避！");
            substitutedollController.itemCount--;
            atackEnemy.transform.position = new Vector3(0f, 0f, 0.1016667f);
        }
        else
        {
            StartCoroutine(ShowCutInAndGoToGameover());
        }
    }

    private IEnumerator ShowCutInAndGoToGameover()
    {
        isGameOver = true; // 重複処理防止用フラグ

        // ジャンプスケアカメラに切り替える
        if (jumpScareCamera != null && mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            jumpScareCamera.gameObject.SetActive(true);
        }

        jumpScareAnimation.StartAnimation();

        // 他のUI要素を非表示
        HideAllUI();

        // ゲームオーバーサウンドを再生
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.clip = gameOverSound;
            audioSource.Play();
        }

        // 指定された時間待機
        yield return new WaitForSeconds(cutInDuration);

        // ゲームオーバーシーンへ遷移
        SceneManager.LoadScene("Gameover");
    }

    public void CurseGaugeUP()
    {
        if (hasSubstituteDoll)
        {
            hasSubstituteDoll = false;
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
        yield return new WaitForSeconds(2.0f);
    }

    private void HideAllUI()
    {
        // ここで他のUI要素を非表示にする処理を追加
    }
}
