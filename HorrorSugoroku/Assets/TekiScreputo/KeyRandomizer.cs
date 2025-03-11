using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRandomizer : MonoBehaviour
{
    //public string KeyName; // �����ꂪ�K�v�I
    // ��x�����������������̃��X�g
    private List<string> oneTimeKeys = new List<string> { "�H���̌�", "�n���̌�" };

    private List<string> keyNames = new List<string>
    {
      "��K�̌�"
    };

    // ��x�����������ꂽ����ǐՂ��郊�X�g
    private List<string> generatedOneTimeKeys = new List<string>();

    // �����_���Ɍ��̖��O�𐶐����郁�\�b�h
    public string GetKeyName()
    {
        // ��x�������������������c���Ă���ꍇ�A����������_���ɑI��
        if (generatedOneTimeKeys.Count < oneTimeKeys.Count)
        {
            // ��x����������Ă��Ȃ�����I��
            string keyToGenerate = oneTimeKeys[Random.Range(0, oneTimeKeys.Count)];

            // ���łɐ������ꂽ���͑I�΂Ȃ�
            while (generatedOneTimeKeys.Contains(keyToGenerate))
            {
                keyToGenerate = oneTimeKeys[Random.Range(0, oneTimeKeys.Count)];
            }

            generatedOneTimeKeys.Add(keyToGenerate);
            return keyToGenerate;
        }
        else
        {
            // ���̌����烉���_���ɑI��
            int randomIndex = Random.Range(0, keyNames.Count);
            return keyNames[randomIndex];
        }
    }
    // �����L�����ǂ������m�F���郁�\�b�h
    public bool IsValidKey(string keyName)
    {
        // ��x�����������ꂽ�����A����ȊO�̃����_���Ȍ����擾�����ꍇ�͗L���Ƃ���
        return oneTimeKeys.Contains(keyName) || keyName.Contains(keyName);
    }
}
