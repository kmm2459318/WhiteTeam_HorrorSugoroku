using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();
    private bool isUsingItem = false; // �A�C�e���g�p�����ǂ����̃t���O

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
        if (isUsingItem)
        {
            Debug.Log("���ݑ��̃A�C�e�����g�p���ł�");
            return false;
        }

        Debug.Log("UseItem���\�b�h���Ăяo����܂���: " + itemName);
        if (items.ContainsKey(itemName) && items[itemName] > 0)
        {
            isUsingItem = true; // �A�C�e���g�p���t���O��ݒ�
            items[itemName]--;
            Debug.Log(itemName + " ���g�p���܂����B�c��F" + items[itemName]);

            if (items[itemName] <= 0)
            {
                items.Remove(itemName);
                Debug.Log(itemName + " ���C���x���g������폜����܂����B");
            }

            // �A�C�e���g�p��Ƀt���O�����Z�b�g
            StartCoroutine(ResetItemUsageFlag());

            return true;
        }
        else
        {
            Debug.Log(itemName + " �͏������Ă��܂���B");
            return false;
        }
    }

    // �A�C�e���g�p���t���O�����Z�b�g����R���[�`��
    private IEnumerator ResetItemUsageFlag()
    {
        yield return new WaitForSeconds(1f); // 1�b��Ƀt���O�����Z�b�g
        isUsingItem = false;
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

    // �����A�C�e����ǉ����郁�\�b�h
    void Start()
    {
        AddItem("��K�̃J�M");
        AddItem("��K�̃J�M");
        AddItem("�H���̃J�M");
        AddItem("�z�[���̃J�M");
        AddItem("�㖱���̃J�M");
        AddItem("�x�b�h���[���̃J�M");
        AddItem("�n�����̃J�M");
        AddItem("�n�����̃J�M�P");
        AddItem("�n�����̃J�M�Q");
        AddItem("�n�����̃J�M�R");
        AddItem("�G���W�����[���̃J�M");
    }
}