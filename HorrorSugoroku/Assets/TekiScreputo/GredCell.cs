using UnityEngine;

public class GredCell : MonoBehaviour
{
    public string cellType = "Normal"; // マス目の種類（例: Normal, Bonus, Penalty）

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{name} にプレイヤーが到達しました。マスの種類: {cellType}");

            // マス目の種類に応じたイベントを実行
            ExecuteEvent();
        }
    }

    void ExecuteEvent()
    {
        switch (cellType)
        {
            case "Bonus":
                Debug.Log("ボーナスマス: プレイヤーがボーナスを獲得！");
                break;
            case "Penalty":
                Debug.Log("ペナルティマス: プレイヤーがペナルティを受けました！");
                break;
            default:
                Debug.Log("通常マス: 特にイベントなし。");
                break;
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
