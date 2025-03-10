using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // �� ���������ɑΉ��IDictionary�Ō����Ǘ�
    private Dictionary<string, int> items = new Dictionary<string, int>();

    // �A�C�e����ǉ�
    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }

        Debug.Log(itemName + " ����肵�܂����B���݂̏������F" + items[itemName]);
    }

    // �A�C�e�����g���i����j
    public bool UseItem(string itemName)
    {
        if (items.ContainsKey(itemName) && items[itemName] > 0)
        {
            items[itemName]--;
            Debug.Log(itemName + " ���g�p���܂����B�c��F" + items[itemName]);

            if (items[itemName] <= 0)
            {
                items.Remove(itemName);
            }
            return true;
        }
        else
        {
            Debug.Log(itemName + " �͏������Ă��܂���B");
            return false;
        }
    }

    // �������Ă��邩�m�F
    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName) && items[itemName] > 0;
    }

    // ���������擾
    public int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }

    // �S�A�C�e���\���i�f�o�b�O�p�j
    public void ShowInventory()
    {
        Debug.Log("=== �v���C���[�C���x���g�� ===");
        foreach (var item in items)
        {
            Debug.Log(item.Key + ": " + item.Value + "��");
        }
    }
   
}