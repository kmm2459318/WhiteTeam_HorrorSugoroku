using UnityEngine;

public class DiceRangeManager : MonoBehaviour
{
    private int minDiceValue = 1;
    private int maxDiceValue = 6;
    private bool shouldTransformRoll = false; // サイコロの出目を変換するかどうかのフラグ

    public void SetDiceRollRange(int min, int max)
    {
        minDiceValue = min;
        maxDiceValue = max;
    }

    public int RollDice()
    {
        int roll = Random.Range(minDiceValue, maxDiceValue + 1);
        if (shouldTransformRoll)
        {
            roll = TransformDiceRoll(roll);
        }
        return roll;
    }

    private int TransformDiceRoll(int diceValue)
    {
        switch (diceValue)
        {
            case 4:
                return 1;
            case 5:
                return 2;
            case 6:
                return 3;
            default:
                return diceValue;
        }
    }

    public void EnableTransformRoll()
    {
        shouldTransformRoll = true;
    }
}