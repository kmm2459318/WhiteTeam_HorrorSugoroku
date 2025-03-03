using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator doorAnimator; // ドアのアニメーター
    public Animator doorAnimatorLeft;
    public float interactionRange = 3f;
    private bool isOpen = false; // ドアの状態


    private Transform player; // プレイヤーの Transform
    public PlayerInventory playerInventory; // プレイヤーのインベントリ参照
    public string requiredItem ; // 必要なアイテム

  
    public GameObject doorPanel; // UIのパネル
    public TextMeshProUGUI messageText; // UIのテキスト
    //public TextMeshProUGUI messgeText;
    //public Button okButton;   // OKボタン
    //public Button cancelButton; // キャンセルボタン

    public GameObject hiddenArea; // 表示したいマス（ドアが開くと表示）
    void Start()
    {
        // プレイヤーをシーン内のタグ "Player" を持つオブジェクトに設定
        player = GameObject.FindGameObjectWithTag("Player").transform;


        if (messageText != null)
        {
            messageText.text = ""; // 初期状態で空にする
        }
        // プレイヤーのインベントリスクリプトを取得
        playerInventory = player.GetComponent<PlayerInventory>();


        if (doorPanel != null)
        {
            doorPanel.SetActive(false); // 最初はUIを非表示
        }
        // 隠れたマスを最初は非アクティブにする
        if (hiddenArea != null)
        {
            hiddenArea.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.E)) // 「E」キーでドアを開ける/閉める
        {
            if (doorPanel.activeSelf)
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
                    if (playerInventory != null && playerInventory.HasItem(requiredItem))
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
        //void ShowMessage(string message, float delay)
        //{
        //    if (messageText != null||doorPanel != null)
        //    {
        //       // doorPanel.SetActive(false);
        //        messageText.text = message;
        //        Invoke("ClearMessage", 2f); // 2秒後にメッセージを消す
        //    }
        //}

        void ShowMessage(string message)
        {
            if (messageText != null)
            {
                messageText.text = message;
                Invoke("ClearMessage", 2f); // 2秒後に消す
            }
        }
        void ClearMessage()
        {
            if (messageText != null)
            {
                messageText.text = ""; // メッセージを消す
            }
        }

        //IEnumerator ClosUI(string message, float delay)
        //{
        //    yield return new WaitForSeconds(delay);
        //    //Debug.Log("iii");
        //    if (doorPanel != null || doorText != null)
        //    {
        //        // Debug.Log("uuu");
        //        doorPanel.SetActive(false);
        //        doorText.text = message;
        //        CloseUI();
        //        Time.timeScale = 1; // ゲームを再開
        //    }
        //}
        void CloseUI()
        {
            bool wasPaused = false;

            if (doorPanel != null && doorPanel.activeSelf)
            {
                doorPanel.SetActive(false);
                wasPaused = true;
            }

            // UIが開いていた場合のみTime.timeScaleを戻す
            if (wasPaused)
            {
                Debug.Log("ゲーム再開！");
                Time.timeScale = 1;
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
            if (doorPanel != null)
            {
                doorPanel.SetActive(true);
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
                doorAnimatorLeft.SetBool("isOpen", true);
               
                //isOpen = true;
                //Debug.Log("ドアが開きました"); // ログを出力
                //                       // 隠れたマス（エリア）を表示
                //if (hiddenArea != null)
                //{
                //    hiddenArea.SetActive(true);
                //}
                //Destroy(gameObject);
            }
           
            isOpen = true;
            Debug.Log($"{requiredItem}ドアが開きました"); // ログを出力
            ShowMessage($"{requiredItem} のドアが開きました");

            // 隠れたマス（エリア）を表示
            if (hiddenArea != null)
            {
                hiddenArea.SetActive(true);
            }
            Destroy(gameObject);
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
