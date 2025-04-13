using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public KeyRandomizer keyRandomizer;

    void Start()
    {
        if (keyRandomizer == null)
        {
            keyRandomizer = FindObjectOfType<KeyRandomizer>();
        }

        if (keyRandomizer == null)
        {
            Debug.LogError("KeyRandomizer ��������܂���I");
            return;
        }

        // �A�C�e�����蓖�ă��X�g�擾
        List<string> itemList = keyRandomizer.GetGeneratedItems();

        // �V�[������ "Item" �^�O�I�u�W�F�N�g���擾
        GameObject[] keyObjects = GameObject.FindGameObjectsWithTag("Item");

        if (itemList.Count < keyObjects.Length)
        {
            Debug.LogWarning("�A�C�e���̐����I�u�W�F�N�g�̕��������ł��I");
        }

        // ���ԂɊ��蓖��
        for (int i = 0; i < keyObjects.Length && i < itemList.Count; i++)
        {
            GameObject keyObject = keyObjects[i];
            string itemName = itemList[i];

            keyObject.name = itemName;

            Debug.Log($"{keyObject.name} �ɖ��O��ݒ肵�܂����B");
        }
    }
}