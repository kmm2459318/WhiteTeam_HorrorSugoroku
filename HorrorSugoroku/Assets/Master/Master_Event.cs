using UnityEngine;
using UnityEngine.Networking;
using JetBrains.Annotations;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Prjectビューの右クリックメニューにScriptableObjectを生成するメニューを追加
// fileName: 生成されるScriptableObjectのファイル名
// menuName: criptableObjectを生成するメニューの名前
// order: メニューの表示順(0なので一番上に表示される)
[CreateAssetMenu(fileName = "Master_Event", menuName = "ScriptableObjectの生成/Master_Eventの生成", order = 0)]

// シートデータを管理するScriptableObject
public class Master_Event : ScriptableObject
{
    public SheetDataRecord[] EventSheet;    // シートデータのリスト名
    [SerializeField] string EventMasterURL;    // スプレットシートのURL


    // スプレットシートの列に対応する変数を定義
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
    //スプレットシートの情報をsheetDataRecordに反映させるメソッド
    public void LoadEventData()
    {
        // urlからCSV形式の文字列をダウンロードする
        using UnityWebRequest request = UnityWebRequest.Get(EventMasterURL);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("EventMasterURL:" + request.error);
            }
        }

        // ダウンロードしたCSVをデシリアライズ(SerializeFieldに入力)する
        EventSheet = CSVSerializer.Deserialize<SheetDataRecord>(request.downloadHandler.text);

        // データの更新が完了したら、ScriptableObjectを保存する
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log("Master_Eventのデータの更新を完了しました");
    }
#endif
}