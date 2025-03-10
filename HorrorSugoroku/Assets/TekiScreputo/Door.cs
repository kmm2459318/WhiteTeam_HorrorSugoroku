using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // リストを使うために追加


public class Door : MonoBehaviour
{
    public Animator doorAnimator; // ドアのアニメーター
    public Animator doorAnimatorLeft;
    public float interactionRange = 3f;
    private bool isOpen = false; // ドアの状態

    private Transform player; // プレイヤーの Transform
    public PlayerInventory playerInventory; // プレイヤーのインベントリ参照
    public string requiredItem = ""; // 必要なアイテム（""なら鍵不要）

    public GameObject doorPanel; // UIのパネル
    public TextMeshProUGUI doorText; // UIのテキスト
    public float messageDisplayTime = 2f; // メッセージを表示する時間

    public GameObject hiddenArea; // 隠れたエリア（ドアが開くと表示）
    // 🔹 [Inspectorで設定可能] 鍵を消費しないドアかどうか
    [Header("鍵を消費しないドア")]
    public bool noKeyConsume = false;

    public Animator leftOpenAnimator; // LeftOpen���ݒ肷���ʂ̃I�u�W�F�N�g�̃A�j���[�^�[
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerInventory = player.GetComponent<PlayerInventory>();

        if (doorPanel != null) doorPanel.SetActive(false);
        if (hiddenArea != null) hiddenArea.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (Input.GetKeyDown(KeyCode.G) && distance <= interactionRange)
        {
            if (!isOpen)
            {
                // 鍵が必要なドアかチェック
                if (string.IsNullOrEmpty(requiredItem) || (playerInventory != null && playerInventory.HasItem(requiredItem)))
                {
                    OpenDoor();
                }
                else
                {
                    ShowMessage("鍵がありません");
                }
            }
        }
    }

    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", true);
            doorAnimatorLeft.SetBool("isOpen", true);
        }

        if (leftOpenAnimator != null)
        {
            leftOpenAnimator.SetBool("LeftOpen", true); // LeftOpen��true�ɐݒ�
            StartCoroutine(ResetLeftOpen()); // 30�b����LeftOpen��false�ɂ����R���[�`�����J�n
        }

        isOpen = true;
        ShowMessage("ドアが開きました");

        // 🔹 「鍵が必要」かつ「鍵を消費しない設定でない」場合のみ消費
        //if (!string.IsNullOrEmpty(requiredItem) && !noKeyConsume)
        //{
        //    playerInventory.RemoveItem(requiredItem); // 鍵を消費
        //}

        if (hiddenArea != null) hiddenArea.SetActive(true);
        Destroy(gameObject, messageDisplayTime); // UIを閉じる時間後にドアを削除
    }

    void ShowMessage(string message)
    {
        if (doorPanel != null && doorText != null)
        {
            doorPanel.SetActive(true);
            doorText.text = message;
            StartCoroutine(HideMessage());
        }
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(messageDisplayTime);
        if (doorPanel != null) doorPanel.SetActive(false);
    }
    

    // 30�b����LeftOpen��false�ɂ����R���[�`��
    IEnumerator ResetLeftOpen()
    {
        yield return new WaitForSeconds(30f);
        if (doorAnimatorLeft != null)
        {
            doorAnimatorLeft.SetBool("LeftOpen", false);
        }
    }
}
