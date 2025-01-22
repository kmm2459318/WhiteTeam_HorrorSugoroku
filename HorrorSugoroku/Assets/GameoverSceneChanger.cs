using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger3 : MonoBehaviour
{
    // 敵オブジェクトを直接設定
    [SerializeField] private GameObject enemyObject;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ぶつかったよ");
        // 敵オブジェクト参照で判定
        if (collision.gameObject == enemyObject)
        {
            GoToGameover();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 敵オブジェクト参照で判定
        if (other.gameObject == enemyObject)
        {
            GoToGameover();
        }
    }

    // Gameover シーンへ遷移するメソッド
    private void GoToGameover()
    {
        SceneManager.LoadScene("Gameover");
    }
}
