using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger3 : MonoBehaviour
{
    // Gameclear �V�[���֑J��
    public void GoToGameover()
    {
        SceneManager.LoadScene("Gameover");
    }
}