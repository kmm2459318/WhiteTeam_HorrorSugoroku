using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> items = new List<string>(); // �C���x���g���i�A�C�e�����X�g�j

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"{itemName} ���C���x���g���ɒǉ����܂����I");
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
            Debug.Log($"{itemName} ���C���x���g������폜���܂����I");
        }
        else
        {
            Debug.Log($"{itemName} �̓C���x���g���ɂ���܂���I");
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void ShowInventory()
    {
        Debug.Log("���݂̃C���x���g��: " + string.Join(", ", items));
    }
}
