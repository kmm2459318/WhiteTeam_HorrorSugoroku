using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理用
using UnityEngine.UI; // UIを使用するため

public class SceneChanger3D : MonoBehaviour
{
    [SerializeField] private string enemyObjectName = "Enemy"; // 敵オブジェクトの名前
    [SerializeField] private Image cutInImage; // カットイン用のUI画像
    [SerializeField] private float cutInDuration = 2.0f; // カットインの表示時間（秒）

    private bool isGameOver = false; // 重複処理防止用フラグ

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGameOver && collision.gameObject.name == enemyObjectName)
        {
            StartCoroutine(ShowCutInAndGoToGameover()); // コルーチンを開始
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver && other.gameObject.name == enemyObjectName)
        {
            StartCoroutine(ShowCutInAndGoToGameover()); // コルーチンを開始
        }
    }

    // カットインを表示してからゲームオーバーシーンに遷移する処理
    private IEnumerator ShowCutInAndGoToGameover()
    {
        isGameOver = true; // 処理が重複しないようにフラグを立てる

        // カットイン画像を表示
        if (cutInImage != null)
        {
            cutInImage.gameObject.SetActive(true); // カットイン画像をアクティブ化
        }

        // 指定した時間だけ待機
        yield return new WaitForSeconds(cutInDuration);

        // ゲームオーバーシーンに移動
        SceneManager.LoadScene("Gameover");
    }
}
