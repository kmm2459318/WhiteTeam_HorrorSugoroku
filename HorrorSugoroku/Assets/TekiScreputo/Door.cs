using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator doorAnimator; // ドアのアニメーター
    public float interactionRange = 3f;
    private bool isOpen = false; // ドアの状態


    private Transform player; // プレイヤーの Transform
    public PlayerInventory playerInventory; // プレイヤーのインベントリ参照
    public string requiredItem = "鍵"; // 必要なアイテム

    public GameObject doorUI; // UIのパネル（Inspector で設定）
    public Button okButton;   // OKボタン
    public Button cancelButton; // キャンセルボタン
    void Start()
    {
        // プレイヤーをシーン内のタグ "Player" を持つオブジェクトに設定
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // プレイヤーのインベントリスクリプトを取得
        playerInventory = player.GetComponent<PlayerInventory>();


        if (doorUI != null)
        {
            doorUI.SetActive(false); // 最初はUIを非表示
        }
        // ボタンのクリックイベントを登録
        //if (okButton != null)
        //    okButton.onClick.AddListener(OpenDoorConfirmed);

        //if (cancelButton != null)
        //    cancelButton.onClick.AddListener(CloseUI);
    }

    void Update()
    {
        // プレイヤーがドアの近くにいるか確認
        float distance = Vector3.Distance(player.position, transform.position);

        if (Input.GetKeyDown(KeyCode.G)) // 「E」キーでドアを開ける/閉める
        {
            if (doorUI.activeSelf)
            {
              //  Debug.Log("aaa");
                //CloseDoor(); // ドアを閉める
                CloseUI(); // UIが開いていたら閉じる
            }
        }

        if (distance <= interactionRange) // インタラクション範囲内にいる場合
        {
            if (distance <= interactionRange && Input.GetKeyDown(KeyCode.G)) // 「E」キーでドアを開ける/閉める
            {
                //if (doorUI.activeSelf)
                //{
                //    Debug.Log("aaa");
                //    //CloseDoor(); // ドアを閉める
                //    CloseUI(); // UIが開いていたら閉じる
                //}
                 if (!isOpen)
                {
                    // 鍵を持っているかどうかチェック
                    if (playerInventory != null && playerInventory.HasItem("鍵"))
                    {

                        //  ShowDoorUI();
                        OpenDoorConfirmed(); // 鍵を使ってドアを開く
                    }
                    else
                    {
                        Debug.Log("鍵がありません。"); // 鍵がなければ開けられない
                    }
                }
            }
        }
       

        //void ShowDoorUI()
        //{
        //    if (doorUI != null)
        //    {
        //        doorUI.SetActive(true);
        //        Time.timeScale = 0;
        //    }
        //}

        void CloseUI()
        {
            //Debug.Log("iii");
            if (doorUI != null)
            {
               // Debug.Log("uuu");
                doorUI.SetActive(false);
                isOpen = false;
                Time.timeScale = 1; // ゲームを再開
            }
        }

        //void OpenDoorConfirmed()
        //{
        //    CloseUI(); // UIを閉じる
        //    OpenDoor(); // ドアを開く
        //    playerInventory.RemoveItem(requiredItem);
        //}

        void OpenDoorConfirmed()
        {
            OpenDoor(); // ドアを開く
            if (doorUI != null)
            {
                doorUI.SetActive(true);
               // Time.timeScale = 0;
            }
          
            playerInventory.RemoveItem(requiredItem); // 鍵をインベントリから削除
        }


        // ドアを開けるメソッド
        void OpenDoor()
        {
            if (doorAnimator != null)
            {
                Debug.Log("dadwed");
                doorAnimator.SetBool("isOpen", true); // アニメーションを再生
                isOpen = true;
                Debug.Log("ドアが開きました"); // ログを出力

                Destroy(gameObject);
            }
        }

        // ドアを閉めるメソッド
        //void CloseDoor()
        //{
        //    if (doorAnimator != null)
        //    {
        //        doorAnimator.SetBool("isOpen", false); // アニメーションを再生
        //        isOpen = false;
        //    }
        //}
    }
}
