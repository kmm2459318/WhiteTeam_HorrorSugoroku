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
    public string requiredItem; // 必要なアイテム


    public GameObject doorPanel; // UIのパネル
    public TextMeshProUGUI doorText; // UIのテキスト
    public float messageDisplayTime = 2f; // メッセージを表示する時間（秒）
    //public TextMeshProUGUI messgeText;
    //public Button okButton;   // OKボタン
    //public Button cancelButton; // キャンセルボタン

    public GameObject hiddenArea; // 表示したいマス（ドアが開くと表示）
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerInventory = player.GetComponent<PlayerInventory>();

        if (doorPanel != null)
        {
            doorPanel.SetActive(false); // 最初はUIを非表示
        }

        if (hiddenArea != null)
        {
            hiddenArea.SetActive(false); // 最初は隠れたエリアを非表示
        }
    }
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.G)) // Gキーでドアを開ける
        {
            if (!isOpen)
            {
                if (playerInventory != null && playerInventory.HasItem(requiredItem))
                {
                    OpenDoorConfirmed(); // 鍵を使ってドアを開く
                }
                else
                {
                    Debug.Log("鍵がありません。"); // 鍵がなければ開けられない
                }
            }
        }
    }
    // UIを表示して一定時間後に閉じる
    IEnumerator ShowMessageAndCloseUI(string message, float delay)
    {
        if (doorPanel != null)
        {
            doorPanel.SetActive(true);
        }

        if (doorText != null)
        {
            doorText.text = message;
        }

        yield return new WaitForSeconds(delay);

        if (doorPanel != null)
        {
            doorPanel.SetActive(false);
        }
    }
    // ドアを開けるメソッド
    void OpenDoorConfirmed()
    {
        OpenDoor(); // ドアを開く
        playerInventory.RemoveItem(requiredItem); // 鍵をインベントリから削除
    }

    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", true);
            doorAnimatorLeft.SetBool("isOpen", true);
        }

        isOpen = true;
        Debug.Log($"{requiredItem} のドアが開きました");

        StartCoroutine(ShowMessageAndCloseUI($"{requiredItem} のドアが開きました", messageDisplayTime));

        if (hiddenArea != null)
        {
            hiddenArea.SetActive(true);
        }

        Destroy(gameObject); // ドアオブジェクトを削除
    }
}
