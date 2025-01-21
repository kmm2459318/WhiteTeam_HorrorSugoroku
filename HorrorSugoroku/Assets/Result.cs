using TMPro;
using UnityEngine;

public class NewEmptyCSharpScript :MonoBehaviour
{
    int turn;
    public TextMeshProUGUI turnText;
    int move;
    public TextMeshProUGUI moveText;
    void Start()
    {
        turn = PlayerPrefs.GetInt("Turn", 20);
        move = PlayerPrefs.GetInt("move", 20); UpdateTurnText();
    }
    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = "Turn: " + turn;  // テキストにターン番号を表示
        }
        if (moveText != null)
        {
            moveText.text = "Move: " + move;  // テキストにターン番号を表示
        }
    }
}
