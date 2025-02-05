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
[CreateAssetMenu(fileName = "Master_Curse", menuName = "ScriptableObject�̐���/Master_Curse�̐���", order = 0)]

// �V�[�g�f�[�^���Ǘ�����ScriptableObject
public class Master_Curse : ScriptableObject
{
    public SheetDataRecord[] CurseSheet;    // �V�[�g�f�[�^�̃��X�g��
    [SerializeField] string CurseMasterURL;    // �X�v���b�g�V�[�g��URL
    internal bool isCursed;


    // �X�v���b�g�V�[�g�̗�ɑΉ�����ϐ����`
    [System.Serializable]
    public class SheetDataRecord
    {
        public int ID;
        public string Name;
        public int ItemIncrease;
        public int TurnIncrease;
        public int CurseGive;
        public int ItemLimit;
        public int MoveMin;
        public int MoveMax;
        
    }

#if UNITY_EDITOR
    //�X�v���b�g�V�[�g�̏���sheetDataRecord�ɔ��f�����郁�\�b�h
    public void LoadCurseData()
    {
        // url����CSV�`���̕�������_�E�����[�h����
        using UnityWebRequest request = UnityWebRequest.Get(CurseMasterURL);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("CurseMasterURL:" + request.error);
            }
        }

        // �_�E�����[�h����CSV���f�V���A���C�Y(SerializeField�ɓ���)����
        CurseSheet = CSVSerializer.Deserialize<SheetDataRecord>(request.downloadHandler.text);

        // �f�[�^�̍X�V������������AScriptableObject��ۑ�����
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log("Master_Curse�̃f�[�^�̍X�V���������܂���");
    }
#endif
}