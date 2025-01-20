using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // ボタンで呼び出すメソッド
    public void GoToMap()
    {
        SceneManager.LoadScene("Map");
    }
}
