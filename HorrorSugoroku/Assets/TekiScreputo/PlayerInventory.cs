using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> inventory = new List<string>(); // �C���x���g���i�A�C�e�����X�g�j
    private bool itemAdded = false; // �A�C�e���ǉ�����x������������t���O
    private float itemAddCooldown = 2f; // �ēx�A�C�e����ǉ��ł���܂ł̑ҋ@���ԁi�b�j
    public void AddItem(string itemName)
    {
        // ���łɃA�C�e�����ǉ�����Ă���ꍇ�͒ǉ����Ȃ�
        if (itemAdded)
        {
            Debug.LogWarning("�A�C�e���͊��ɒǉ�����Ă��܂��B��x�����ǉ��ł��܂��B");
            return;
        }

        // �A�C�e�����C���x���g���ɑ��݂��Ȃ��ꍇ�̂ݒǉ�
        if (!inventory.Contains(itemName))
        {
            inventory.Add(itemName); // �A�C�e����ǉ�
            Debug.Log($"{itemName} ���C���x���g���ɒǉ����܂����B���݂̃A�C�e����: {inventory.Count}");
            itemAdded = true; // �A�C�e���ǉ��t���O�𗧂Ă�

            // �A�C�e���ǉ���ɃR���[�`���Ő��b��Ƀt���O�����Z�b�g
            StartCoroutine(ResetItemAddCooldown());
        }
        else
        {
            Debug.LogWarning($"{itemName} �͊��ɃC���x���g���ɂ���܂��I");
        }
    }

    // �A�C�e�����C���x���g������폜����
    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log($"{itemName} ���C���x���g������폜���܂����I");
            itemAdded = false; // �A�C�e���폜���Ƀt���O�����Z�b�g�i�ēx�A�C�e���ǉ��\�j
        }
        else
        {
            Debug.Log($"{itemName} �̓C���x���g���ɂ���܂���I");
        }
    }

    // �A�C�e����ǉ�������A��莞�Ԍ�Ƀt���O�����Z�b�g����R���[�`��
    private IEnumerator ResetItemAddCooldown()
    {
        // ��莞�ԑҋ@
        yield return new WaitForSeconds(itemAddCooldown);

        // �t���O�����Z�b�g
        itemAdded = false;
        Debug.Log("�A�C�e���ǉ��̃N�[���_�E�����I�����܂����B�ēx�A�C�e����ǉ��ł��܂��B");
    }

    // �C���x���g���Ɏw�肵���A�C�e�����܂܂�Ă��邩���m�F����
    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    // ���݂̃C���x���g���̓��e��\������
    public void ShowInventory()
    {
        Debug.Log("���݂̃C���x���g��: " + string.Join(", ", inventory));
    }
}