using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger2 : MonoBehaviour
{
    // Gameclear シーンへ遷移
    public void GoToGameclear()
    {
        SceneManager.LoadScene("Gameclear");
    }
}