using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    // KeyRandomizer を参照
    public KeyRandomizer keyRandomizer;

    void Start()
    {
        // KeyRandomizer を探して取得
        if (keyRandomizer == null)
        {
            keyRandomizer = FindObjectOfType<KeyRandomizer>();
        }

        if (keyRandomizer == null)
        {
            Debug.LogError("KeyRandomizer が見つかりません！");
            return;
        }

        // シーン内のすべての Key タグの付いたオブジェクトを取得
        GameObject[] keyObjects = GameObject.FindGameObjectsWithTag("Key");

        // 各 Key オブジェクトにランダムな鍵の名前を振り分ける
        foreach (GameObject keyObject in keyObjects)
        {
            // KeyRandomizer でランダムな鍵の名前を取得
            string keyName = keyRandomizer.GetKeyName();

            // 振り分けた名前をそのオブジェクトの名前に設定する（または他の方法で保存）
            keyObject.name = keyName;  // オブジェクト名に設定する例

            // 名前をデバッグ出力（確認用）
            Debug.Log(keyObject.name + " に名前が設定されました。");
        }
    }
}