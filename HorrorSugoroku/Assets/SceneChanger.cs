using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToStart : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Story";

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 画面クリック or タップ
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
