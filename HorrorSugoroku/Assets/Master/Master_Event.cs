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
[CreateAssetMenu(fileName = "Master_Event", menuName = "ScriptableObject�̐���/Master_Event�̐���", order = 0)]

// �V�[�g�f�[�^���Ǘ�����ScriptableObject
public class Master_Event : ScriptableObject
{
    public SheetDataRecord[] EventSheet;    // �V�[�g�f�[�^�̃��X�g��
    [SerializeField] string EventMasterURL;    // �X�v���b�g�V�[�g��URL


    // �X�v���b�g�V�[�g�̗�ɑΉ�����ϐ����`
    [System.Serializable]
    public class SheetDataRecord
    {
        public int ID;
        public string Name;
        public int Choices;
        public int Consumption;
        public int MakeaSound;
        public bool DemonChase;
        public bool ItemUse;
        public bool ItemGet;
        public bool Itemlose;

    }

#if UNITY_EDITOR
    //�X�v���b�g�V�[�g�̏���sheetDataRecord�ɔ��f�����郁�\�b�h
    public void LoadEventData()
    {
        // url����CSV�`���̕�������_�E�����[�h����
        using UnityWebRequest request = UnityWebRequest.Get(EventMasterURL);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("EventMasterURL:" + request.error);
            }
        }

        // �_�E�����[�h����CSV���f�V���A���C�Y(SerializeField�ɓ���)����
        EventSheet = CSVSerializer.Deserialize<SheetDataRecord>(request.downloadHandler.text);

        // �f�[�^�̍X�V������������AScriptableObject��ۑ�����
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log("Master_Event�̃f�[�^�̍X�V���������܂���");
    }
#endif
}