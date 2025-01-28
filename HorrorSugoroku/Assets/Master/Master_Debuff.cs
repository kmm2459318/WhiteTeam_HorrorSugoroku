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
[CreateAssetMenu(fileName = "Master_Debuff", menuName = "ScriptableObjectの生成/Master_Debuffの生成", order = 0)]

// シートデータを管理するScriptableObject
public class Master_Debuff : ScriptableObject
{
    public SheetDataRecord[] DebuffSheet;    // シートデータのリスト名
    [SerializeField] string DebuffMasterURL;    // スプレットシートのURL


    // スプレットシートの列に対応する変数を定義
    [System.Serializable]
    public class SheetDataRecord
    {
        public int ID;
        public string Name;
        public int DecreaseMin;
        public int DecreaseMax;
        public bool ItemGive;
        public bool UnusableItem;
        public int UnusableItemTrun;

    }

#if UNITY_EDITOR
    //スプレットシートの情報をsheetDataRecordに反映させるメソッド
    public void LoadDebuffData()
    {
        // urlからCSV形式の文字列をダウンロードする
        using UnityWebRequest request = UnityWebRequest.Get(DebuffMasterURL);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("DebuffMasterURL:" + request.error);
            }
        }

        // ダウンロードしたCSVをデシリアライズ(SerializeFieldに入力)する
        DebuffSheet = CSVSerializer.Deserialize<SheetDataRecord>(request.downloadHandler.text);

        // データの更新が完了したら、ScriptableObjectを保存する
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log("Master_Debuffのデータの更新を完了しました");
    }
#endif
}