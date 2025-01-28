using UnityEngine;
using UnityEngine.Networking;
using JetBrains.Annotations;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Prject�r���[�̉E�N���b�N���j���[��ScriptableObject�𐶐����郁�j���[��ǉ�
// fileName: ���������ScriptableObject�̃t�@�C����
// menuName: criptableObject�𐶐����郁�j���[�̖��O
// order: ���j���[�̕\����(0�Ȃ̂ň�ԏ�ɕ\�������)
[CreateAssetMenu(fileName = "Master_Item", menuName = "ScriptableObject�̐���/Master_Item�̐���", order = 0)]

// �V�[�g�f�[�^���Ǘ�����ScriptableObject
public class Master_Item : ScriptableObject
{
    public SheetDataRecord[] ItemSheet;    // �V�[�g�f�[�^�̃��X�g��
    [SerializeField] string ItemMasterURL;    // �X�v���b�g�V�[�g��URL


    // �X�v���b�g�V�[�g�̗�ɑΉ�����ϐ����`
    [System.Serializable]
    public class SheetDataRecord
    {
        public int ID;
        public string Name;
        public enum Type { Player, Ghost, Map, }
        public Type type;
        public int Recovery;
        public int Volume;
        public int UsedTurn;

    }

#if UNITY_EDITOR
    //�X�v���b�g�V�[�g�̏���sheetDataRecord�ɔ��f�����郁�\�b�h
    public void LoadItemData()
    {
        // url����CSV�`���̕�������_�E�����[�h����
        using UnityWebRequest request = UnityWebRequest.Get(ItemMasterURL);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("ItemMasterURL:" + request.error);
            }
        }

        // �_�E�����[�h����CSV���f�V���A���C�Y(SerializeField�ɓ���)����
        ItemSheet = CSVSerializer.Deserialize<SheetDataRecord>(request.downloadHandler.text);

        // �f�[�^�̍X�V������������AScriptableObject��ۑ�����
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log("Master_Item�̃f�[�^�̍X�V���������܂���");
    }
#endif
}