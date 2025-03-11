using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRandomizer : MonoBehaviour
{
    //public string KeyName; // ←これが必要！
    // 一度だけ生成したい鍵のリスト
    private List<string> oneTimeKeys = new List<string> { "食堂の鍵", "地下の鍵" };

    private List<string> keyNames = new List<string>
    {
      "一階の鍵"
    };

    // 一度だけ生成された鍵を追跡するリスト
    private List<string> generatedOneTimeKeys = new List<string>();

    // ランダムに鍵の名前を生成するメソッド
    public string GetKeyName()
    {
        // 一度だけ生成したい鍵が残っている場合、それをランダムに選択
        if (generatedOneTimeKeys.Count < oneTimeKeys.Count)
        {
            // 一度も生成されていない鍵を選択
            string keyToGenerate = oneTimeKeys[Random.Range(0, oneTimeKeys.Count)];

            // すでに生成された鍵は選ばない
            while (generatedOneTimeKeys.Contains(keyToGenerate))
            {
                keyToGenerate = oneTimeKeys[Random.Range(0, oneTimeKeys.Count)];
            }

            generatedOneTimeKeys.Add(keyToGenerate);
            return keyToGenerate;
        }
        else
        {
            // 他の鍵からランダムに選択
            int randomIndex = Random.Range(0, keyNames.Count);
            return keyNames[randomIndex];
        }
    }
    // 鍵が有効かどうかを確認するメソッド
    public bool IsValidKey(string keyName)
    {
        // 一度だけ生成された鍵か、それ以外のランダムな鍵を取得した場合は有効とする
        return oneTimeKeys.Contains(keyName) || keyName.Contains(keyName);
    }
}
