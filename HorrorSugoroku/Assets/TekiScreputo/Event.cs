using UnityEngine;

public class Event : MonoBehaviour
{
    public string cellEffect = "Normal"; // マス目の効果（例: Normal, Bonus, Penalty）

    public void ActivateEffect()
    {
        // マス目の効果を発動
        switch (cellEffect)
        {
            case "Event":
                Debug.Log($"{name}: イベント発動！");
                break;
            case "Blockl":
                Debug.Log($"{name}: ペナルティ効果発動！");
                break;
            default:
                Debug.Log($"{name}: 通常マス - 効果なし。");
                break;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // タグが"Player"のオブジェクトのみ反応
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{name} にプレイヤーが到達しました（アイテムマス）");

          
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
