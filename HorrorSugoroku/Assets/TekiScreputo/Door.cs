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
    public Animator childAnimator; // 子オブジェクトのアニメーター
    public Animator rightAnimator; // 右側のドアのアニメーター

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
    public Transform windowTransform1; // 窓1のTransform
    public Transform windowTransform2; // 窓2のTransform

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

        // 左側のドアのAnimatorを名前で取得
        Transform leftTransform = transform.Find("Left");
        if (leftTransform != null)
        {
            childAnimator = leftTransform.GetComponent<Animator>();
            Debug.Log("Left Animator set");

            // 窓1のTransformを取得
            windowTransform1 = leftTransform.Find("Window1");
            if (windowTransform1 != null)
            {
                Debug.Log("Window1 Transform set");
            }
            else
            {
                Debug.Log("Window1 object not found");
            }

            // 窓2のTransformを取得
            windowTransform2 = leftTransform.Find("Window2");
            if (windowTransform2 != null)
            {
                Debug.Log("Window2 Transform set");
            }
            else
            {
                Debug.Log("Window2 object not found");
            }
        }
        else
        {
            Debug.Log("Left object not found");
        }

        // 右側のドアのAnimatorを名前で取得
        Transform rightTransform = transform.Find("Right");
        if (rightTransform != null)
        {
            rightAnimator = rightTransform.GetComponent<Animator>();
            Debug.Log("Right Animator set");
        }
        else
        {
            Debug.Log("Right object not found");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        Debug.Log($"Distance: {distance}, Interaction Range: {interactionRange}");

        if (distance <= interactionRange)
        {
            if (Input.GetKeyDown(KeyCode.G)) // Gキーに戻す
            {
                Debug.Log("G key pressed");
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

                // LeftOpenTriggerを設定して左側のドアのアニメーションを再生
                if (childAnimator != null)
                {
                    Debug.Log("Setting LeftOpenTrigger");
                    childAnimator.SetTrigger("LeftOpenTrigger");
                    StartCoroutine(TransitionLeftAnimation());
                }
                else
                {
                    Debug.Log("Left Animator is null");
                }

                // RightOpenTriggerを設定して右側のドアのアニメーションを再生
                if (rightAnimator != null)
                {
                    Debug.Log("Setting RightOpenTrigger");
                    rightAnimator.SetTrigger("RightOpenTrigger");
                    StartCoroutine(TransitionRightAnimation());
                }
                else
                {
                    Debug.Log("Right Animator is null");
                }
            }
            else
            {
                Debug.Log("G key not pressed");
            }
        }
    }

    IEnumerator TransitionLeftAnimation()
    {
        yield return new WaitForSeconds(10f);
        if (childAnimator != null)
        {
            Debug.Log("Transitioning to Create Open Door right 2");
            childAnimator.SetTrigger("CreateOpenDoorRight2");
        }
    }

    IEnumerator TransitionRightAnimation()
    {
        yield return new WaitForSeconds(10f);
        if (rightAnimator != null)
        {
            Debug.Log("Transitioning to Create Open Door2");
            rightAnimator.SetTrigger("CreateOpenDoor2");
        }
    }

    void LateUpdate()
    {
        // 左側のドアに追従する窓の位置や回転を更新
        if (windowTransform1 != null && childAnimator != null)
        {
            Transform leftDoorTransform = childAnimator.transform;
            windowTransform1.position = leftDoorTransform.position;
            windowTransform1.rotation = leftDoorTransform.rotation;
        }

        if (windowTransform2 != null && childAnimator != null)
        {
            Transform leftDoorTransform = childAnimator.transform;
            windowTransform2.position = leftDoorTransform.position;
            windowTransform2.rotation = leftDoorTransform.rotation;
        }

        // 右側のドアに追従する窓の位置や回転を更新
        if (windowTransform1 != null && rightAnimator != null)
        {
            Transform rightDoorTransform = rightAnimator.transform;
            windowTransform1.position = rightDoorTransform.position;
            windowTransform1.rotation = rightDoorTransform.rotation;
        }

        if (windowTransform2 != null && rightAnimator != null)
        {
            Transform rightDoorTransform = rightAnimator.transform;
            windowTransform2.position = rightDoorTransform.position;
            windowTransform2.rotation = rightDoorTransform.rotation;
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
            doorAnimator.SetTrigger("LeftOpenTrigger");
        }

        isOpen = true;
        Debug.Log($"{requiredItem} のドアが開きました");

        StartCoroutine(ShowMessageAndCloseUI($"{requiredItem} のドアが開きました", messageDisplayTime));

        if (hiddenArea != null)
        {
            hiddenArea.SetActive(true);
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
}