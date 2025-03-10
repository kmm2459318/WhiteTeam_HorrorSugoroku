using UnityEngine;

public class Clickebleobj : MonoBehaviour
{
    // ���̃X�N���v�g�̓N���b�N�\�ȃI�u�W�F�N�g�ɃA�^�b�`����܂�
    public string objectTag;  // ���̃I�u�W�F�N�g�̃^�O�Ɋ�Â��ď�����ς���ꍇ�Ɏg�p
    public PlayerInventory playerInventory;  // �v���C���[�C���x���g���ւ̎Q��
    public KeyRandomizer keyRandomizer;  // KeyRandomizer�N���X�ւ̎Q��

    void Start()
    {
        // �K�v�ȃR���|�[�l���g�̎Q�Ƃ��擾
        if (playerInventory == null) playerInventory = FindObjectOfType<PlayerInventory>();
        if (keyRandomizer == null) keyRandomizer = FindObjectOfType<KeyRandomizer>();
    }
    // �N���b�N���ꂽ�I�u�W�F�N�g�ɑΉ�����X�N���v�g�����s
    void OnMouseDown()
    {
        // ���̃I�u�W�F�N�g�̃^�O���m�F
        if (CompareTag("Key"))
        {
            // �����_���Ȍ��̖��O���擾
            string keyName = keyRandomizer.GetKeyName();
            // �v���C���[�C���x���g���ɒǉ�
            playerInventory.AddItem(keyName);
            Debug.Log(keyName + " ���C���x���g���ɒǉ�����܂����B");
            // �����ŃL�[�Ɋւ��鏈�����s��
          //  ExecuteKeyScript();
        }
        else if (CompareTag("Map"))
        {
            ExecuteMapScript();
        }
        else if (CompareTag("Item"))
        {
            ExecuteItemScript();
        }
        else
        {
            Debug.Log("���Ή��̃^�O: " + tag);
        }
    }

    // �L�[�I�u�W�F�N�g�̃X�N���v�g�����s
    //void ExecuteKeyScript()
    //{
    //    Debug.Log("�L�[�I�u�W�F�N�g���N���b�N����܂����B");
    //    // �����_���Ȍ��̖��O���擾
    //    string keyName = keyRandomizer.GetRandomKeyName();
    //    // �v���C���[�C���x���g���ɒǉ�
    //    playerInventory.AddItem(keyName);
    //    Debug.Log(keyName + " ���C���x���g���ɒǉ�����܂����B");
    //    // �����ŃL�[�Ɋւ��鏈�����s��
    //}

    // �n�}�I�u�W�F�N�g�̃X�N���v�g�����s
    void ExecuteMapScript()
    {
        Debug.Log("�n�}�I�u�W�F�N�g���N���b�N����܂����B");
        // �����Œn�}�Ɋւ��鏈�����s��
    }

    // �A�C�e���I�u�W�F�N�g�̃X�N���v�g�����s
    void ExecuteItemScript()
    {
        Debug.Log("�A�C�e���I�u�W�F�N�g���N���b�N����܂����B");
        // �����ŃA�C�e���Ɋւ��鏈�����s��
    }
}
