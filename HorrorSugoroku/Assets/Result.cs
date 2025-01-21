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
            turnText.text = "Turn: " + turn;  // �e�L�X�g�Ƀ^�[���ԍ���\��
        }
        if (moveText != null)
        {
            moveText.text = "Move: " + move;  // �e�L�X�g�Ƀ^�[���ԍ���\��
        }
    }
}
