using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemHotbar : MonoBehaviour
{
    public Button slot1Button; // 1キーに対応するボタン
    public Button slot2Button; // 2キーに対応するボタン
    public TextMeshProUGUI slot1Text; // 1キーのスロット UI 表示
    public TextMeshProUGUI slot2Text; // 2キーのスロット UI 表示

    void Update()
    {
        // `1` キーでスロット1のボタンを押す
        if (Input.GetKeyDown(KeyCode.Alpha1) && slot1Button != null)
        {
            slot1Button.onClick.Invoke();
        }

        // `2` キーでスロット2のボタンを押す
        if (Input.GetKeyDown(KeyCode.Alpha2) && slot2Button != null)
        {
            slot2Button.onClick.Invoke();
        }
    }

    // スロットの UI ボタンをセットする（動的に変えるとき用）
    public void SetSlotButton(int slotNumber, Button button)
    {
        if (slotNumber == 1)
        {
            slot1Button = button;
            if (slot1Text != null)
                slot1Text.text = "1: " + button.name;
        }
        else if (slotNumber == 2)
        {
            slot2Button = button;
            if (slot2Text != null)
                slot2Text.text = "2: " + button.name;
        }
    }
}