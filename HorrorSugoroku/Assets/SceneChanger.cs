using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToStart : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Story";

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��ʃN���b�N or �^�b�v
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
