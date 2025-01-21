using TMPro;
using UnityEngine;

public class NewEmptyCSharpScript :MonoBehaviour
{
    int turn;
    public TextMeshProUGUI turnText;
    void Start() {turn = PlayerPrefs.GetInt("Turn", 20); UpdateTurnText(); }
    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = "Turn: " + turn;  // テキストにターン番号を表示
        }
    }
}
