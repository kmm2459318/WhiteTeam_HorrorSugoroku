using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger2 : MonoBehaviour
{
    // Gameclear �V�[���֑J��
    public void GoToGameclear()
    {
        SceneManager.LoadScene("Gameclear");
    }
}