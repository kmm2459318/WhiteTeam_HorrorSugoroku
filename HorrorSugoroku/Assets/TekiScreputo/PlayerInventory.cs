using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();
    private bool isUsingItem = false; // �A�C�e���g�p�����ǂ����̃t���O

    private bool isCooldown = false;  // �A�C�e���ǉ��̃N�[���_�E���t���O
    private bool isAddingItem = false;  // �A�C�e���ǉ��������Ǘ�����t���O
    private float cooldownTime = 3f;  // �N�[���_�E�����ԁi3�b�j

    // �A�C�e����ǉ��i�N�[���_�E��������ǉ��j
    public void AddItem(string itemName)
    {
        if (isAddingItem)
        {
            Debug.Log("���݃A�C�e���ǉ����ł��B");
            return;  // �A�C�e���ǉ����͒ǉ����X�L�b�v
        }

        if (isCooldown)
        {
            Debug.Log($"{itemName} �̓N�[���_�E�����ł��B");
            return;  // �N�[���_�E�����̓A�C�e����ǉ��ł��Ȃ�
        }

        isAddingItem = true;  // �A�C�e���ǉ����t���O�𗧂Ă�

        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }

        Debug.Log($"{itemName} ���C���x���g���ɒǉ����܂����B���݂̏������F{items[itemName]}");

        // �A�C�e���ǉ���ɃN�[���_�E���J�n
        StartCoroutine(CooldownCoroutine());
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
            Debug.Log($"{itemName} ���g�p���܂����B�c��F{items[itemName]}");

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
            Debug.Log($"{itemName} �͏������Ă��܂���B");
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

    // �N�[���_�E���p�R���[�`��
    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;  // �N�[���_�E����
        yield return new WaitForSeconds(cooldownTime);  // �w�肳�ꂽ���Ԃ����ҋ@
        isCooldown = false;  // �N�[���_�E���I��
        isAddingItem = false;  // �A�C�e���ǉ��t���O������
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
    
                
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