using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    // KeyRandomizer ���Q��
    public KeyRandomizer keyRandomizer;

    void Start()
    {
        // KeyRandomizer ��T���Ď擾
        if (keyRandomizer == null)
        {
            keyRandomizer = FindObjectOfType<KeyRandomizer>();
        }

        if (keyRandomizer == null)
        {
            Debug.LogError("KeyRandomizer ��������܂���I");
            return;
        }

        // �V�[�����̂��ׂĂ� Key �^�O�̕t�����I�u�W�F�N�g���擾
        GameObject[] keyObjects = GameObject.FindGameObjectsWithTag("Key");

        // �e Key �I�u�W�F�N�g�Ƀ����_���Ȍ��̖��O��U�蕪����
        foreach (GameObject keyObject in keyObjects)
        {
            // KeyRandomizer �Ń����_���Ȍ��̖��O���擾
            string keyName = keyRandomizer.GetKeyName();

            // �U�蕪�������O�����̃I�u�W�F�N�g�̖��O�ɐݒ肷��i�܂��͑��̕��@�ŕۑ��j
            keyObject.name = keyName;  // �I�u�W�F�N�g���ɐݒ肷���

            // ���O���f�o�b�O�o�́i�m�F�p�j
            Debug.Log(keyObject.name + " �ɖ��O���ݒ肳��܂����B");
        }
    }
}