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
[CreateAssetMenu(fileName = "Master_Item", menuName = "ScriptableObjectの生成/Master_Itemの生成", order = 0)]

// シートデータを管理するScriptableObject
public class Master_Item : ScriptableObject
{
    public SheetDataRecord[] ItemSheet;    // シートデータのリスト名
    [SerializeField] string ItemMasterURL;    // スプレットシートのURL


    // スプレットシートの列に対応する変数を定義
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
    //スプレットシートの情報をsheetDataRecordに反映させるメソッド
    public void LoadItemData()
    {
        // urlからCSV形式の文字列をダウンロードする
        using UnityWebRequest request = UnityWebRequest.Get(ItemMasterURL);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("ItemMasterURL:" + request.error);
            }
        }

        // ダウンロードしたCSVをデシリアライズ(SerializeFieldに入力)する
        ItemSheet = CSVSerializer.Deserialize<SheetDataRecord>(request.downloadHandler.text);

        // データの更新が完了したら、ScriptableObjectを保存する
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log("Master_Itemのデータの更新を完了しました");
    }
#endif
}