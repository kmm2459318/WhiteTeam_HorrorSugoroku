using UnityEngine;

public class Clickebleobj : MonoBehaviour
{
    // このスクリプトはクリック可能なオブジェクトにアタッチされます
    public string objectTag;  // このオブジェクトのタグに基づいて処理を変える場合に使用
    public PlayerInventory playerInventory;  // プレイヤーインベントリへの参照
    public KeyRandomizer keyRandomizer;  // KeyRandomizerクラスへの参照

    void Start()
    {
        // 必要なコンポーネントの参照を取得
        if (playerInventory == null) playerInventory = FindObjectOfType<PlayerInventory>();
        if (keyRandomizer == null) keyRandomizer = FindObjectOfType<KeyRandomizer>();
    }
    // クリックされたオブジェクトに対応するスクリプトを実行
    void OnMouseDown()
    {
        // このオブジェクトのタグを確認
        if (CompareTag("Key"))
        {
            // ランダムな鍵の名前を取得
            string keyName = keyRandomizer.GetKeyName();
            // プレイヤーインベントリに追加
            playerInventory.AddItem(keyName);
            Debug.Log(keyName + " がインベントリに追加されました。");
            // ここでキーに関する処理を行う
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
            Debug.Log("未対応のタグ: " + tag);
        }
    }

    // キーオブジェクトのスクリプトを実行
    //void ExecuteKeyScript()
    //{
    //    Debug.Log("キーオブジェクトがクリックされました。");
    //    // ランダムな鍵の名前を取得
    //    string keyName = keyRandomizer.GetRandomKeyName();
    //    // プレイヤーインベントリに追加
    //    playerInventory.AddItem(keyName);
    //    Debug.Log(keyName + " がインベントリに追加されました。");
    //    // ここでキーに関する処理を行う
    //}

    // 地図オブジェクトのスクリプトを実行
    void ExecuteMapScript()
    {
        Debug.Log("地図オブジェクトがクリックされました。");
        // ここで地図に関する処理を行う
    }

    // アイテムオブジェクトのスクリプトを実行
    void ExecuteItemScript()
    {
        Debug.Log("アイテムオブジェクトがクリックされました。");
        // ここでアイテムに関する処理を行う
    }
}
