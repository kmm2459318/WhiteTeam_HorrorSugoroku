using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimator; // ドアのアニメーター
    public string openAnimation = "Open"; // ドアを開けるアニメーションの名前
    public string closeAnimation = "Close"; // ドアを閉めるアニメーションの名前
    public bool isOpen = false; // ドアが開いているかどうか
    public float interactionRange = 3f; // プレイヤーがドアを開けるために必要な距離

    private Transform player; // プレイヤーの Transform
    private PlayerInventory playerInventory; // プレイヤーのインベントリ参照
    public string requiredItem = "鍵"; // 必要なアイテム
    void Start()
    {
        // プレイヤーをシーン内のタグ "Player" を持つオブジェクトに設定
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // プレイヤーのインベントリスクリプトを取得
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        // プレイヤーがドアの近くにいるか確認
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionRange) // インタラクション範囲内にいる場合
        {
            if (distance <= interactionRange && Input.GetKeyDown(KeyCode.G)) // 「E」キーでドアを開ける/閉める
            {
                if (isOpen)
                {
                    CloseDoor(); // ドアを閉める
                }
                else
                {
                    // 鍵を持っているかどうかチェック
                    if (playerInventory != null && playerInventory.HasItem("鍵"))
                    {
                      
                        OpenDoor(); // 鍵があればドアを開ける
                        Debug.Log("鍵を使って扉を開けた");
                        playerInventory.RemoveItem("鍵"); // 鍵を使う
                    }
                    else
                    {
                        Debug.Log("鍵がありません。"); // 鍵がなければ開けられない
                    }
                }
            }
        }

        // ドアを開けるメソッド
        void OpenDoor()
        {
            if (doorAnimator != null)
            {
                doorAnimator.Play(openAnimation); // アニメーションを再生
                isOpen = true;
            }
        }

        // ドアを閉めるメソッド
        void CloseDoor()
        {
            if (doorAnimator != null)
            {
                doorAnimator.Play(closeAnimation); // アニメーションを再生
                isOpen = false;
            }
        }
    }
}