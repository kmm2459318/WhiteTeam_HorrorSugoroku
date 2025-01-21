using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger3 : MonoBehaviour
{
    // Gameclear ƒV[ƒ“‚Ö‘JˆÚ
    public void GoToGameover()
    {
        SceneManager.LoadScene("Gameover");
    }
}